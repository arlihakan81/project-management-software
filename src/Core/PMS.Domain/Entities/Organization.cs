namespace PMS.Domain.Entities
{
    public class Organization
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Domain { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}
