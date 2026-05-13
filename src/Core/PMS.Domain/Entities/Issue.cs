using PMS.Domain.Entities.Commons;
using PMS.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PMS.Domain.Entities
{
    public class Issue : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }

        public IssueType Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? DueDate { get; set; }

        public Guid ColumnId { get; set; }
        public Guid? UserId { get; set; }

        public virtual Column Column { get; set; }
        public virtual User? User { get; set; }





    }
}
