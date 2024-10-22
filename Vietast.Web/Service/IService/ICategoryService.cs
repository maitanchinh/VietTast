using Vietast.Web.Models;

namespace Vietast.Web.Service.IService
{
    public interface ICategoryService
    {
        Task<ResponseDTO?> GetCategoryAsync(Guid categoryId);
        Task<ResponseDTO?> GetAllCategoriesAsync();
        Task<ResponseDTO?> CreateCategoryAsync(CategoryCreateDTO categoryCreateDTO);
        Task<ResponseDTO?> UpdateCategoryAsync(CategoryUpdateDTO categoryUpdateDTO);
        Task<ResponseDTO?> DeleteCategoryAsync(string categoryId);
    }
}
