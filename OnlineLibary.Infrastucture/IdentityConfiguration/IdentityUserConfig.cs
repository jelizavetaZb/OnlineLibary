using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineLibary.Infrastructure.Extensions;

namespace OnlineLibary.Infrastructure.IdentityConfiguration
{
    public class IdentityUserConfig<TUser, TUserLogin, TUserToken, TUserRole> : IEntityTypeConfiguration<TUser>
           where TUser : IdentityUser<int>
           where TUserLogin : IdentityUserLogin<int>
           where TUserToken : IdentityUserToken<int>
           where TUserRole : IdentityUserRole<int>
    {
        public virtual void Configure(EntityTypeBuilder<TUser> builder)
        {
            builder.HasIndex(u => u.NormalizedUserName).HasDatabaseName("UserNameIndex").IsUnique();
            builder.HasIndex(u => u.NormalizedEmail).HasDatabaseName("EmailIndex");
            builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            builder.Property(u => u.UserName).HasMaxLength(256);
            builder.Property(u => u.NormalizedUserName).HasMaxLength(256);
            builder.Property(u => u.Email).HasMaxLength(256);
            builder.Property(u => u.NormalizedEmail).HasMaxLength(256);

            builder.HasMany<TUserLogin>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();
            builder.HasMany<TUserToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();
            builder.HasMany<TUserRole>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
            builder.ToPluralEntityTypeName();
        }
    }
}
