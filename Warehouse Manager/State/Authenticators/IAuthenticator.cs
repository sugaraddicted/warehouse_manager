using System.Threading.Tasks;
using Warehouse_Manager.Data.Services.AuthenticationServices;
using Warehouse_Manager.Dto;
using Warehouse_Manager.MVVM.Model;

namespace Warehouse_Manager.State.Authenticators
{
    public interface IAuthenticator
    {
        User CurrentUser { get;}
        ShoppingCart ShoppingCart { get; set; }
        bool IsLogedIn { get; set; }

        Task<RegistrationResult> Register(RegisterDto registerDto, string role);
        Task<bool> Login(string username, string password);
        void Logout();
    }
}
