using System.ComponentModel.DataAnnotations;

namespace Shop.Model
{
    public class SubCategory
    {

        [Key]
        public int SubcategoryId { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();


    }
}
