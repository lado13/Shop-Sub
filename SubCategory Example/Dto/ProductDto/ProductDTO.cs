using System.ComponentModel.DataAnnotations;

namespace Shop.Dto.ProductDto
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int SubcategoryId { get; set; }
        public string? ImagePath { get; set; }

        //[Display(Name = "Image")]
        //[DataType(DataType.Upload)]
        //public IFormFile Image { get; set; }


    }
}
