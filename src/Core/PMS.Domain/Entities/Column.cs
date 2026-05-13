using PMS.Domain.Entities.Commons;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PMS.Domain.Entities
{
    public class Column : BaseEntity
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Kolon adı boş bırakılamaz")]
        public string Name { get; set; } = string.Empty;

        [AllowNull]
        public string? Color { get; set; }

        [Required]
        public int Order { get; set; } // e.g 1000, 2000, 3000 ranged point/number 

        public bool IsArchived { get; set; } = false; // e.g the developer can want to hide issues on 'done' column

        public Guid BoardId { get; set; }

        public virtual ICollection<Issue>? Issues { get; set; }

        public virtual Board Board { get; set; } = new();


    }
}