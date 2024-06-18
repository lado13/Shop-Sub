using Shop.Model;

namespace Shop.Interfaces
{
    public interface IUserOrderService
    {
        object UserOrders { get; }

        Task<UserOrder> GetById(int id);
        Task<IEnumerable<UserOrder>> GetAll();
        Task<UserOrder> Add(UserOrder userOrder);
        Task Delete(int id);
        Task<User> GetUserByEmailAsync(string email);
    }
}
