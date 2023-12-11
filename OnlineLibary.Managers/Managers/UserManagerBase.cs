using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using OnlineLibary.Domain.Enums;
using System.Security.Claims;

namespace OnlineLibary.Managers.Managers
{
    public class UserManagerBase 
    {
        protected readonly IHttpContextAccessor ContextAccessor;
        protected string IpAddress => ContextAccessor.HttpContext!.Connection.RemoteIpAddress!.ToString();
        protected ClaimsPrincipal User => ContextAccessor.HttpContext!.User;

        public UserManagerBase(IHttpContextAccessor contextAccessor)
        {
            ContextAccessor = contextAccessor;
        }
    }
}
