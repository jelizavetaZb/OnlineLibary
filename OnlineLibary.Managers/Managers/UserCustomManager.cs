using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OnlineLibary.Domain.Entities.UserEntities;
using OnlineLibary.Domain.Enums;
using OnlineLibary.Infrastructure.Repositories;
using OnlineLibary.Managers.Helpers;
using OnlineLibary.Managers.Models;
using OnlineLibary.Managers.Models.Identity;
using System.Data;
using System.Security.Claims;

namespace OnlineLibary.Managers.Managers
{
    public class UserCustomManager : BaseManager
    {
        private readonly SignInManager<User> _signInManager;
        private readonly IdentityUserManager _userManager;
        private readonly ILogger<UserCustomManager> _logger;
        private readonly UserRepository _userRepository;
        private readonly IConfiguration _config;
        private readonly FileHelper _fileManager;
        private readonly RolesRepository _rolesRepository;
        private readonly UserUserRolesRepository _userUserRolesRepository;
        protected string _ipAddress;
        protected ClaimsPrincipal _user;

        public UserCustomManager(SignInManager<User> signInManager, IdentityUserManager userManager, ILogger<UserCustomManager> logger,
            IHttpContextAccessor contextAccessor, UserRepository userRepository, IMapper mapper, IConfiguration configuration,
            FileHelper fileManager, RolesRepository rolesRepository, UserUserRolesRepository userUserRolesRepository) : base(mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _userRepository = userRepository;
            _config = configuration;
            _fileManager = fileManager;
            _rolesRepository = rolesRepository;
            _userUserRolesRepository = userUserRolesRepository;
            _ipAddress = contextAccessor.HttpContext!.Connection.RemoteIpAddress!.ToString();
            _user = contextAccessor.HttpContext!.User;
        }

        public async Task<IdentityResult> RegisterUser(RegisterInputModel model)
        {
            var user = new User()
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateCreated = DateTime.UtcNow,
                EmailConfirmed = true //for now not realesed email confirmation
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return result;
            }
            await _userManager.AddToRoleAsync(user, UserRoleType.Reader.ToString());
            await _signInManager.SignInAsync(user, isPersistent: false);

            _logger.LogInformation("User created");
            return result;
        }

        public async Task SingOut()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
        }

        public async Task<SignInResult> Login(LoginInputModel model)
        {
            return await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, lockoutOnFailure: false);
        }

        public string GetUserFirstAndLastName()
        {
            var user = GetCurrentUser();
            return user.FirstName + " " + user.LastName;
        }

        public ProfileEditModel GetProfileEditModel(int? id)
        {
            var user = id != null ? _userRepository.GetById(id.Value) : GetCurrentUser();
            return Mapper.Map<ProfileEditModel>(user);
        }

        public UserRoleEditModel GetUserRoleEditModel(int? id)
        {
            var user = id != null ? _userRepository.GetById(id.Value) : GetCurrentUser();
            return Mapper.Map<UserRoleEditModel>(user);
        }

        public async Task<ResponseResult> UpdateUserProfileAsync(ProfileEditModel model)
        {
            var result = new ResponseResult
            {
                UpdatedId = model.Id
            };

            if (model == null)
            {
                result.AddError(string.Empty, "Form data are empty");
                return result;
            }
            var user = _userRepository.GetById(model.Id);

            if (user == null)
            {
                result.AddError(nameof(model.Id), "User not founded");
                return result;
            }

            if (model.NewProfileImage != null && model.NewProfileImage.Length > 0)
            {
                if (!model.NewProfileImage.ContentType.Contains("image"))
                {
                    result.AddError(nameof(model.NewProfileImage), "Allowed only images");
                    return result;
                }
                var folder = _config["System:CoverImagesFolder"];
                user.ProfileImage = await _fileManager.UploadFile(model.NewProfileImage, folder);
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            _userRepository.Update(user);

            result.IsSuccess = true;
            return result;
        }

        public ResponseResult UpdateUserRole(UserRoleEditModel model)
        {
            var result = new ResponseResult
            {
                UpdatedId = model.Id
            };

            if (model == null)
            {
                result.AddError(string.Empty, "Form data are empty");
                return result;
            }
            var user = _userRepository.GetById(model.Id);

            if (user == null)
            {
                result.AddError(nameof(model.Id), "User not founded");
                return result;
            }

            _userUserRolesRepository.DeleteRange(user.UserRoles);
            var roles = model.Roles.Where(x => x.IsSelected);
            if (roles.Any())
            {
                var userRoles = new List<UserUserRole>();
                foreach (var role in roles)
                {
                    userRoles.Add(new UserUserRole() { RoleId = role.Id, UserId = user.Id });
                }
                _userUserRolesRepository.AddRange(userRoles);
            }


            result.IsSuccess = true;
            return result;
        }

        public List<UserTableModel> GetUsersTable()
        {
            var users = _userRepository.GetAll();
            return users.ProjectTo<UserTableModel>(MapperConfig).ToList();
        }

        public int GetCurrentUserId()
        {
            return int.Parse(_userManager.GetUserId(_user));
        }

        public IEnumerable<UserRoleModel> GetUserRoles(int userId)
        {
            var query = _userRepository.GetById(userId).UserRoles.Select(x => x.Role).AsQueryable();
            return query.ProjectTo<UserRoleModel>(MapperConfig);
        }

        public IQueryable<UserRoleModel> GetAllRoles()
        {
            var roles = _rolesRepository.GetAll();
            return roles.ProjectTo<UserRoleModel>(MapperConfig);
        }

        private User GetCurrentUser()
        {
            var id = int.Parse(_userManager.GetUserId(_user));
            var user = _userRepository.GetById(id);
            return user;
        }

    }
}
