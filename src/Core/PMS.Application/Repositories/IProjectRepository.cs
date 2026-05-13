using PMS.Application.Repositories.Generic;
using PMS.Domain.Entities;

namespace PMS.Application.Repositories
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<bool> IsProjectTitleUniqueAsync(string title, Guid? projectId = null);
    }
}
