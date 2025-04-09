using Autofac;
using Autofac.Extensions.DependencyInjection;
using Medium.Infrastructure.Data;
using Medium.Web.Data;
using Medium.Web.WebModules;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System.Reflection;

namespace Medium.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region Bootstrap Logger
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connection = configuration.GetConnectionString("DefaultConnection");

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug().WriteTo.MSSqlServer(
                    connectionString: connection,
                    sinkOptions: new MSSqlServerSinkOptions { TableName = "ApplicationLogs", AutoCreateSqlTable = false })
                .ReadFrom.Configuration(configuration)
                .CreateBootstrapLogger();
            #endregion

            try
            {
                Log.Information("Application Starting...");
                var builder = WebApplication.CreateBuilder(args);

                #region Serilog Configuration
                builder.Host.UseSerilog((hostBuilderContext, loggerConfiguration) =>
                {
                    loggerConfiguration.MinimumLevel.Debug()
                    .WriteTo.MSSqlServer(
                        connectionString: connection,
                        sinkOptions: new MSSqlServerSinkOptions { TableName = "ApplicationLogs", AutoCreateSqlTable = false })
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .Enrich.FromLogContext()
                    .ReadFrom.Configuration(builder.Configuration);

                });
                #endregion

                // Add services to the container.
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
                var migrationAssembly = Assembly.GetExecutingAssembly() ?? throw new InvalidOperationException("Migration Assembly not found.");

                builder.Services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(connectionString));

                builder.Services.AddDbContext<MediumDbContext>(options =>
                    options.UseSqlServer(connectionString, (x) => x.MigrationsAssembly(migrationAssembly)));

                builder.Services.AddDatabaseDeveloperPageExceptionFilter();

                builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<ApplicationDbContext>();
                builder.Services.AddControllersWithViews();

                #region Autofac Configuration
                builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
                    .ConfigureContainer<ContainerBuilder>(containerBuilder =>
                    {
                        containerBuilder.RegisterModule(new WebModule(connectionString, migrationAssembly.FullName));
                    });
                #endregion

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseMigrationsEndPoint();
                }
                else
                {
                    app.UseExceptionHandler("/Home/Error");
                    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                    app.UseHsts();
                }

                app.UseHttpsRedirection();
                app.UseRouting();

                app.UseAuthorization();

                app.MapStaticAssets();

                app.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}")
                    .WithStaticAssets();

                app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}")
                    .WithStaticAssets();

                app.MapRazorPages()
                   .WithStaticAssets();

                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application Crashed...");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
