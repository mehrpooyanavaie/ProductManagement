using Microsoft.EntityFrameworkCore;
using ProductApi.Domain.Entities;
using ProductApi.Application.Interfaces;
using ProductApi.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductApi.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        override public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await context.Products.ToListAsync();
        }
        override public async Task<Product> GetByIdAsync(int id)
        {
            return await context.Products.FirstOrDefaultAsync(x => x.Id == id);
        }
        override public async Task UpdateAsync(Product product)
        {
            context.Entry(product).State = EntityState.Modified;
        }
        public async Task<IEnumerable<Product>> GetProductsByUserIdAsync(string userId)
        {
            return await context.Products.Where(p => p.UserId == userId).ToListAsync();
        }

    }
}