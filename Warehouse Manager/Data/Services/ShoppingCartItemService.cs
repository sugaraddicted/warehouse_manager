
using Warehouse_Manager.Data.Base;
using Warehouse_Manager.Data.Services.Interfaces;
using Warehouse_Manager.MVVM.Model;

namespace Warehouse_Manager.Data.Services
{
    public class ShoppingCartItemService : EntityBaseRepository<ShoppingCartItem>, IShoppingCartItemService
    {
        private readonly AppDbContext _context;
        public ShoppingCartItemService(AppDbContext context) : base(context)
        {
        }
    }
}
