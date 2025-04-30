using System.ComponentModel.DataAnnotations;

namespace Medium.Web.Areas.Admin.Models.Category
{
    public class CategoryUpdateModel
    {
        public Guid Id { get; set; }
        [Required(AllowEmptyStrings = false), StringLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required(AllowEmptyStrings = false), StringLength(100)]
        public string Description { get; set; } = string.Empty;
        public DateTime? UpdatedDate { get; set; }
    }
}
