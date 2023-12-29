using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace OnlineLibary.Managers.Managers
{
    public class BaseUserManager
    {
        protected readonly IHttpContextAccessor ContextAccessor;
        protected string IpAddress => ContextAccessor.HttpContext!.Connection.RemoteIpAddress!.ToString();
        protected ClaimsPrincipal User => ContextAccessor.HttpContext!.User;

        protected IMapper Mapper;
        protected IConfigurationProvider MapperConfig => Mapper.ConfigurationProvider;

        public BaseUserManager(IHttpContextAccessor contextAccessor, IMapper mapper)
        {
            Mapper = mapper;
            ContextAccessor = contextAccessor;
        }
    }
}
