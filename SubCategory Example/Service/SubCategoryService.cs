using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Dto.SubCategoryDto;
using Shop.Interfaces;
using Shop.Model;

namespace Shop.Service
{

    // SubCategoryService handles the business logic for managing subcategories, including retrieving, creating, updating, and deleting subcategories.
    public class SubCategoryService : ISubCategoryService
    {



        private readonly AppDbContext _context;



        // Constructor to initialize AppDbContext.
        public SubCategoryService(AppDbContext context)
        {
            _context = context;
        }




        // Retrieves all subcategories, including their associated products.
        public async Task<IEnumerable<SubcategoryDTO>> GetSubcategories()
        {
            try
            {
                var subcategories = await _context.Subcategories
                    .Include(s => s.Products)
                    .ToListAsync();

                var subcategoryDTOs = subcategories.Select(s => new SubcategoryDTO
                {
                    SubcategoryId = s.SubcategoryId,
                    Name = s.Name,
                    CategoryId = s.CategoryId,

                });

                return subcategoryDTOs;
            }
            catch (Exception)
            {
                throw;
            }
        }






        // Retrieves a specific subcategory by ID.
        public async Task<SubcategoryDTO> GetSubcategory(int id)
        {
            var subcategory = await _context.Subcategories.FindAsync(id);
            if (subcategory == null)
            {
                return null;
            }

            var subCategoryDto = new SubcategoryDTO
            {
                SubcategoryId = subcategory.SubcategoryId,
                Name = subcategory.Name,
                CategoryId = subcategory.CategoryId
            };

            return subCategoryDto;
        }





        // Creates a new subcategory.
        public async Task<SubcategoryDTO> PostSubcategory(SubcategoryDTO subcategoryDto)
        {
            var subcategory = new SubCategory
            {
                Name = subcategoryDto.Name,
                CategoryId = subcategoryDto.CategoryId
            };

            _context.Subcategories.Add(subcategory);
            await _context.SaveChangesAsync();

            subcategoryDto.SubcategoryId = subcategory.SubcategoryId;
            return subcategoryDto;
        }





        // Updates an existing subcategory by ID.
        public async Task<bool> PutSubcategory(int id, SubcategoryDTO subcategoryDto)
        {
            if (id != subcategoryDto.SubcategoryId)
            {
                return false;
            }

            var subcategory = await _context.Subcategories.FindAsync(id);
            if (subcategory == null)
            {
                return false;
            }

            subcategory.Name = subcategoryDto.Name;
            subcategory.CategoryId = subcategoryDto.CategoryId;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubcategoryExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }






        // Deletes a subcategory by ID.
        public async Task<bool> DeleteSubcategory(int id)
        {
            var subcategory = await _context.Subcategories.FindAsync(id);
            if (subcategory == null)
            {
                return false;
            }

            _context.Subcategories.Remove(subcategory);
            await _context.SaveChangesAsync();
            return true;
        }




        // Checks if a subcategory exists by ID.
        private bool SubcategoryExists(int id)
        {
            return _context.Subcategories.Any(e => e.SubcategoryId == id);
        }


        public async Task<IEnumerable<SubcategoryDTO>> GetSubcategoriesByCategory(int categoryId)
        {
            // Implement logic to retrieve subcategories based on the provided categoryId
            // This might involve accessing a database or other data source

            // Example (replace with your actual implementation):
            IEnumerable<SubcategoryDTO> subcategories = await _context.Subcategories
                .Where(s => s.CategoryId == categoryId)
                .Select(s => new SubcategoryDTO
                {
                    SubcategoryId = s.SubcategoryId,
                    Name = s.Name,
                    CategoryId = s.CategoryId
                })
                .ToListAsync();

            return subcategories;
        }


    }
}
