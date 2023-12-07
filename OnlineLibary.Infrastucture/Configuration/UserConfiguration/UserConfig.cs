using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLibary.Domain.Entities.UserEntities;
using OnlineLibary.Infrastructure.Extensions;
using OnlineLibary.Infrastructure.IdentityConfiguration;

namespace OnlineLibary.Infrastructure.Configuration.UserConfiguration
{
    public class UserConfig : IdentityUserConfig<User, UserLogin, UserToken, UserUserRole>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);
            builder.Property(e => e.DateCreated).DatabaseGenerated();
            //builder.HasData(GetSeeds());
        }
        #region Seed Data

        //private static User[] GetSeeds()
        //{
        //    const string userName = "jz21055@edu.lu.lv";
        //    return new[]
        //    {
        //        new User
        //        {
        //            Id = 1,
        //            AccessFailedCount = 0,
        //            UserName = userName,
        //            Email = userName,
        //            NormalizedEmail = userName.ToUpper(),
        //            NormalizedUserName = userName.ToUpper(),
        //            PasswordHash = "AQAAAAEAACcQAAAAEGN+9sRVEjwfnw+tNGeHwGFDiEP1Gbz8YHShVwOqFGs6u/ZLqobZDfTpCmGFZqjeXw==",
        //            ConcurrencyStamp =  "e875331b-5839-4bd0-921c-5b0949ae83d5",
        //            SecurityStamp = "ZPQCE7HYOUCBSMOZ72WKH6DKCASCEY5Q",
        //            EmailConfirmed = true,
        //            LockoutEnabled = false,
        //            PhoneNumberConfirmed = false,
        //            TwoFactorEnabled = false,
        //        }
        //    };
        //}

        #endregion
    }
}
