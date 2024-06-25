namespace Shop.Dto.OrderDto
{
    public class OrderInfoDto
    {
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public string Avatar { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int ProductId { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string OrderDate { get; set; }
    }
}
