using Medium.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Medium.Infrastructure.Data
{
    public class MediumDbContext : DbContext
    {
        public MediumDbContext(DbContextOptions<MediumDbContext> options) : base(options)
        {
            
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
