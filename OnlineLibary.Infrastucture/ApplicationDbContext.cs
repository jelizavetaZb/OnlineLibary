using Microsoft.EntityFrameworkCore;
using OnlineLibary.Domain.Entities.BookEntities;
using OnlineLibary.Domain.Entities.UserEntities;

namespace OnlineLibary.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserUserRole> UserUserRoles { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Chapter> Chapters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //User related configs
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserIdentityDbContext).Assembly);

            // Applies all configurations defined in this assembly
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}