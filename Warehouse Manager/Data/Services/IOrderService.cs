using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Manager.MVVM.Model;

namespace Warehouse_Manager.Data.Services
{
    public interface IOrderService
    {
        Task StoreOrdedrAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress);

        Task<List<Order>> GetAllOrdersByUserIdAndRoleAsync(string userId, string userRole);
    }
}
