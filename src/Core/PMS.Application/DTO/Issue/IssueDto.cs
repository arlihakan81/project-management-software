using PMS.Application.DTO.Column;
using PMS.Application.DTO.User;
using PMS.Domain.Enums;

namespace PMS.Application.DTO.Issue
{
    public class IssueDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public IssueType Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? DueDate { get; set; }

        public virtual ColumnDto Column { get; set; } = new();
        public virtual UserDto? User { get; set; }
    }
}
