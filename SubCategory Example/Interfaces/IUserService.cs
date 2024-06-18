using Shop.Dto.UserDto;
using Shop.Model;

namespace Shop.Interfaces
{
    public interface IUserService
    {

        Task<IEnumerable<User>> GetAllUsers();
        Task<bool> RemoveUser(string userId);
        Task<bool> EditUserInfo(string userId, EditUserDTO model);

    }
}
