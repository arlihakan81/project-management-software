using PMS.Application.DTO.Column;

namespace PMS.Application.DTO.Board
{
    public class BoardDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public virtual ICollection<ColumnDto> Columns { get; set; } = [];
    }
}
