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
            return await context.Products.Include(p => p.categoryProducts)
                                .ThenInclude(cp => cp.category).ToListAsync();
        }
        override public async Task<Product> GetByIdAsync(int id)
        {
            return await context.Products.Include(p => p.categoryProducts).
                ThenInclude(cp => cp.category).FirstOrDefaultAsync(x => x.Id == id);

        }
        override public async Task UpdateAsync(Product product)
        {
            context.Entry(product).State = EntityState.Modified;
        }
        public async Task<IEnumerable<Product>> GetProductsByUserIdAsync(string userId)
        {
            return await context.Products.Include(p => p.categoryProducts).
                ThenInclude(cp => cp.category).Where(p => p.UserId == userId).ToListAsync();
        }
        public async Task<Product> HelpToUpdateAsync(int id)
        {
            return await context.Products
                        .Include(p => p.categoryProducts)
                            .FirstOrDefaultAsync(p => p.Id == id);
        }
        public override async Task DeleteAsync(int id)
        {
            var product = await HelpToUpdateAsync(id);
            context.CategoryProducts.RemoveRange(product.categoryProducts);
            context.Products.Remove(product);
        }
        public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            var products = await context.CategoryProducts
                .Include(cp => cp.product) 
                    .ThenInclude(p => p.categoryProducts)
                        .ThenInclude(cp => cp.category)
                    .       Where(cp => cp.CategoryId == categoryId)
                    .           Select(cp => cp.product) 
                .                   ToListAsync();
            return products;
        }
    }
}