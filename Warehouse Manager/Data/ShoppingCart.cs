using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Warehouse_Manager.Data;

namespace Warehouse_Manager.MVVM.Model
{
    public class ShoppingCart
    {
        private AppDbContext _context { get; set; }

        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCart(AppDbContext context)
        {
            _context = context;
        }

        public void AddItemToCart(Product product)
        {
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.Product.Id == product.Id && n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = _context.Products.FirstOrDefault(n => n.Id == product.Id),
                    Quantity = 1
                };
                _context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Quantity++;
            }
            _context.SaveChanges();
        }

        public void RemoveItemFromCart(Product product)
        {
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.Product.Id == product.Id && n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Quantity > 1)
                {
                    shoppingCartItem.Quantity--;
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
            ShoppingCartItems = _context.ShoppingCartItems
                .Where(n => n.ShoppingCartId == ShoppingCartId)
                .ToList();

            return ShoppingCartItems;
        }

        public async Task ClearShoppingCartAsync()
        {
            var items = await _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).ToListAsync();

            _context.ShoppingCartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }

        public decimal GetShoppingCartTotal() => _context.ShoppingCartItems
            .Where(n => n.ShoppingCartId == ShoppingCartId).Select(n => n.Product.Price * n.Quantity).Sum();
    }
}

