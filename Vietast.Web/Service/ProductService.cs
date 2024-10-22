using Vietast.Web.Models;
using Vietast.Web.Service.IService;
using Vietast.Web.Utils;

namespace Vietast.Web.Service
{
    public class ProductService : IProductService
    {
        private readonly IBaseService _baseService;
        public ProductService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDTO?> GetAllProductsAsync()
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "/api/product"
            });
        }

        public async Task<ResponseDTO?> GetProductAsync(Guid productId)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "/api/product" + $"/{productId}"
            });
        }
        public async Task<ResponseDTO?> CreateProductAsync(ProductCreateDTO productCreateDTO)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = SD.ApiType.POST,
                Url = SD.ProductAPIBase + "/api/product",
                Data = productCreateDTO
            });
        }

        public async Task<ResponseDTO?> UpdateProductAsync(ProductUpdateDTO productUpdateDTO)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = SD.ApiType.PUT,
                Url = SD.ProductAPIBase + "/api/product",
                Data = productUpdateDTO
            });
        }
        public async Task<ResponseDTO?> DeleteProductAsync(string productId)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ProductAPIBase + $"/api/product/{productId}"
            });
        }
    }
}
