namespace Vietast.Web.Models
{
    public class LoginResponseDTO
    {
        public bool IsSuccess { get; set; } = true;
        public string? Message { get; set; }
        public UserDTO? User { get; set; }
        public string? Token { get; set; }
    }
}
