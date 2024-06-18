namespace Shop.Dto.ProductDto
{
    public class AllProduct
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int SubcategoryId { get; set; }
        public string ImagePath { get; set; }

    }
}
