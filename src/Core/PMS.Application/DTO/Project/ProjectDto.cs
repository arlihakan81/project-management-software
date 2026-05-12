using PMS.Application.DTO.User;
using PMS.Domain.Enums;

namespace PMS.Application.DTO.Project
{
    public class ProjectDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? Deadline { get; set; }
        public ProjectStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastUpdatedDate { get; set; }

        public virtual UserDto? Manager { get; set; }

    }
}
