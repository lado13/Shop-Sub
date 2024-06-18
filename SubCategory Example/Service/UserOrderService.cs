using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Interfaces;
using Shop.Model;

namespace Shop.Service
{
    public class UserOrderService : IUserOrderService
    {
        private readonly AppDbContext _context;

        public UserOrderService(AppDbContext context)
        {
            _context = context;
        }

        public object UserOrders => throw new NotImplementedException();

        public async Task<UserOrder> Add(UserOrder userOrder)
        {
            try
            {
                _context.UserOrders.Add(userOrder);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the Order.", ex);
            }
            return userOrder;
        }

        public async Task Delete(int id)
        {
            try
            {
                var item = await _context.UserOrders.FindAsync(id);
                if (item != null)
                {
                    _context.UserOrders.Remove(item);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the order.", ex);
            }
        }

        public async Task<IEnumerable<Model.UserOrder>> GetAll()
        {
            try
            {
                return await _context.UserOrders.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all orders.", ex);
            }
        }

        public async Task<Model.UserOrder> GetById(int id)
        {
            try
            {
                return await _context.UserOrders.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving order with ID {id}.", ex);
            }
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving user with email {email}.", ex);
            }
        }
    }
}
