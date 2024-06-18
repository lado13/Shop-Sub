using Shop.Dto.CategoryDto;
using Shop.Dto.ProductDto;

namespace Shop.Interfaces
{
    public interface ICategoryService
    {

        Task<IEnumerable<CategoryDTO>> GetCategories();
        Task<CategoryDTO> GetCategory(int id);
        Task<CategoryDTO> PostCategory(CategoryCreateDTO categoryDto);
        Task<bool> PutCategory(int id, CategoryCreateDTO categoryDto);
        Task<bool> DeleteCategory(int id);
        Task<IEnumerable<CategoryDTO>> FilterByCategory(int categoryId);
        Task<IEnumerable<ProductDTO>> FilterProductsBySubcategory(int subcategoryId);

    }
}
