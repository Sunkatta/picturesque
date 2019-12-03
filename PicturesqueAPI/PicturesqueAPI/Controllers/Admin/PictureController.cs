using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Picturesque.Domain;

namespace PicturesqueAPI.Controllers.Admin
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PictureController : Controller
    {
        private readonly IPictureServiceManager _pictureServiceManager;

        public PictureController(IPictureServiceManager pictureServiceManager)
        {
            _pictureServiceManager = pictureServiceManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _pictureServiceManager.GetPicturesAsync());
        }

        [HttpPost("UploadPictures")]
        public async Task<IActionResult> UploadPictures(List<IFormFile> files)
        {
            try
            {
                await _pictureServiceManager.UploadPicturesAsync(files);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}