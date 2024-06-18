using Shop.Dto.ProductDto;

namespace Shop.Interfaces
{
    public interface IProductService
    {

        Task<IEnumerable<AllProduct>> GetProducts();
        Task<ProductDTO> GetProduct(int id);
        Task<ProductDTO> PostProduct(ProductDTO productDto);
        Task<bool> PutProduct(int id, ProductDTO productDto);
        Task<bool> DeleteProduct(int id);
        Task<IEnumerable<ProductDTO>> SearchProductsByTitle(string title);
        Task<IEnumerable<ProductDTO>> GetProductsBySubCategory(int subcategoryId);

    }
}
