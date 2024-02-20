using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse_Manager.Data.Base;
using Warehouse_Manager.Data.Services.Interfaces;
using Warehouse_Manager.MVVM.Model;

namespace Warehouse_Manager.Data.Services
{
    public class OrderService : EntityBaseRepository<Order>, IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task<List<Order>> GetAllOrdersByUserIdAsync(int userId)
        {
            var orders = await _context.Orders.Include(n => n.OrderItems).ThenInclude(n => n.Product).Include(n => n.User).ToListAsync();

            orders = orders.Where(n => n.UserId == userId).ToList();

            return orders;
        }

        public Order GetOrderById(int orderId)
        {
            var order = _context.Orders.Where(o => o.Id == orderId).Include(n => n.OrderItems)
                .ThenInclude(n => n.Product).Include(n => n.User).Include(n => n.ShippingAddress).FirstOrDefault();

            return order;
        }
        public async Task StoreOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }
    }
}
