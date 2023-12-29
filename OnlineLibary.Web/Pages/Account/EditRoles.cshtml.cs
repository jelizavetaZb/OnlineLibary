﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineLibary.Domain.Entities.UserEntities;
using OnlineLibary.Domain.Enums;
using OnlineLibary.Managers.Managers;
using OnlineLibary.Managers.Models.Identity;
using OnlineLibary.Web.Helpers;

namespace OnlineLibary.Web.Pages.Account
{
    [CustomAuthorize(UserRoleType.GlobalAdmin)]
    public class EditRolesModel : PageModel
    {
        private readonly UserCustomManager _authManager;
        public EditRolesModel(UserCustomManager authManager)
        {
            _authManager = authManager;
        }
        [BindProperty]
        public UserRoleEditModel Input {  get; set; }

        public IActionResult OnGet(int id)
        {
            if (id == 0)
            {
                return Redirect(PagesList.Error);
            }
            Input = _authManager.GetUserRoleEditModel(id);
            var userRoles = _authManager.GetUserRoles(id).ToList();
            var allRoles = _authManager.GetAllRoles()?.ToList();

            foreach (var role in allRoles)
            {
                role.IsSelected = userRoles
                    .Select(x => x.Id)
                    .Contains(role.Id);
            }
            Input.Roles = allRoles;
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var result = await _authManager.UpdateUserRoleAsync(Input);
                if (result.IsSuccess)
                {
                    return Redirect(PagesList.AccountUsers);
                }
                if (result.Errors.Any())
                {
                    foreach (var (key, error) in result.Errors)
                    {
                        ModelState.AddModelError(key, error);
                    }
                }
            }
            return Page();
        }

    }
}