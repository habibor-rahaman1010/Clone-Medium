using System.ComponentModel.DataAnnotations;

namespace Medium.Application.DTO
{
    public class CategoryDto
    {
        [Required(AllowEmptyStrings = false), StringLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required(AllowEmptyStrings = false), StringLength(100)]
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
