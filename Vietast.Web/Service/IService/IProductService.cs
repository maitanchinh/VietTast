using Vietast.Web.Models;

namespace Vietast.Web.Service.IService
{
    public interface IProductService
    {
        Task<ResponseDTO?> GetProductAsync(Guid productId);
        Task<ResponseDTO?> GetAllProductsAsync();
        Task<ResponseDTO?> CreateProductAsync(ProductCreateDTO productCreateDTO);
        Task<ResponseDTO?> UpdateProductAsync(ProductUpdateDTO productUpdateDTO);
        Task<ResponseDTO?> DeleteProductAsync(string productId);
    }
}
