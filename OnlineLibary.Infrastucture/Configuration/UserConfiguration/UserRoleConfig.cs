using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLibary.Domain.Entities.UserEntities;
using OnlineLibary.Domain.Enums;
using OnlineLibary.Infrastructure.IdentityConfiguration;

namespace OnlineLibary.Infrastructure.Configuration.UserConfiguration
{
    public class UserRoleConfig : IdentityRoleConfig<UserRole, UserUserRole>
    {
        public override void Configure(EntityTypeBuilder<UserRole> builder)
        {
            base.Configure(builder);
            //builder.HasData(GetSeeds());
        }
        #region Seed Data

        //private static UserRole[] GetSeeds()
        //{
        //    return new[]
        //    {
        //        new UserRole
        //        {
        //            Id = 1, Name = "GlobalAdmin",
        //            NormalizedName = "GLOBALADMIN",
        //            ConcurrencyStamp = "5c3dcebe-5788-4322-9dc3-12f8e32e5934",
        //            UserRoleType = UserRoleType.GlobalAdmin,
        //            Description = "Allowed to view and edit all"
        //        },
        //        new UserRole
        //        {
        //        Id = 2,
        //        Name = "User",
        //        NormalizedName = "USER",
        //        ConcurrencyStamp = "3e661345-4f50-4684-91eb-d80f0caba702",
        //        UserRoleType = UserRoleType.User,
        //        Description = "Can read books"
        //        },
        //        new UserRole
        //        {
        //            Id = 4,
        //            Name = "Editor",
        //            NormalizedName = "EDITOR",
        //            ConcurrencyStamp = "976ef246-a185-4e6e-a31c-eeee18c491ff",
        //            UserRoleType = UserRoleType.Editor,
        //            Description = "Can add books"
        //        }
        //    };
        //}

        #endregion
    }
}
