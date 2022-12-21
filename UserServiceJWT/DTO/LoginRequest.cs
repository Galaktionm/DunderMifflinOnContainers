using System.ComponentModel.DataAnnotations;

namespace UserServiceJWT.DTO
{

    public class LoginRequest
    {
        [Required(ErrorMessage = "Email can not be empty.")]
        public string email { get; set; } = null!;
        [Required(ErrorMessage = "Password can not be empty.")]
        public string password { get; set; } = null!;
    }

}