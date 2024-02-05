using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Manager.MVVM.Model;

namespace Warehouse_Manager.Data.Services.Interfaces
{
    public interface IUserService
    {
        Task AddUserAsync(User user);
        Task<User> GetByUsernameAsync(string username);
        Task<User> GetByEmailAsync(string email);
    }
}
