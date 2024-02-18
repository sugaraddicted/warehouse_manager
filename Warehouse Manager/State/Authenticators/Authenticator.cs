using System;
using System.Threading.Tasks;
using Warehouse_Manager.Data;
using Warehouse_Manager.Data.Services.AuthenticationServices;
using Warehouse_Manager.Data.Services.Interfaces;
using Warehouse_Manager.Dto;
using Warehouse_Manager.MVVM.Model;

namespace Warehouse_Manager.State.Authenticators
{
    public class Authenticator : IAuthenticator
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly AppDbContext _dbContext;

        public Authenticator(IAuthenticationService authenticationService, AppDbContext dbContext)
        {
            _authenticationService = authenticationService;
            _dbContext = dbContext;
        }

        public User? CurrentUser { get; private set; }

        public ShoppingCart? ShoppingCart { get; set; }

        public bool IsLogedIn { get; set;
        }

        public async Task<bool> Login(string username, string password)
        {
            bool success = true;
           try
            {
                CurrentUser = await _authenticationService.Login(username, password);
                ShoppingCart = new ShoppingCart(_dbContext) { ShoppingCartId = CurrentUser.Id.ToString() };
            }
            catch (Exception)
            { 
               success = false;
            }
            return success;
        }

        public void Logout()
        {
            CurrentUser = null;
            ShoppingCart = null;
            IsLogedIn = false;
        }

        public async Task<RegistrationResult> Register(RegisterDto registerDto, string role)
        {
            return await _authenticationService.Register(registerDto, role);
        }
    }
}
