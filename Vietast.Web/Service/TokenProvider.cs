using Vietast.Web.Service.IService;
using Vietast.Web.Utils;

namespace Vietast.Web.Service
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TokenProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string? GetToken()
        {
            return _httpContextAccessor.HttpContext?.Request.Cookies[SD.TokenCookie];
        }

        public void RemoveToken()
        {
            _httpContextAccessor.HttpContext?.Response.Cookies.Delete(SD.TokenCookie);
        }

        public void SetToken(string token)
        {
            _httpContextAccessor.HttpContext?.Response.Cookies.Append(SD.TokenCookie, token);
        }
    }
}
