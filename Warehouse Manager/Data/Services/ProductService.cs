using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Markup;
using Warehouse_Manager.Data.Base;
using Warehouse_Manager.Data.Services.Interfaces;
using Warehouse_Manager.Dto;
using Warehouse_Manager.MVVM.Model;

namespace Warehouse_Manager.Data.Services
{
    public class ProductService : EntityBaseRepository<Product>, IProductService
    {
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewProductAsync(ProductDto data)
        {
            var newProduct = new Product()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                StockQuantity = data.StockQuantity,
                BinaryContent = data.BinaryContent,
                FileType = ".jpg",
                UploadDate = DateTime.Now,
            };

            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(ProductDto data)
        {
            var dbProduct = await _context.Products.FirstOrDefaultAsync(m => m.Id == data.Id);

            if (dbProduct != null)
            {
                dbProduct.Name = data.Name;
                dbProduct.Description = data.Description;
                dbProduct.Price = data.Price;
                dbProduct.StockQuantity = data.StockQuantity;
                dbProduct.BinaryContent = data.BinaryContent;
                dbProduct.ModificationDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }
    }
}
