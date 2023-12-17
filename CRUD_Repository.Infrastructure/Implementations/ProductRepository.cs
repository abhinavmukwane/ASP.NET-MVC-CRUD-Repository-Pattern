using CRUD_Repository.Core;
using CRUD_Repository.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Repository.Infrastructure.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            var products = await _context.Product.ToListAsync();
            return products;
        }

        public async Task Add(Product model)
        {
            await _context.Product.AddAsync(model);
            await Save();
        }

        public async Task<Product> GetById(int id)
        {
            return await _context.Product.FindAsync(id);
        }

        public async Task Update(Product model)
        {
            var product = await _context.Product.FindAsync(model.ProductID);
            if (product != null)
            {
                product.ProductName = model.ProductName;
                product.Price = model.Price;
                product.Qty = model.Qty;
                _context.Update(product);
                await Save();
            }
        }

        public async Task Delete(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
                await Save();
            }
        }

        private async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
