using System.Text.Json.Serialization;

namespace Vietast.Services.ProductAPI.Models.DTO
{
    public class ProductDTO
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public CategoryDTO? Category { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsActive { get; set; }
    }
    public class  ProductCreateDTO
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required Guid? CategoryId { get; set; }
        public required IFormFile Image { get; set; }
    }
    public class ProductUpdateDTO
    {
        public required Guid ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
    }
    public class ProductFilterDTO
    {
        public string? Name { get; set; }
        public Guid? CategoryId { get; set; }
        public bool? IsActive { get; set; }
    }
}
