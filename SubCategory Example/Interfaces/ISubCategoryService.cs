using Shop.Dto.SubCategoryDto;

namespace Shop.Interfaces
{
    public interface ISubCategoryService
    {

        Task<IEnumerable<SubcategoryDTO>> GetSubcategories();
        Task<SubcategoryDTO> GetSubcategory(int id);
        Task<SubcategoryDTO> PostSubcategory(SubcategoryDTO subcategoryDto);
        Task<bool> PutSubcategory(int id, SubcategoryDTO subcategoryDto);
        Task<bool> DeleteSubcategory(int id);
        Task<IEnumerable<SubcategoryDTO>> GetSubcategoriesByCategory(int categoryId);

    }
}
