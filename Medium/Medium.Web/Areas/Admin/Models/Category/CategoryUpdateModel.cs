namespace Medium.Web.Areas.Admin.Models.Category
{
    public class CategoryUpdateModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
