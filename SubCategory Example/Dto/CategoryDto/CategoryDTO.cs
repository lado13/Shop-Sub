using Shop.Dto.SubCategoryDto;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Dto.CategoryDto
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public List<SubcategoryDTO> Subcategories { get; set; }
    }
}
