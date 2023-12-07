using OnlineLibary.Domain.Entities.UserEntities;
using OnlineLibary.Infrastructure.IdentityConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibary.Infrastructure.Configuration.UserConfiguration
{
    public class UserLoginConfig : IdentityUserLoginConfig<UserLogin> { }
}
