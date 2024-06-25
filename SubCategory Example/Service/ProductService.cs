using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Dto.ProductDto;
using Shop.Interfaces;
using Shop.Model;

namespace Shop.Service
{

    // ProductService handles the business logic for managing products, including retrieving, creating, updating, deleting, and searching products.
    public class ProductService : IProductService
    {


        //private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppDbContext _context;




        // Constructor to initialize AppDbContext and IWebHostEnvironment.
        public ProductService(AppDbContext context)
        {
            _context = context;
        }



        // Retrieves all products, including their details such as image path.
        public async Task<IEnumerable<AllProduct>> GetProducts()
        {
            try
            {
                var products = await _context.Products
                       .OrderBy(p => p.ProductId)
                       .Select(p => new AllProduct
                       {
                           Id = p.ProductId,
                           Name = p.Name,
                           Price = p.Price,
                           SubcategoryId = p.SubcategoryId,
                           ImagePath = p.Image

                       })
                       .ToListAsync();

                return products;

            }
            catch (Exception)
            {

                throw;
            }

        }





        // Retrieves a specific product by ID, including its image path.
        public async Task<ProductDTO> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return null;
            }


            var productDto = new ProductDTO
            {
                Id = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                SubcategoryId = product.SubcategoryId,
                ImagePath = product.Image
            };

            return productDto;

        }





        // Creates a new product, including saving its image if provided.
        public async Task<ProductDTO> PostProduct(ProductDTO productDto)
        {
            try
            {
                var product = new Product
                {
                    ProductId = productDto.Id,
                    Name = productDto.Name,
                    Price = productDto.Price,
                    SubcategoryId = productDto.SubcategoryId,
                    Image = productDto.ImagePath

                };



                _context.Products.Add(product);
                await _context.SaveChangesAsync();


                return productDto;
            }
            catch (Exception)
            {

                throw;
            }
        }




        // Updates an existing product by ID, including updating its image if provided.
        public async Task<bool> PutProduct(int id, ProductDTO productDto)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return false;
                }
                else
                {

                    product.Name = productDto.Name;
                    product.Price = productDto.Price;
                    product.Image = productDto.ImagePath;
                    product.SubcategoryId = productDto.SubcategoryId;
                }

                await _context.SaveChangesAsync();
                return true;



            }
            catch (Exception)
            {

                throw;
            }



            
        }







        // Deletes a product by ID.
        public async Task<bool> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }



        // Searches products by their title.
        public async Task<IEnumerable<ProductDTO>> SearchProductsByTitle(string title)
        {
            var products = await _context.Products
                                            .Where(p => p.Name.Contains(title))
                                            .Select(p => new ProductDTO
                                            {
                                                Id = p.ProductId,
                                                Name = p.Name,
                                                Price = p.Price,
                                                ImagePath = p.Image

                                            })
                                            .ToListAsync();
            return products;
        }




        // Checks if a product exists by ID.
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }



        public async Task<IEnumerable<ProductDTO>> GetProductsBySubCategory(int subcategoryId)
        {
            IEnumerable<ProductDTO> products = await _context.Products
                .Where(p => p.SubcategoryId == subcategoryId)
                .Select(p => new ProductDTO
                {
                    Id = p.ProductId,
                    Name = p.Name,
                    Price = p.Price,
                    ImagePath = p.Image,
                    SubcategoryId = p.SubcategoryId
                })
                .ToListAsync();

            return products;
        }


    }
}
