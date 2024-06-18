using Microsoft.AspNetCore.Mvc;
using Shop.Dto.ProductDto;
using Shop.Interfaces;

namespace Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    // ProductController handles CRUD operations and searching for products.
    public class ProductController : ControllerBase
    {




        private readonly IProductService _productService;



        // Constructor to initialize IProductService.
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }




        // Retrieves all products.

        [HttpGet("GetAllProduct")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            var products = await _productService.GetProducts();
            return Ok(products);
        }




        // Retrieves a specific product by ID.

        [HttpGet("GetOneProduct{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            var product = await _productService.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }




        // Creates a new product.

        [HttpPost("AddProduct")]
        public async Task<ActionResult<ProductDTO>> PostProduct(ProductDTO productDto)
        {
            var createdProduct = await _productService.PostProduct(productDto);
            return Ok(createdProduct);
        }




        // Updates an existing product by ID.

        [HttpPut("PutProduct/{id}")]
        public async Task<IActionResult> PutProduct(int id, [FromQuery] ProductDTO productDto)
        {
            var success = await _productService.PutProduct(id, productDto);
            if (success)
            {
                return Ok("Product updated successfully");
            }
            else
            {
                return NotFound("Product not found or failed to update");
            }
        }




        // Deletes a product by ID.

        [HttpDelete("DeleteProduct{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var success = await _productService.DeleteProduct(id);
            if (!success)
            {
                return NotFound();
            }
            return Ok();
        }




        // Searches for products by title.

        [HttpGet("SearchProductByTitle")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> SearchProductsByTitle([FromQuery] string title)
        {

            var products = await _productService.SearchProductsByTitle(title);
            return Ok(products);

        }

        [HttpGet("GetProductsBySubCategory/{subcategoryId}")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsBySubCategory(int subcategoryId)
        {
            var products = await _productService.GetProductsBySubCategory(subcategoryId);
            if (products == null || !products.Any())
            {
                return NotFound();
            }
            return Ok(products);
        }






    }
}
