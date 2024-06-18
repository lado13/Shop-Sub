using Microsoft.AspNetCore.Mvc;
using Shop.Dto.SubCategoryDto;
using Shop.Interfaces;

namespace Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]



    // SubCategoryController handles CRUD operations for subcategories.
    public class SubCategoryController : ControllerBase
    {



        private readonly ISubCategoryService _subCategoryService;




        // Constructor to initialize ISubCategoryService.
        public SubCategoryController(ISubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }




        // Retrieves all subcategories.

        [HttpGet("GetAllSubCategory")]
        public async Task<ActionResult<IEnumerable<SubcategoryDTO>>> GetSubcategories()
        {
            var subcategories = await _subCategoryService.GetSubcategories();
            return Ok(subcategories);
        }




        // Retrieves a specific subcategory by ID.

        [HttpGet("GetOneSubCategory/{id}")]
        public async Task<ActionResult<SubcategoryDTO>> GetSubcategory(int id)
        {
            var subcategory = await _subCategoryService.GetSubcategory(id);
            if (subcategory == null)
            {
                return NotFound();
            }
            return Ok(subcategory);
        }



        [HttpGet("GetSubcategoriesByCategory/{categoryId}")]
        public async Task<ActionResult<IEnumerable<SubcategoryDTO>>> GetSubcategoriesByCategory(int categoryId)
        {
            var subcategories = await _subCategoryService.GetSubcategoriesByCategory(categoryId);
            if (subcategories == null || !subcategories.Any())
            {
                return NotFound();
            }
            return Ok(subcategories);
        }



        // Creates a new subcategory.

        [HttpPost("AddSubCategory")]
        public async Task<ActionResult<SubcategoryDTO>> PostSubcategory(SubcategoryDTO subcategoryDto)
        {
            var createdSubcategory = await _subCategoryService.PostSubcategory(subcategoryDto);
            return CreatedAtAction(nameof(GetSubcategory), new { id = createdSubcategory.SubcategoryId }, createdSubcategory);
        }





        // Updates an existing subcategory by ID.

        [HttpPut("UpdateSubCategory/{id}")]
        public async Task<IActionResult> PutSubcategory(int id, SubcategoryDTO subcategoryDto)
        {
            var success = await _subCategoryService.PutSubcategory(id, subcategoryDto);
            if (!success)
            {
                return NotFound();
            }
            return Ok();
        }




        // Deletes a subcategory by ID.

        [HttpDelete("DeleteSubCategory/{id}")]
        public async Task<IActionResult> DeleteSubcategory(int id)
        {
            var success = await _subCategoryService.DeleteSubcategory(id);
            if (!success)
            {
                return NotFound();
            }
            return Ok();
        }




    }
}
