using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Manager.Data.Services.AuthenticationServices;
using Warehouse_Manager.Dto;
using Warehouse_Manager.MVVM.Model;

namespace Warehouse_Manager.Data.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<RegistrationResult> Register(RegisterDto registerDto, string role);
        Task<User> Login(string username, string password);
    }
}
