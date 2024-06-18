using System.ComponentModel.DataAnnotations;

namespace Shop.Dto.SubCategoryDto
{
    public class SubcategoryDTO
    {

        public int SubcategoryId { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
    }
}
