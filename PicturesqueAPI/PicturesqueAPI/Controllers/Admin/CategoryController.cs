using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Picturesque.Application;
using Picturesque.Common;
using Picturesque.Domain;

namespace PicturesqueAPI.Controllers.Admin
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CategoryController : Controller
    {
        private readonly ICategoryServiceManager _categoryManager;

        public CategoryController(ICategoryServiceManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _categoryManager.GetCategoriesAsync());
        }

        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] CreateCategoryEntry entry)
        {
            try
            {
                Category category = new Category(entry.Name);

                await _categoryManager.CreateCategoryAsync(category);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory([FromBody] DeleteCategoryEntry entry)
        {
            try
            {
                Category category = await _categoryManager.GetRawCategoryById(entry.Id);
                await _categoryManager.DeleteCategoryAsync(category);

                return Ok(await _categoryManager.GetCategoriesAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory([FromBody] CreateCategoryEntry entry)
        {
            try
            {
                Category category =
                    await _categoryManager.GetRawCategoryById(entry.Id);
                if (category == null)
                    return NotFound();

                category =
                    new Category(
                        entry.Name ?? category.Name,
                        new CustomId(new Guid(category.Id))
                        );

                await _categoryManager.UpdateCategoryAsync(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}