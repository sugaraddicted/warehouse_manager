using System.Threading.Tasks;
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
