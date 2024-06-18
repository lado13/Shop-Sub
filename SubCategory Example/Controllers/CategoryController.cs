using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Dto.CategoryDto;
using Shop.Dto.ProductDto;
using Shop.Interfaces;

namespace Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    // CategoryController handles CRUD operations and filtering for categories.
    public class CategoryController : ControllerBase
    {



        private readonly ICategoryService _categoryService;




        // Constructor to initialize ICategoryService.
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }



        // Retrieves all categories.

        [HttpGet("GetAllCategory")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            var categories = await _categoryService.GetCategories();
            return Ok(categories);
        }




        // Retrieves a specific category by ID.

        [HttpGet("GetOneCategory/{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(int id)
        {
            var category = await _categoryService.GetCategory(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }




        // Creates a new category.

        [HttpPost("AddCategory")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<CategoryDTO>> PostCategory(CategoryCreateDTO categoryDto)
        {
            var createdCategory = await _categoryService.PostCategory(categoryDto);
            return CreatedAtAction(nameof(GetCategory), new { id = createdCategory.CategoryId }, createdCategory);
        }




        // Updates an existing category.

        [HttpPut("UpdateCategory/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutCategory(int id, CategoryCreateDTO categoryDto)
        {
            var success = await _categoryService.PutCategory(id, categoryDto);
            if (!success)
            {
                return NotFound();
            }
            return Ok();
        }



        // Deletes a category by ID.

        [HttpDelete("DeleteCategory/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(int id)
        {


            var success = await _categoryService.DeleteCategory(id);
            if (!success)
            {
                return NotFound();
            }
            return Ok("Removed successfully");



            //if (User.IsInRole("Admin"))
            //{
            //    var success = await _categoryService.DeleteCategory(id);
            //    if (!success)
            //    {
            //        return NotFound();
            //    }
            //    return Ok("Removed successfully");


            //}

            //return Ok("You are not admin!");

        }




        // Filters categories by a specific category ID.

        [HttpGet("FilterByCategory/{categoryId}")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> FilterByCategory(int categoryId)
        {
            var categories = await _categoryService.FilterByCategory(categoryId);
            return Ok(categories);
        }





        // Filters products by a specific subcategory ID.

        [HttpGet("FilterProductsBySubcategory/{subcategoryId}")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> FilterProductsBySubcategory(int subcategoryId)
        {
            var products = await _categoryService.FilterProductsBySubcategory(subcategoryId);
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }




    }




}

