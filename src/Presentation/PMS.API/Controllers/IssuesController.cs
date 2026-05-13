using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.DTO.Issue;
using PMS.Application.Interfaces;
using System.Linq.Expressions;

namespace PMS.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class IssuesController(IIssueService issueService) : ControllerBase
    {
        private readonly IIssueService _issueService = issueService;

        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int offset = 10, int limit = 10, string? filter = null) 
            => Ok(await _issueService.GetAllIssuesAsync(page, offset, limit, filter != null ? p => p.Title.Contains(filter) : null));

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id) => Ok(await _issueService.GetIssueByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateIssueDto createIssueDto)
        {
            try
            {
                await _issueService.AddAsync(createIssueDto);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException);
            }
        }







    }
}
