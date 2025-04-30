using Autofac;
using MapsterMapper;
using Medium.Application.Services;
using Medium.Domain;
using Medium.Domain.RepositoriesInterface;
using Medium.Domain.ServicesInterface;
using Medium.Domain.UnitOfWorkInterface;
using Medium.Infrastructure;
using Medium.Infrastructure.Data;
using Medium.Infrastructure.Repositories;
using Medium.Infrastructure.UnitOfWork;
using Medium.Web.Data;

namespace Medium.Web.WebModules
{
    public class WebModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssembly;

        public WebModule(string connectionString, string migrationAssembly)
        {
            _connectionString = connectionString;
            _migrationAssembly = migrationAssembly;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MediumDbContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssembly", _migrationAssembly)
                .InstancePerLifetimeScope();

            builder.RegisterType<ApplicationDbContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssembly", _migrationAssembly)
                .InstancePerLifetimeScope();

            builder.RegisterType<MediumUnitOfWork>().As<IMediumUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CategoryManagementService>().As<ICategoryManagementService>()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<ApplicationTime>().As<IApplicationTime>()
                .InstancePerLifetimeScope();

            /*builder.RegisterType<IMapper>().As<ServiceMapper>()
                .InstancePerLifetimeScope();*/

            base.Load(builder);
        }
    }
}
