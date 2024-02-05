using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Manager.Data.Base;
using Warehouse_Manager.MVVM.Model;
using Warehouse_Manager.MVVM.ViewModel;

namespace Warehouse_Manager.Data.Services.Interfaces
{
    public interface IOrderService : IEntityBaseRepository<Order>
    {
        Task StoreOrderAsync(Order order);

        Task<List<Order>> GetAllOrdersByUserIdAsync(int userId);
    }
}
