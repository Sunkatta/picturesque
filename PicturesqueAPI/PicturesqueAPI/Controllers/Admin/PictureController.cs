using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Picturesque.Application;
using Picturesque.Domain;

namespace PicturesqueAPI.Controllers.Admin
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize(Policy = "Admin")]
    public class PictureController : Controller
    {
        private readonly IPictureServiceManager _pictureServiceManager;

        public PictureController(IPictureServiceManager pictureServiceManager)
        {
            _pictureServiceManager = pictureServiceManager;
        }

        [HttpPost("DeletePicture")]
        public async Task<IActionResult> DeleteCategory([FromBody]DeletePictureEntry entry)
        {
            try
            {
                Picture picture = await _pictureServiceManager.GetRawPictureById(entry.Id);
                await _pictureServiceManager.DeletePictureAsync(picture);

                return Ok(await _pictureServiceManager.GetPicturesAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdatePictureCategory")]
        public async Task<IActionResult> UpdatePictureCategory([FromBody]UpdatePictureEntry entry)
        {
            try
            {
                await _pictureServiceManager.UpdatePictureCategoryAsync(entry.CategoryId, entry.Id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _pictureServiceManager.GetPicturesAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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