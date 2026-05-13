using PMS.Application.DTO.Board;

namespace PMS.Application.DTO.Column
{
    public class ColumnDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Color { get; set; }
        public virtual BoardDto Board { get; set; } = new();
    }
}
