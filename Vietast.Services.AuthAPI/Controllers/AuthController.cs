using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Vietast.Services.AuthAPI.Models;
using Vietast.Services.AuthAPI.Models.DTO;
using Vietast.Services.AuthAPI.Services.IServices;

namespace Vietast.Services.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected ResponseDTO _response;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
            _response = new();
        }
        [HttpPost("register")]
        public async Task<ResponseDTO> Register([FromBody] RegistrationRequestDTO registrationRequestDTO)
        {
            var result = await _authService.Register(registrationRequestDTO);
            return result;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var result = await _authService.Login(loginRequestDTO);
            if (result != null && result.IsSuccess)
            {
                _response.IsSuccess = true;
                _response.Message = "Login successful";
                _response.Result = result;
                
            }
            else
            {
                _response.IsSuccess = false;
                _response.Message = "Login failed";
            }          
            return Ok(_response);
        }
        [HttpPost("assign-role")]
        public async Task<ResponseDTO> AssignRole([FromBody] RegistrationRequestDTO model)
        {
            var result = await _authService.AssignRole(model.Email, model.Role.ToUpper());
            if (result)
            {
                _response.IsSuccess = true;
                _response.Message = "Role assigned successfully";
                return _response;
            }
            _response.IsSuccess = false;
            _response.Message = "Role assignment failed";
            return _response;
        }
    }
}
