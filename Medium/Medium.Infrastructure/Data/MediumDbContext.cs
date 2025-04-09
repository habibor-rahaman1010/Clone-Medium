using Medium.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Medium.Infrastructure.Data
{
    public class MediumDbContext : DbContext
    {
        private readonly string _connectionString;
        private readonly string _migrationAssembly;

        public MediumDbContext(string connectionString, string migrationAssembly)
        {
            _connectionString = connectionString;
            _migrationAssembly = migrationAssembly;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString, (x) => x.MigrationsAssembly(_migrationAssembly));
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationLogs>()
                .HasKey(x => x.Id);

            base.OnModelCreating(builder);
        }

        public DbSet<ApplicationLogs> ApplicationLogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}
