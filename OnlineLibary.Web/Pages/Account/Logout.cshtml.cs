// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineLibary.Managers.Managers;

namespace OnlineLibary.Web.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly AuthManager _authManager;
        public LogoutModel(AuthManager authManager)
        {
            _authManager = authManager;
        }

        public async Task<IActionResult> OnGet()
        {
            await _authManager.SingOut();
            return LocalRedirect(SitePages.Index);
        }

        public async Task<IActionResult> OnPost()
        {
            await _authManager.SingOut();
            return LocalRedirect(SitePages.Index);
        }
    }
}
