using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OnlineLibary.Domain.Enums;

namespace OnlineLibary.Web.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public sealed class CustomAuthorize : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly UserRoleType[] _roles;
        public CustomAuthorize(params UserRoleType[] roles)
        {
            _roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (!user.Identity.IsAuthenticated)
                return;
            if (!user.HasAnyRole(_roles))
                context.Result = new RedirectResult(PagesList.Error);
        }
    }
}
