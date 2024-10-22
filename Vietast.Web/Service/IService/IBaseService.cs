using Vietast.Web.Models;

namespace Vietast.Web.Service.IService
{
    public interface IBaseService
    {
        Task<ResponseDTO?> SendAsync(RequestDTO requestDTO);
    }
}
