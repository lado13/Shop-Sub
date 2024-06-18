using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.Dto.UserDto;
using Shop.Interfaces;
using Shop.Model;
using Shop.Role;
using System.Security.Claims;

namespace Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    // AccountController handles user registration, login, logout, and user management actions.
    public class AccountController : ControllerBase
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserService _userManagementService;
        private readonly IJwtToken _jwtToken;



        // Constructor to initialize UserManager, SignInManager, and IUserService.
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IUserService userManagementService, IJwtToken jwtToken)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userManagementService = userManagementService;
            _jwtToken = jwtToken;
        }










        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new User
                    {
                        Name = model.Name,
                        Lastname = model.Lastname,
                        UserName = model.Email,
                        Email = model.Email,
                        Avatar = model.Avatar,
                    };

                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {

                        await _userManager.UpdateAsync(user);


                        await _userManager.AddToRoleAsync(user, AppRole.User);
                        await _signInManager.SignInAsync(user, isPersistent: false);

                        var token = _jwtToken.GenerateJwtToken(user);

                        return Ok(new { Token = token });
                    }

                    return BadRequest(result.Errors);
                }

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during registration: {ex}");
                return StatusCode(500, "An error occurred during registration. Please try again later.");
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.UserName);
                    var token = _jwtToken.GenerateJwtToken(user);

                    return Ok(new { Token = token });
                }

                return BadRequest("Wrong password");
            }

            return BadRequest(ModelState);
        }





        // Log out the current user

        [HttpPost("LogOut")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("User logged out successfully");
        }




        // Get a list of all users

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> ShowAllUsers()
        {
            var users = await _userManagementService.GetAllUsers();
            return Ok(users);
        }




        // Delete a user by their ID

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> RemoveUser(string userId)
        {
            var success = await _userManagementService.RemoveUser(userId);
            if (success)
            {
                return Ok("User removed successfully");
            }
            return BadRequest("Error occurred while removing user");
        }






        // Edit the current user's information

        [HttpPut("EditUserInfo")]
        public async Task<IActionResult> UserInfoEdit(EditUserDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized("User ID not found in claims");
            }

            var success = await _userManagementService.EditUserInfo(userId, model);
            if (success)
            {
                return Ok("User information updated successfully");
            }
            return BadRequest("Error occurred while updating user information");
        }




    }


}
