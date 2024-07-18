using ProductApi.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace ProductApi.Application.Interfaces
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsByUserIdAsync(string userId);
    }
}