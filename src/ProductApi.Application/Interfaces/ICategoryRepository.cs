using ProductApi.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace ProductApi.Application.Interfaces
{
    public interface ICategoryRepository:IGenericRepository<Category>
    {
        Task<List<Category>> GetCategoriesByIdsAsync(List<int> categoryIds);
        Task HelpToDeleteCategoryAsync(int id);
    }
}