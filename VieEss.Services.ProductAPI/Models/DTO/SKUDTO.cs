namespace VietTast.Services.ProductAPI.Models.DTO
{
    public class SKUDTO
    {
        public required Guid SKUId { get; set; }
        public required Guid ProductId { get; set; }
        public required string ImageUrl { get; set; }
        public required int Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
    public class SKUCreateDTO
    {
        public required Guid ProductId { get; set; }
        public required string ImageUrl { get; set; }
        public required int Price { get; set; }
    }
    public class SKUUpdateDTO
    {
        public required Guid SKUId { get; set; }
        public required Guid ProductId { get; set; }
        public required string ImageUrl { get; set; }
        public required int Price { get; set; }
    }
}
