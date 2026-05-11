namespace PMS.Domain.Entities.Commons
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public Guid CreatedById { get; set; }
        public Guid? UpdatedById { get; set; }

        public Guid OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }

        public virtual User CreatedBy { get; set; }
        public virtual User? UpdatedBy { get; set; }


    }
}
