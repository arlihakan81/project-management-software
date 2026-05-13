using PMS.Application.Repositories.Generic;
using PMS.Domain.Entities;

namespace PMS.Application.Repositories
{
    public interface IIssueRepository : IRepository<Issue>
    {
        Task<IEnumerable<Issue>?> GetIssuesByProjectIdAsync(Guid projectId);
    }
}
