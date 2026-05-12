using Microsoft.EntityFrameworkCore;
using PMS.Application.Repositories.Generic;
using PMS.Domain.Entities.Commons;
using PMS.Persistence.Data;
using System.Linq.Expressions;

namespace PMS.Persistence.Repositories.Generic
{
    public class Repository<T>(AppDbContext context) : IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context = context;

        public async Task AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = _context.Set<T>().Find(id) ?? throw new Exception($"{id} not found item");
            entity.IsDeleted = true;
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>?> GetAllAsync(int page = 1, int offset = 1, int limit = 10, Expression<Func<T, bool>>? expression = null)
        {
            return await _context.Set<T>()
                .Where(expression ?? (x => true))
                .Skip((page - 1) * offset)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(expression);
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
