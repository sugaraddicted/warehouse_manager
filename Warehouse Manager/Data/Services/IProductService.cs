using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Warehouse_Manager.Data.Base;
using Warehouse_Manager.MVVM.Model;
using Warehouse_Manager.MVVM.ViewModel;

namespace Warehouse_Manager.Data.Services
{  
    public interface IProductService : IEntityBaseRepository<Product>
    {
        Task<Product> GetProductByIdAsync(int id);
        Task AddNewProductAsync();
        Task UpdateProductAsync(ProductViewModel data);
    }
}
