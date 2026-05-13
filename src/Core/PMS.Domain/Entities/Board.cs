using PMS.Domain.Entities.Commons;
using PMS.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PMS.Domain.Entities
{
    public class Board : BaseEntity
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pano adı Boş bırakılamaz")]
        public string Name { get; set; } = string.Empty;

        public BoardType Type { get; set; }

        [AllowNull]
        public string? Description { get; set; }

        public bool IsArchived { get; set; } = false;
        public Guid ProjectId { get; set; }

        public virtual ICollection<Column> Columns { get; set; }
        public virtual Project Project { get; set; }






    }
}
