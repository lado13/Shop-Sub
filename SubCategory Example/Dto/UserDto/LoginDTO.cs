using System.ComponentModel.DataAnnotations;

namespace Shop.Dto.UserDto
{
    public class LoginDTO
    {

        [Required(ErrorMessage = "Username is required.")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }

    }
}
