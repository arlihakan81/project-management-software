using PMS.Domain.Enums;

namespace PMS.Application.DTO.Issue
{
    public class CreateIssueDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public IssueType Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid ColumnId { get; set; }
        public Guid? UserId { get; set; }
    }
}
