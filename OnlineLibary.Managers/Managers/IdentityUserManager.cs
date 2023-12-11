﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OnlineLibary.Domain.Entities.UserEntities;
using OnlineLibary.Domain.Enums;
using OnlineLibary.Infrastructure.Extensions;
using System.Security.Claims;

namespace OnlineLibary.Managers.Managers
{
    public class IdentityUserManager : UserManager<User>
    {
        public IdentityUserManager(IUserStore<User> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher,
            IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators,
            ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors,
            IServiceProvider services, ILogger<UserManager<User>> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger) { }

        public async Task<bool> IsInAnyRoleAsync(User user, params UserRoleType[] roles)
        {
            return await roles.AnyAsync(async x => await IsInRoleAsync(user, x.ToString()));
        }

        public async Task<bool> HasLoginsAsync(ClaimsPrincipal user)
            => await HasLoginsAsync(await GetUserAsync(user));

        public async Task<bool> HasLoginsAsync(User user)
            => (await GetLoginsAsync(user)).Any();
    }
}
