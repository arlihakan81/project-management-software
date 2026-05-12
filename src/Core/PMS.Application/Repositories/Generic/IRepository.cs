using PMS.Domain.Entities.Commons;
using System.Linq.Expressions;

namespace PMS.Application.Repositories.Generic
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>?> GetAllAsync(int page=1, int offset=10, int limit=10, Expression<Func<T, bool>>? expression = null);
        Task<T?> GetByIdAsync(Guid id);
        Task<T?> GetAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
    }
}
