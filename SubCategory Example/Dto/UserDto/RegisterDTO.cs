using System.ComponentModel.DataAnnotations;

namespace Shop.Dto.UserDto
{
    public class RegisterDTO
    {

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Lastname is required")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirm Password do not match")]
        public string ConfirmPassword { get; set; }

        public string? Avatar { get; set; }
        //[Display(Name = "Avatar")]
        //[DataType(DataType.Upload)]
        //public IFormFile? AvatarImage { get; set; }


    }
}
