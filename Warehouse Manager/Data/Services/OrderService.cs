using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Warehouse_Manager.Data.Base;
using Warehouse_Manager.Data.Services.Interfaces;
using Warehouse_Manager.MVVM.Model;
using Warehouse_Manager.MVVM.ViewModel;

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

        public async Task StoreOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach (var item in order.OrderItems)
            {
                item.OrderId = order.Id;
                await _context.AddAsync(item);
            }
            await _context.SaveChangesAsync();
        }
    }
}
