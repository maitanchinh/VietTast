using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Vietast.Services.AuthAPI.Models;
using Vietast.Services.AuthAPI.Models.DTO;
using Vietast.Services.AuthAPI.Services.IServices;
using Vietast.Services.ProductAPI.Data;

namespace Vietast.Services.AuthAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJWTTokenGenerator _jwtTokenGenerator;
        public AuthService(AppDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJWTTokenGenerator jwtTokenGenerator)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.UserName.ToLower() == email.ToLower());
            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO request)
        {
            try
            {
                var user = _dbContext.Users.FirstOrDefault(u => u.UserName.ToLower() == request.Username.ToLower());
                var isValid = await _userManager.CheckPasswordAsync(user, request.Password);
                if (user != null && isValid)
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var token = _jwtTokenGenerator.GenerateToken(user, userRoles);
                    return new LoginResponseDTO()
                    {
                        IsSuccess = true,
                        Token = token,
                        User = new UserDTO()
                        {
                            Id = user.Id,
                            Name = user.Name,
                            Email = user.Email,
                            PhoneNumber = user.PhoneNumber,
                        }
                    };

                }
                return new LoginResponseDTO()
                {
                    IsSuccess = false,
                    Message = "Invalid Authentication",
                    Token = null,
                };
            }
            catch (Exception ex)
            {
                return new LoginResponseDTO()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponseDTO> Register(RegistrationRequestDTO request)
        {
            ApplicationUser user = new ApplicationUser()
            {
                Name = request.Name,
                Email = request.Email,
                UserName = request.Email,
                NormalizedEmail = request.Email.ToUpper(),
                PhoneNumber = request.PhoneNumber,
            };

            ResponseDTO responseDTO = new ResponseDTO();

            try
            {
                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    var userToReturn = _dbContext.Users.First(u => u.UserName == request.Email);
                    responseDTO.Result = new UserDTO()
                    {
                        Id = userToReturn.Id,
                        Name = userToReturn.Name,
                        Email = userToReturn.Email,
                        PhoneNumber = userToReturn.PhoneNumber,
                    };
                    return responseDTO;

                }
                else
                {
                    responseDTO.IsSuccess = false;
                    responseDTO.Message = result.Errors.FirstOrDefault().Description;
                    return responseDTO;
                }
            }
            catch (Exception ex)
            {
                responseDTO.IsSuccess = false;
                responseDTO.Message = ex.Message;
                return responseDTO;
            }
        }

    }
}
