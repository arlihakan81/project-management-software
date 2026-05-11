using PMS.Domain.Entities.Commons;
using PMS.Domain.Enums;

namespace PMS.Domain.Entities
{
    public class Project : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? Deadline { get; set; }
        public ProjectStatus Status { get; set; }
        public bool IsArchived { get; set; } = false;
        public Guid? ManagerId { get; set; }
        public virtual User? Manager { get; set; }
    }
}
