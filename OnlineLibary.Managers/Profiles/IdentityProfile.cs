using AutoMapper;
using OnlineLibary.Domain.Entities.UserEntities;
using OnlineLibary.Managers.Models.Identity;

namespace OnlineLibary.Managers.Profiles
{
    public class IdentityProfile : Profile
    {
        public IdentityProfile()
        {
            CreateMap<User, ProfileEditModel>();
            CreateMap<User, UserRoleEditModel>();
            CreateMap<User, UserTableModel>();
            CreateMap<UserRole, UserRoleModel>();
        }
    }
}
