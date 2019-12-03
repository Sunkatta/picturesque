using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Picturesque.Domain
{
    public interface IPictureServiceManager
    {
        Task AddPictureToCategory(Picture picture, string categoryName);
        Task DeletePictureAsync(Picture picture);
        Task<IEnumerable<PictureView>> GetPicturesAsync();
        Task<Picture> GetRawPictureById(string id);
        Task UpdatePictureCategoryAsync(string categoryId, string pictureId);
        Task UploadPicturesAsync(List<IFormFile> files);
    }
}
