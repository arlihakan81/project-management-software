using AutoMapper;
using PMS.Application.DTO.Project;
using PMS.Application.Interfaces;
using PMS.Application.Repositories.Generic;
using PMS.Domain.Entities;
using System.Linq.Expressions;

namespace PMS.Persistence.Services
{
    public class ProjectService(IRepository<Project> repository, IMapper mapper) : IProjectService
    {
        private readonly IRepository<Project> _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task AddAsync(CreateProjectDto createProjectDto)
        {
            var project = _mapper.Map<Project>(createProjectDto);
            await _repository.AddAsync(project);
        }

        public async Task<IEnumerable<ProjectDto>?> GetAllProjectsAsync(int page = 1, int offset = 10, int limit = 10, Expression<Func<ProjectDto, bool>>? filter = null)
        {
            var projects = await _repository.GetAllAsync(page, offset, limit);
            var projectDtos = _mapper.Map<IEnumerable<ProjectDto>>(projects);
            if (filter != null)
            {
                projectDtos = projectDtos.Where(filter.Compile());
            }
            return projectDtos;
        }

        public async Task<ProjectDto?> GetProjectByIdAsync(Guid id)
        {
            return await _repository.GetAsync(p => p.Id == id) is Project project ? _mapper.Map<ProjectDto>(project) : null;
        }

        public async Task UpdateAsync(Guid id, UpdateProjectDto updateProjectDto)
        {
            var project = await _repository.GetByIdAsync(id);
            if (project == null)
            {
                throw new Exception($"Project with id {id} not found");
            }
            await _repository.UpdateAsync(_mapper.Map(updateProjectDto, project));
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }



    }
}
