using Vietast.Web.Models;
using Vietast.Web.Service.IService;
using Vietast.Web.Utils;

namespace Vietast.Web.Service
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;
        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public Task<ResponseDTO?> AssignRoleAsync(RegistrationRequestDTO registrationRequestDTO)
        {
            return _baseService.SendAsync(new RequestDTO
            {
                ApiType = SD.ApiType.POST,
                Url = SD.AuthAPIBase + "/api/auth/assign-role",
                Data = registrationRequestDTO
            }, withBearer: false);
        }

        public Task<ResponseDTO?> LoginAsync(LoginRequestDTO loginRequestDTO)
        {
            return _baseService.SendAsync(new RequestDTO
            {
                ApiType = SD.ApiType.POST,
                Url = SD.AuthAPIBase + "/api/auth/login",
                Data = loginRequestDTO
            }, withBearer: false);
        }

        public async Task<ResponseDTO?> RegisterAsync(RegistrationRequestDTO registrationRequestDTO)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = SD.ApiType.POST,
                Url = SD.AuthAPIBase + "/api/auth/register",
                Data = registrationRequestDTO
            }, withBearer: false);

        }
    }
}
