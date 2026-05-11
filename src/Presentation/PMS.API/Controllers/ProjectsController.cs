using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Domain.Entities;

namespace PMS.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly List<Project> _projects = new List<Project>()
        {
            new () { Name = "Project Alpha", Description = "First project", StartDate = DateTime.Now, Status = Domain.Enums.ProjectStatus.Inactive },
            new () { Name = "Project Beta", Description = "Second project", StartDate = DateTime.Now, Status = Domain.Enums.ProjectStatus.OnGoing },
            new () { Name = "Project Teta", Description = "Third project", StartDate = DateTime.Now, Status = Domain.Enums.ProjectStatus.Cancelled },
            new () { Name = "Project Seta", Description = "Fourth project", StartDate = DateTime.Now, Status = Domain.Enums.ProjectStatus.Inactive },
            new () { Name = "Project Jeta", Description = "Fifth project", StartDate = DateTime.Now, Status = Domain.Enums.ProjectStatus.Inactive },
            new () { Name = "Project Goethe", Description = "Sixth project", StartDate = DateTime.Now, Status = Domain.Enums.ProjectStatus.Cancelled },
            new () { Name = "Project Panathinaikos", Description = "Seventh project", StartDate = DateTime.Now, Status = Domain.Enums.ProjectStatus.OnGoing},
        };


        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_projects);
        }



    }
}
