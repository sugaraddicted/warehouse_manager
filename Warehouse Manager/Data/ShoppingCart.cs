using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using Warehouse_Manager.Data;

namespace Warehouse_Manager.MVVM.Model
{
    public class ShoppingCart
    {
        public AppDbContext _context { get; set; }

        public string Id { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCart(AppDbContext context)
        {
            _context = context;
        }

        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            var context = services.GetService<AppDbContext>();

            string cartId = "";

            return new ShoppingCart(context) { Id = cartId };

        }

        public void AddItemToCart(OrderItem item)
        {
            var shoppingCartItem =
                _context.ShoppingCartItems.FirstOrDefault(n => n.OrderItem.Id == item.Id && n.Id == item.Id);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = Id,
                    OrderItem = item
                };
                _context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.OrderItem.Quantity++;
            }

            _context.SaveChanges();
        }

        public void RemoveItemFromCart(Product product)
        {
            var shoppingCartItem =
                _context.ShoppingCartItems.FirstOrDefault(n =>
                    n.OrderItem.Product.Id == product.Id && n.ShoppingCartId == Id);

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.OrderItem.Quantity > 1)
                {
                    shoppingCartItem.OrderItem.Quantity--;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _context.SaveChanges();
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _context.ShoppingCartItems
                .Where(n => n.ShoppingCartId == Id).Include(n => n.OrderItem.Product).ToList());
        }

        public async Task ClearShoppingCartAsync()
        {
            var items = await _context.ShoppingCartItems.Where(n => n.ShoppingCartId == Id).ToListAsync();

            _context.ShoppingCartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }

        public decimal GetShoppingCartTotal() => _context.ShoppingCartItems
            .Where(n => n.ShoppingCartId == Id).Select(n => n.OrderItem.Product.Price * n.OrderItem.Quantity).Sum();
    }
}
