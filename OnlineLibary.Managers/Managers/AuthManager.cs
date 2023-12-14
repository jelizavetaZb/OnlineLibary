using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OnlineLibary.Domain.Entities.UserEntities;
using OnlineLibary.Domain.Enums;
using OnlineLibary.Infrastructure.Repositories;
using OnlineLibary.Managers.Models.Identity;

namespace OnlineLibary.Managers.Managers
{
    public class AuthManager : BaseUserManager
    {
        private readonly SignInManager<User> _signInManager;
        private readonly IdentityUserManager _userManager;
        private readonly ILogger<AuthManager> _logger;
        private readonly UserRepository _userRepository;
        private readonly IConfiguration _config;
        public AuthManager(SignInManager<User> signInManager, IdentityUserManager userManager, ILogger<AuthManager> logger,
            IHttpContextAccessor contextAccessor, UserRepository userRepository, IMapper mapper, IConfiguration configuration) : base(contextAccessor, mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _userRepository = userRepository;
            _config = configuration;
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
            await _userManager.AddToRoleAsync(user, UserRoleType.User.ToString());
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

        public ProfileEditModel GetUserProfile(int? id)
        {
            var user = id != null ? _userRepository.GetById(id.Value) : GetCurrentUser();
            return Mapper.Map<ProfileEditModel>(user);
        }

        public async Task UpdateUserPhotoAsync(IFormFile file, int userId)
        {
            if (file == null || file.Length > 0 || !file.ContentType.Contains("image"))
            {
                return;
            }

            var realPath = _config["System:ProfileImages"];
            var fileName = new Guid() + file.FileName;
            var upload = Path.Combine(realPath, fileName);
            using (var fileStream = new FileStream(upload, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            var user = _userRepository.GetById(userId);
            user.ProfileImage = Path.Combine(realPath, fileName);
            _userRepository.Update(user);
        }

        private User GetCurrentUser()
        {
            var id = int.Parse(_userManager.GetUserId(User));
            var user = _userRepository.GetById(id);
            return user;
        }

    }
}
