using AutoMapper;
using PMS.Application.DTO.Issue;
using PMS.Application.Interfaces;
using PMS.Application.Repositories;
using PMS.Domain.Entities;
using System.Linq.Expressions;

namespace PMS.Persistence.Services
{
    public class IssueService(IIssueRepository issueRepository, IMapper mapper) : IIssueService
    {
        private readonly IIssueRepository _issueRepository = issueRepository;
        private readonly IMapper _mapper = mapper;

        public async Task AddAsync(CreateIssueDto createIssueDto)
        {
            var issue = _mapper.Map<Issue>(createIssueDto);
            await _issueRepository.AddAsync(issue);
        }

        public async Task<IEnumerable<IssueDto>?> GetAllIssuesAsync(int page = 1, int offset = 10, int limit = 10, Expression<Func<IssueDto, bool>>? expression = null)
        {
            var issues = await _issueRepository.GetAllAsync(page, offset, limit);
            var result = _mapper.Map<IEnumerable<IssueDto>>(issues);
            if(expression is not null)
            {
                result = result.Where(expression.Compile());
            }
            return issues is null ? [] : result;
        }

        public async Task<IssueDto?> GetIssueByIdAsync(Guid id)
        {
            var issue = await _issueRepository.GetByIdAsync(id);
            if (issue is null)
                return null;
            return _mapper.Map<IssueDto>(issue);
        }

        public async Task<IEnumerable<IssueDto>?> GetIssuesByProjectIdAsync(Guid projectId)
        {
            var issues = await _issueRepository.GetIssuesByProjectIdAsync(projectId);
            return _mapper.Map<IEnumerable<IssueDto>>(issues);
        }
    }
}
