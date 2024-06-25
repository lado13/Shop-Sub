using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Dto.OrderDto;
using Shop.Interfaces;
using Shop.Model;
using System.Security.Claims;

namespace Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOrderController : ControllerBase
    {

        private readonly IUserOrderService _userOrder;
        private readonly AppDbContext _context;

        public UserOrderController(IUserOrderService userOrder, AppDbContext context)
        {
            _userOrder = userOrder;
            _context = context;
        }


        //Returns all customer orders

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var orders = await _context.UserOrders
                    .Include(o => o.Users)
                    .Include(o => o.Products)
                    .Select(o => new OrderInfoDto
                    {
                        OrderId = o.Id,
                        UserId = o.UserId,
                        Avatar = o.Users.Avatar,
                        FirstName = o.Users.Name,
                        LastName = o.Users.Lastname,
                        Email = o.Users.Email,
                        //Role = o.Users.Role,
                        ProductId = o.ProductId,
                        Image = o.Products.Image,
                        Title = o.Products.Name,
                  
                        Price = o.Products.Price,
                        OrderDate = o.OrderDate.ToLongDateString(),

                    }).ToListAsync();

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving orders: {ex.Message}");
            }
        }




        //Retrieves a specific single customer order by id

        [HttpGet("Get")]
        public async Task<IActionResult> GetOrder(int id)
        {
            try
            {
                var order = await _context.UserOrders
                    .Include(o => o.Users)
                    .Include(o => o.Products)
                    .FirstOrDefaultAsync(o => o.Id == id);

                if (order == null)
                {
                    return NotFound();
                }

                var orderDto = new OrderInfoDto
                {
                    OrderId = order.Id,
                    UserId = order.UserId,
                    Avatar = order.Users.Avatar,
                    FirstName = order.Users.Name,
                    LastName = order.Users.Lastname,
                    Email = order.Users.Email,
                    ProductId = order.ProductId,
                    Image = order.Products.Image,
                    Title = order.Products.Name,
                    Price = order.Products.Price,
                    OrderDate = order.OrderDate.ToShortDateString()
                };

                return Ok(orderDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving order: {ex.Message}");
            }
        }




        //This method sends the order to the database

        [HttpPost("Upload")]
        public async Task<ActionResult<UserOrder>> CreateOrder(OrderDto orderDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized("User ID not found in claims");
            }

            foreach (var productId in orderDTO.ProductIds)
            {
                var product = await _context.Products.FindAsync(productId);

                if (product == null)
                {
                    return NotFound($"Product with ID {productId} not found.");
                }

                var order = new UserOrder
                {
                    UserId = userId,
                    ProductId = productId,
                    OrderDate = orderDTO.OrderDate
                };

                _context.UserOrders.Add(order);
            }

            await _context.SaveChangesAsync();

            return Ok(orderDTO);
        }


        //Deletes a customer order from the database

        [HttpDelete("DeleteOrderById")]
        public async Task<IActionResult> RemoveOrder(int id)
        {
            await _userOrder.Delete(id);
            return NoContent();
        }







    }
}
