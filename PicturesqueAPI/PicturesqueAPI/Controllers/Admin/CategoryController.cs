using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
    }
}