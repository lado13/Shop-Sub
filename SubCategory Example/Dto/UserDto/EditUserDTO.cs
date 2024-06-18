using System.ComponentModel.DataAnnotations;

namespace Shop.Dto.UserDto
{
    public class EditUserDTO
    {
        public string? Name { get; set; }
        public string? Avatar { get; set; }

        //[Display(Name = "Avatar")]
        //[DataType(DataType.Upload)]
        //public IFormFile? AvatarImage { get; set; }

    }
}
