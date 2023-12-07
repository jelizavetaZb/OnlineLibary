using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineLibary.Domain.Entities.UserEntities;
using OnlineLibary.Infrastructure.Extensions;

namespace OnlineLibary.Infrastructure;

public class UserIdentityDbContext : IdentityDbContext<User, UserRole, int, UserClaim, UserUserRole, UserLogin, UserRoleClaim, UserToken>
    {
        public UserIdentityDbContext(DbContextOptions<UserIdentityDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Explicitly set the Provider Name in order to switch correct database specifics (sql statements)
            EntityTypeBuilderExtensions.ProviderName = Database.ProviderName;
            // Applies all configurations defined in this assembly
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            // Disable cascade delete globally
            modelBuilder.DisableCascadeDelete();
        }
    }
