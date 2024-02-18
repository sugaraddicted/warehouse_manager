using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Warehouse_Manager.MVVM.Model;
using Warehouse_Manager.Exeptions;
using Warehouse_Manager.Dto;
using Warehouse_Manager.Data.Services.Interfaces;

namespace Warehouse_Manager.Data.Services.AuthenticationServices
{
    public enum RegistrationResult
    {
        Success,
        PasswordsDoNotMatch,
        EmailAlreadyUsed,
        UsernameAlreadyUsed,
        NotAllFieldsAreFilled
    }
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthenticationService(IUserService userService, IPasswordHasher<User> passwordHasher)
        {
            _userService = userService;
            _passwordHasher = passwordHasher;
        }
        public async Task<User> Login(string username, string password)
        {
            User storedUser = await _userService.GetByUsernameAsync(username);
            if(storedUser == null)
            {
                throw new UserNotFoundExeption(username);
            }

            try
            {
                var verificationResult = _passwordHasher.VerifyHashedPassword(storedUser, storedUser.PasswordHash, password);
                if (verificationResult != PasswordVerificationResult.Success)
                {
                    throw new InvalidPasswordExeption(username, password);
                }
                return storedUser;
            }
            catch (UserNotFoundExeption ex)
            {
                throw ex;
            } 
            return storedUser;
        }

        public async Task<RegistrationResult> Register(RegisterDto registerDto, string role)
        {
            RegistrationResult result = RegistrationResult.Success;
            if(registerDto.Password != registerDto.PasswordConfirmation)
            {
                result = RegistrationResult.PasswordsDoNotMatch;
            }

            if(registerDto.Firstname.IsNullOrEmpty())
            {
                result = RegistrationResult.NotAllFieldsAreFilled;
            }
            if (registerDto.Lastname.IsNullOrEmpty())
            {
                result = RegistrationResult.NotAllFieldsAreFilled;
            }
            if (registerDto.Username.IsNullOrEmpty())
            {
                result = RegistrationResult.NotAllFieldsAreFilled;
            }
            if (registerDto.Email.IsNullOrEmpty())
            {
                result = RegistrationResult.NotAllFieldsAreFilled;
            }
            if (registerDto.Password.IsNullOrEmpty())
            {
                result = RegistrationResult.NotAllFieldsAreFilled;
            }

            if (await _userService.GetByUsernameAsync(registerDto.Username) != null)
            {
                result = RegistrationResult.UsernameAlreadyUsed;
            }

            if (await _userService.GetByEmailAsync(registerDto.Email) != null)
            {
                result = RegistrationResult.EmailAlreadyUsed;
            }

            if (result == RegistrationResult.Success)
            {
                User user = new User()
                {
                    Email = registerDto.Email,
                    Username = registerDto.Username,
                    Firstname = registerDto.Firstname,
                    Lastname = registerDto.Lastname,
                    UserRole = role
                };
                string hash = _passwordHasher.HashPassword(user, registerDto.Password);
                user.PasswordHash = hash;
                await _userService.AddUserAsync(user);
            }

            return result;
        }
    }
}
