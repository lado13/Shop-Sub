using System.ComponentModel.DataAnnotations;

namespace Shop.Model
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int SubcategoryId { get; set; }
        public SubCategory Subcategory { get; set; }
        public string Image { get; set; }
        List<UserOrder> Order { get; set; }

    }
}
