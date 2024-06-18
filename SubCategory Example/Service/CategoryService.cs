using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Dto.CategoryDto;
using Shop.Dto.ProductDto;
using Shop.Interfaces;
using Shop.Dto.SubCategoryDto;
using Shop.Model;

namespace Shop.Service
{

    // CategoryService handles the business logic for managing categories and their related subcategories and products.
    public class CategoryService : ICategoryService
    {


        private readonly AppDbContext _context;



        // Constructor to initialize AppDbContext.
        public CategoryService(AppDbContext context)
        {
            _context = context;
        }



        // Retrieves all categories including their subcategories.
        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            try
            {
                var categories = await _context.Categories
                    .Include(c => c.Subcategories) // Include subcategories
                    .OrderBy(c => c.CategoryId)
                    .Select(c => new CategoryDTO
                    {
                        CategoryId = c.CategoryId,
                        Name = c.Name,
                        Subcategories = c.Subcategories.Select(s => new SubcategoryDTO
                        {
                            SubcategoryId = s.SubcategoryId,
                            Name = s.Name,
                            CategoryId = s.CategoryId,

                        }).ToList()
                    })
                    .ToListAsync();

                return categories;
            }
            catch (Exception)
            {
                throw;
            }
        }






        // Retrieves a specific category by ID, including its subcategories.
        public async Task<CategoryDTO> GetCategory(int id)
        {
            try
            {
                var category = await _context.Categories
                    .Include(c => c.Subcategories)
                    .FirstOrDefaultAsync(c => c.CategoryId == id);

                if (category == null)
                {
                    return null;
                }

                var categoryDto = new CategoryDTO
                {
                    CategoryId = category.CategoryId,
                    Name = category.Name,
                    Subcategories = category.Subcategories.Select(s => new SubcategoryDTO
                    {
                        SubcategoryId = s.SubcategoryId,
                        Name = s.Name
                    }).ToList()
                };

                return categoryDto;
            }
            catch (Exception)
            {
                throw;
            }
        }





        // Creates a new category.
        public async Task<CategoryDTO> PostCategory(CategoryCreateDTO categoryDto)
        {
            var category = new Category
            {
                Name = categoryDto.Name,
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return new CategoryDTO
            {
                CategoryId = category.CategoryId,
                Name = category.Name
            };
        }






        // Updates an existing category by ID.
        public async Task<bool> PutCategory(int id, CategoryCreateDTO categoryDto)
        {

            try
            {
                var category = await _context.Categories.Include(c => c.Subcategories).FirstOrDefaultAsync(c => c.CategoryId == id);
                if (category == null)
                {
                    return false;
                }

                category.Name = categoryDto.Name;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }






        // Deletes a category by ID.
        public async Task<bool> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return false;
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return true;
        }






        // Filters categories by a specific category ID.
        public async Task<IEnumerable<CategoryDTO>> FilterByCategory(int categoryId)
        {
            var categories = await _context.Categories
                .Where(c => c.CategoryId == categoryId)
                .Include(c => c.Subcategories)
                .ToListAsync();

            return categories.Select(c => new CategoryDTO
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Subcategories = c.Subcategories.Select(s => new SubcategoryDTO
                {
                    SubcategoryId = s.SubcategoryId,
                    Name = s.Name,
                    CategoryId = s.CategoryId
                }).ToList()
            }).ToList();
        }






        // Filters products by a specific subcategory ID.
        public async Task<IEnumerable<ProductDTO>> FilterProductsBySubcategory(int subcategoryId)
        {
            var subcategory = await _context.Subcategories.FindAsync(subcategoryId);
            if (subcategory == null)
            {
                return null;
            }

            return await _context.Products
                .Where(p => p.SubcategoryId == subcategoryId)
                .Select(p => new ProductDTO
                {
                    Id = p.ProductId,
                    Name = p.Name,
                    Price = p.Price,
                    SubcategoryId = p.SubcategoryId
                })
                .ToListAsync();
        }





        // Checks if a category exists by ID.
        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryId == id);
        }



    }
}
