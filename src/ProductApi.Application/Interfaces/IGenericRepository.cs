using System.Collections.Generic;
using System.Threading.Tasks;
namespace ProductApi.Application.Interfaces
{
    public interface IGenericRepository<T> where T : ProductApi.Domain.Entities.BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<int> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}