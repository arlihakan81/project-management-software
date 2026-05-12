using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.DTO.Project;
using PMS.Application.Interfaces;
using PMS.Domain.Entities;

namespace PMS.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectsController(IProjectService projectService) : ControllerBase
    {
        private readonly IProjectService _projectService = projectService;

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int offset = 10, [FromQuery] int limit = 10, [FromQuery] string? filter = null)
        {
            var projects = await _projectService.GetAllProjectsAsync(page, offset, limit, filter != null ? p => p.Name.Contains(filter) : null);
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProjectDto createProjectDto)
        {
            await _projectService.AddAsync(createProjectDto);
            return CreatedAtAction(nameof(Get), new {title = createProjectDto.Name});
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateProjectDto updateProjectDto)
        {
            try
            {
                await _projectService.UpdateAsync(id, updateProjectDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _projectService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }







    }
}
