using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.Dto.UserDto;
using Shop.Interfaces;
using Shop.Model;

namespace Shop.Service
{

    // UserService handles the business logic for managing users, including editing user information, retrieving all users, and removing users.
    public class UserService : IUserService
    {


        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _environment;



        // Constructor to initialize UserManager and IWebHostEnvironment.
        public UserService(UserManager<User> userManager, IWebHostEnvironment environment)
        {
            _userManager = userManager;
            _environment = environment;
        }





        // Edits user information, including updating the user's name and avatar image.
        public async Task<bool> EditUserInfo(string userId, EditUserDTO model)
        {

            // Find the user by ID.
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false;
            }


            // Update user's name.
            user.Name = model.Name;
            user.Avatar = model.Avatar; 


            // If a new avatar image is provided, save it.
            

            // Update the user in the database.
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }




        // Retrieves all users.
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userManager.Users.ToListAsync();
        }


        // Removes a user by ID.
        public async Task<bool> RemoveUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                return result.Succeeded;
            }
            return false;
        }






    }
}
