using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Warehouse_Manager.Data.Base;
using Warehouse_Manager.Dto;
using Warehouse_Manager.MVVM.Model;

namespace Warehouse_Manager.Data.Services.Interfaces
{
    public interface IProductService : IEntityBaseRepository<Product>
    {
        Task AddNewProductAsync(ProductDto data);
        Task UpdateProductAsync(ProductDto data);
    }
}
