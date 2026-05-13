using Microsoft.EntityFrameworkCore;
using PMS.Application.Repositories;
using PMS.Domain.Entities;
using PMS.Persistence.Data;
using PMS.Persistence.Repositories.Generic;
using System.Linq.Expressions;

namespace PMS.Persistence.Repositories
{
    public class IssueRepository(AppDbContext context) : Repository<Issue>(context), IIssueRepository
    {
        private readonly AppDbContext _context = context;

        public override async Task<IEnumerable<Issue>?> GetAllAsync(int page = 1, int offset = 1, int limit = 10, Expression<Func<Issue, bool>>? expression = null)
        {
            return expression is null ?
                await _context.Issues.Include(i => i.Column).ThenInclude(c => c.Board).ThenInclude(b => b.Project)
                .Skip((page - 1) * offset)
                .Take(limit).ToListAsync() :
                await _context.Issues.Include(i => i.Column).ThenInclude(c => c.Board).ThenInclude(b => b.Project)
                .Where(expression).Skip((page - 1) * offset)
                .Take(limit).ToListAsync();
        }

        public async Task<IEnumerable<Issue>?> GetIssuesByProjectIdAsync(Guid projectId)
        {
            return await _context.Issues.Include(i => i.Column).ThenInclude(c => c.Board).ThenInclude(b => b.Project)
                .Where(i => i.Column.Board.ProjectId == projectId).ToListAsync();
        }

    }
}
