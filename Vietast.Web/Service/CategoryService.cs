using Vietast.Web.Models;
using Vietast.Web.Service.IService;
using Vietast.Web.Utils;

namespace Vietast.Web.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IBaseService _baseService;
        public CategoryService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDTO?> CreateCategoryAsync(CategoryCreateDTO categoryCreateDTO)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = SD.ApiType.POST,
                Url = SD.ProductAPIBase + "/api/category",
                Data = categoryCreateDTO
            });
        }

        public async Task<ResponseDTO?> DeleteCategoryAsync(string categoryId)
        {
			return await _baseService.SendAsync(new RequestDTO
			{
				ApiType = SD.ApiType.DELETE,
				Url = SD.ProductAPIBase + $"/api/category/{categoryId}"
			});
		}

        public async Task<ResponseDTO?> GetAllCategoriesAsync()
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "/api/category"
            });
        }

        public async Task<ResponseDTO?> GetCategoryAsync(Guid categoryId)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "/api/category" + $"/{categoryId}"
            });
        }

        Task<ResponseDTO?> ICategoryService.UpdateCategoryAsync(CategoryUpdateDTO categoryUpdateDTO)
        {
            throw new NotImplementedException();
        }
    }
}
