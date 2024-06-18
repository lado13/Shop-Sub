using System.ComponentModel.DataAnnotations;

namespace Shop.Model
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public List<SubCategory> Subcategories { get; set; } = new List<SubCategory>();


    }
}
