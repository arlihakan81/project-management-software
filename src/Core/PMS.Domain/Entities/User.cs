namespace PMS.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string? Avatar { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public bool IsEmailConfirmed { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public Guid RoleId { get; set; }
        public Guid OrganizationId { get; set; }


        public virtual Organization Organization { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Project> ManagedProjects { get; set; } = new List<Project>();


    }
}
