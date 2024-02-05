using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Manager.Data.Base;
using Warehouse_Manager.Data.Services.Interfaces;
using Warehouse_Manager.MVVM.Model;
using Warehouse_Manager.MVVM.ViewModel;

namespace Warehouse_Manager.Data.Services
{
    public class UserService : EntityBaseRepository<User>, IUserService
    {
        private readonly AppDbContext _context;
        public UserService(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.Users.Where(u => u.Username == username).FirstOrDefaultAsync(); 
        }
        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync(); 
        }
    }
}
