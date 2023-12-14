using AutoMapper;
using OnlineLibary.Domain.Entities.UserEntities;
using OnlineLibary.Managers.Models.Identity;

namespace OnlineLibary.Managers.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, ProfileEditModel>();
        }
    }
}
