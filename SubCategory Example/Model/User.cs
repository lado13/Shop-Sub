using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Shop.Model
{
    public class User : IdentityUser
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string? Avatar { get; set; }
        List<UserOrder> Order { get; set; }    

    }
}
