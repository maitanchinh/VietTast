using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Vietast.Web.Models;
using Vietast.Web.Service.IService;
using Vietast.Web.Utils;

namespace Vietast.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;
        public AuthController(IAuthService authService, ITokenProvider tokenProvider)
        {
            _authService = authService;
            _tokenProvider = tokenProvider;
        }
        public IActionResult Login()
        {
            LoginRequestDTO loginDTO = new LoginRequestDTO();
            return View(loginDTO);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDTO loginRequestDTO)
        {
            if (ModelState.IsValid)
            {
                ResponseDTO? result = await _authService.LoginAsync(loginRequestDTO);
                if (result != null && result.IsSuccess)
                {
                    LoginResponseDTO? loginResponseDTO = JsonConvert.DeserializeObject<LoginResponseDTO>(Convert.ToString(result.Result));
                    await SignInUser(loginResponseDTO);
                    _tokenProvider.SetToken(loginResponseDTO.Token);
                    TempData["success"] = "Login successfully!";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["error"] = result.Message;
                }
            }
            return View(loginRequestDTO);
        }
        public IActionResult Register()
        {
            var roleList = new List<SelectListItem>
            {
                new SelectListItem{Text = SD.RoleAdmin, Value = SD.RoleAdmin},
                new SelectListItem{Text = SD.RoleCustomer, Value = SD.RoleCustomer}
            };
            ViewBag.RoleList = roleList;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDTO registrationRequestDTO)
        {
            if (ModelState.IsValid)
            {
                ResponseDTO? response = await _authService.RegisterAsync(registrationRequestDTO);
                response = await _authService.AssignRoleAsync(registrationRequestDTO);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Registration successfully";
                }
                else
                {
                    TempData["error"] = response?.Message;
                    var roleList = new List<SelectListItem>
                {
                    new SelectListItem{Text = SD.RoleAdmin, Value = SD.RoleAdmin},
                    new SelectListItem{Text = SD.RoleCustomer, Value = SD.RoleCustomer}
                };
                    ViewBag.RoleList = roleList;
                }
            }
            return View(registrationRequestDTO);
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            _tokenProvider.RemoveToken();
            return RedirectToAction("Index", "Home");
        }
        private async Task SignInUser(LoginResponseDTO model)
        {
            var hanler = new JwtSecurityTokenHandler();
            var token = hanler.ReadJwtToken(model.Token);
            var identity = new ClaimsIdentity(token.Claims, CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, token.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, token.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name, token.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));

            identity.AddClaim(new Claim(ClaimTypes.Name, token.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));

            var principle = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principle);
        }
    }
}
