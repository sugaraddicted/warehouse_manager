using System.Collections.Generic;
using System.Threading.Tasks;
using Warehouse_Manager.Data.Base;
using Warehouse_Manager.MVVM.Model;

namespace Warehouse_Manager.Data.Services.Interfaces
{
    public interface IOrderService : IEntityBaseRepository<Order>
    {
        Task StoreOrderAsync(Order order);
        Order GetOrderById(int orderId);

        Task<List<Order>> GetAllOrdersByUserIdAsync(int userId);
    }
}
