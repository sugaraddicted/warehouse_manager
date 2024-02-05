using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public User CurrentUser { get; private set; }

        public ShoppingCart ShoppingCart { get; set; }

        public bool IsLogedIn => CurrentUser != null;

        public async Task<bool> Login(string username, string password)
        {
            bool success = true;
           // try
           // {
                CurrentUser = await _authenticationService.Login(username, password);
                ShoppingCart = new ShoppingCart(_dbContext) { ShoppingCartId = CurrentUser.Id.ToString() };
            //}
           // catch (Exception)
           // {
            //    success = false;
           // }
            return success;

        }

        public void Logout()
        {
            CurrentUser = null;
        }

        public async Task<RegistrationResult> Register(RegisterDto registerDto, string role)
        {
            return await _authenticationService.Regicter(registerDto, role);
        }
    }
}
