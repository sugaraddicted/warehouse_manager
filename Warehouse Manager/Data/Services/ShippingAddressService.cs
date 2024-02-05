using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Manager.Data.Base;
using Warehouse_Manager.Data.Services.Interfaces;
using Warehouse_Manager.MVVM.Model;

namespace Warehouse_Manager.Data.Services
{
    public class ShippingAddressService : EntityBaseRepository<ShippingAddress>, IShippingAddressService
    {
        private readonly AppDbContext _context;
        public ShippingAddressService(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
