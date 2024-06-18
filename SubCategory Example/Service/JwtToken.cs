using Microsoft.IdentityModel.Tokens;
using Shop.Interfaces;
using Shop.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Shop.Service
{
    public class JwtToken : IJwtToken
    {

        private readonly IConfiguration _configuration;


        public JwtToken(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken(User user)
        {

            // Retrieve user information from your user store
            var userName = user.UserName;
            var userImageUrl = user.Avatar; // Assume you have an imageurl property

            var claims = new[]
            {
                 new Claim(JwtRegisteredClaimNames.Sub, userName),
                 new Claim("Image", userImageUrl), // Custom claim for the image URL
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

            };
   

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);



        }
    }
}
