using Microsoft.EntityFrameworkCore;
using ProductApi.Application.Interfaces;
using ProductApi.Domain.Entities;
using ProductApi.Infrastructure.Persistence;
namespace ProductApi.Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<List<Category>> GetCategoriesByIdsAsync(List<int> categoryIds)
        {
            return await context.Categories
                .Where(c => categoryIds.Contains(c.Id))
                .ToListAsync();
        }
        public async Task HelpToDeleteCategoryAsync(int id)
        {
            var category = await context.Categories
                .Include(c => c.categoryProducts)
                .ThenInclude(cp => cp.product) 
                .FirstOrDefaultAsync(c => c.Id == id);

            foreach (var categoryProduct in category.categoryProducts)
            {
                var product = categoryProduct.product;

                var otherCategories = await context.CategoryProducts
                    .Where(cp => cp.ProductId == product.Id && cp.CategoryId != id)
                    .ToListAsync();

                if (otherCategories.Count() == 0)
                {
                    context.Products.Remove(product);
                }
            }

            context.CategoryProducts.RemoveRange(category.categoryProducts);

        }

    }
}