using PMS.Application.DTO.Issue;
using System.Linq.Expressions;

namespace PMS.Application.Interfaces
{
    public interface IIssueService
    {
        Task<IEnumerable<IssueDto>?> GetAllIssuesAsync(int page = 1, int offset = 10, int limit = 10, Expression<Func<IssueDto, bool>>? expression = null);
        Task<IssueDto?> GetIssueByIdAsync(Guid id);
        Task<IEnumerable<IssueDto>?> GetIssuesByProjectIdAsync(Guid projectId);

        Task AddAsync(CreateIssueDto createIssueDto);
    }
}
