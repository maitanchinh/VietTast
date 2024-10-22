using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vietast.Services.ProductAPI.Models
{
    public class SKU
    {
        [Key]
        public required Guid SKUId { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public string ImageUrl { get; set; }
        public int Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
