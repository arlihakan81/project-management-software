using Microsoft.EntityFrameworkCore;
using PMS.Application.Repositories;
using PMS.Domain.Entities;
using PMS.Persistence.Data;
using PMS.Persistence.Repositories.Generic;
using System.Linq.Expressions;

namespace PMS.Persistence.Repositories
{
    public class ProjectRepository(AppDbContext context) : Repository<Project>(context), IProjectRepository
    {
#pragma warning disable CS9124 // Parametre yakalanıp kapsayan türün durumuna girer ve değeri bir alan, özellik veya olay başlatmak için de kullanılır.
        private readonly AppDbContext _context = context;
#pragma warning restore CS9124 // Parametre yakalanıp kapsayan türün durumuna girer ve değeri bir alan, özellik veya olay başlatmak için de kullanılır.

        public override async Task AddAsync(Project entity)
        {
            await base.AddAsync(entity);
            await _context.SaveChangesAsync();
            var newBoard = new Board()
            {
                Name = $"{entity.Name}'s Board",
                ProjectId = entity.Id,
                Type = Domain.Enums.BoardType.Kanban,
                Columns =
                    [
                        new (){ Name = "To Do", Order = 10, Color="#405491" },
                        new (){ Name = "In Progress", Order = 20, Color="#888abc" },
                        new (){ Name = "Done", Order = 30, Color = "#009217" }
                    ]
            };
            _context.Boards.Add(newBoard);
            await _context.SaveChangesAsync();
        }

        public override async Task<IEnumerable<Project>?> GetAllAsync(int page = 1, int offset = 1, int limit = 10, Expression<Func<Project, bool>>? expression = null)
        {
            return expression is null ? 
                await _context.Projects
                .Include(p => p.Manager)
                .Include(p => p.Boards)
                .ThenInclude(b => b.Columns)
                .ThenInclude(c => c.Issues)
                .Skip((page - 1) * offset)
                .Take(limit).ToListAsync() :
                
                await _context.Projects
                .Include(p => p.Manager)
                .Include(p => p.Boards)
                .ThenInclude(b => b.Columns)
                .ThenInclude(c => c.Issues)
                .Where(expression)
                .Skip((page - 1) * offset)
                .Take(limit).ToListAsync();
        }

        public async Task<bool> IsProjectTitleUniqueAsync(string title, Guid? projectId = null)
        {
            return projectId is null ?
                !await _context.Projects.AnyAsync(p => p.Name.ToLower().Trim() == title.ToLower().Trim()) : 
                !await _context.Projects.AnyAsync(p => p.Name.ToLower().Trim() == title.ToLower().Trim() && p.Id != projectId.Value);
        }
    }
}
