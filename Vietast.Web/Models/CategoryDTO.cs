namespace Vietast.Web.Models
{
    public class CategoryDTO
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
    }
    public class CategoryCreateDTO
    {
        public required string Name { get; set; }
    }
    public class CategoryUpdateDTO
    {
        public required Guid CategoryId { get; set; }
        public required string Name { get; set; }
    }
}
