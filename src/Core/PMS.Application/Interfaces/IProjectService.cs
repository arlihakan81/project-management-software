using PMS.Application.DTO.Project;
using PMS.Application.Repositories.Generic;
using PMS.Domain.Entities;
using System.Linq.Expressions;

namespace PMS.Application.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDto>?> GetAllProjectsAsync(int page = 1, int offset = 10, int limit = 10, Expression<Func<ProjectDto, bool>>? filter = null);
        Task<ProjectDto?> GetProjectByIdAsync(Guid id);
        Task AddAsync(CreateProjectDto createProjectDto);
        Task UpdateAsync(Guid id, UpdateProjectDto updateProjectDto);
        Task DeleteAsync(Guid id);


    }
}
