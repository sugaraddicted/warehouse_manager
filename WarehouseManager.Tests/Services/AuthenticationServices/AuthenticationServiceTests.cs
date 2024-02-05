
using Microsoft.AspNetCore.Identity;
using Moq;
using Warehouse_Manager.Data;
using Warehouse_Manager.Data.Services.AuthenticationServices;
using Warehouse_Manager.Data.Services.Interfaces;
using Warehouse_Manager.Dto;
using Warehouse_Manager.Exeptions;
using Warehouse_Manager.MVVM.Model;

namespace WarehouseManager.Tests.Services.AuthenticationServices
{
    [TestFixture]
    public class AuthenticationServiceTests
    {
        private Mock<IUserService> _mockUserService;
        private Mock<IPasswordHasher<User>> _mockPasswordHasher;
        private IAuthenticationService _authenticationService;

        [SetUp]
        public void SetUp()
        {
            _mockUserService = new Mock<IUserService>();
            _mockPasswordHasher = new Mock<IPasswordHasher<User>>();
            _authenticationService = new AuthenticationService(_mockUserService.Object, _mockPasswordHasher.Object);
        }

        [Test]
        public async Task Login_WithValidCradential_ReturnsCorrectUser()
        {
            //Arrange
            string username = "username";
            var password = "password";
            _mockUserService.Setup(s => s.GetByUsernameAsync(username)).ReturnsAsync(new User() { Username = username });
            _mockPasswordHasher.Setup(s => s.VerifyHashedPassword(It.IsAny<User>(), It.IsAny<string>(), password)).Returns(PasswordVerificationResult.Success);
            
            //Act
            User user = await _authenticationService.Login(username, password);

            //Assert
            string actualUsername = user.Username;
            Assert.AreEqual(username, actualUsername);
        }

        [Test]
        public void Login_WithInvalidCradential_ThrowsInvalidPasswordExeptionForUsername()
        {
            //Arrange
            string username = "username";
            var password = "password";
            _mockUserService.Setup(s => s.GetByUsernameAsync(username)).ReturnsAsync(new User() { Username = username });
            _mockPasswordHasher.Setup(s => s.VerifyHashedPassword(It.IsAny<User>(), It.IsAny<string>(), password)).Returns(PasswordVerificationResult.Failed);

            //Act
            InvalidPasswordExeption ex = Assert.ThrowsAsync<InvalidPasswordExeption>(() => _authenticationService.Login(username, password));

            //Assert
            string actualUsername = ex.Username;
            Assert.AreEqual(username, actualUsername);
        }


        [Test]
        public void Login_WithNonExistingUsername_ThrowsInvalidPasswordExeptionForUsername()
        {
            //Arrange
            string username = "username";
            var password = "password";
            _mockPasswordHasher.Setup(s => s.VerifyHashedPassword(It.IsAny<User>(), It.IsAny<string>(), password)).Returns(PasswordVerificationResult.Failed);

            //Act
            UserNotFoundExeption ex = Assert.ThrowsAsync<UserNotFoundExeption>(() => _authenticationService.Login(username, password));

            //Assert
            string actualUsername = ex.Username;
            Assert.AreEqual(username, actualUsername);
        }

        [Test]
        public async Task Register_WithNotMatchingPasswords_ReturnsPasswordsDoNotMatch()
        {
            //Arrange
            string password = "123456";
            string confirmPassword = "12345";
            RegistrationResult expectedResult = RegistrationResult.PasswordsDoNotMatch;
            RegisterDto registerDto = new RegisterDto()
            {
                Email = It.IsAny<string>(),
                Username = It.IsAny<string>(),
                Firstname = It.IsAny<string>(),
                Lastname = It.IsAny<string>(),
                Password = password,
                PasswordConfirmation = confirmPassword
            };

            //Act
            RegistrationResult actual = await _authenticationService.Regicter(registerDto, It.IsAny<string>());
            
            //Assert
            Assert.AreEqual(expectedResult, actual);
        }

        [Test]
        public async Task Register_WithAlreadyUsedEmail_ReturnsEmailAlreadyUsed()
        {
            //Arrange
            string email = "email@email.com";
            RegistrationResult expectedResult = RegistrationResult.EmailAlreadyUsed;
            _mockUserService.Setup(s => s.GetByEmailAsync(email)).ReturnsAsync(new User { Email = email });
            RegisterDto registerDto = new RegisterDto()
            {
                Email = email,
                Username = It.IsAny<string>(),
                Firstname = It.IsAny<string>(),
                Lastname = It.IsAny<string>(),
                Password = "password",
                PasswordConfirmation = "password"
            };

            //Act
            RegistrationResult actual = await _authenticationService.Regicter(registerDto, It.IsAny<string>());

            //Assert
            Assert.AreEqual(expectedResult, actual);
        }

        [Test]
        public async Task Register_WithAlreadyUsedUsername_ReturnsUsernameAlreadyUsed()
        {
            //Arrange
            string username = "username";
            RegistrationResult expectedResult = RegistrationResult.UsernameAlreadyUsed;
            _mockUserService.Setup(s => s.GetByUsernameAsync(username)).ReturnsAsync(new User { Username = username });
            RegisterDto registerDto = new RegisterDto()
            {
                Email = It.IsAny<string>(),
                Username = username,
                Firstname = It.IsAny<string>(),
                Lastname = It.IsAny<string>(),
                Password = "password",
                PasswordConfirmation = "password"
            };

            //Act
            RegistrationResult actual = await _authenticationService.Regicter(registerDto, It.IsAny<string>());

            //Assert
            Assert.AreEqual(expectedResult, actual);
        }

        [Test]
        public async Task Register_WithNonExistingUserAndMatchingPasswords_ReturnsSuccess()
        {
            //Arrange
            RegistrationResult expectedResult = RegistrationResult.Success;
            RegisterDto registerDto = new RegisterDto()
            {
                Email = It.IsAny<string>(),
                Username = It.IsAny<string>(),
                Firstname = It.IsAny<string>(),
                Lastname = It.IsAny<string>(),
                Password = It.IsAny<string>(),
                PasswordConfirmation = It.IsAny<string>()
            };

            //Act
            RegistrationResult actual = await _authenticationService.Regicter(registerDto, It.IsAny<string>());

            //Assert
            Assert.AreEqual(expectedResult, actual);
        }
    }
}
