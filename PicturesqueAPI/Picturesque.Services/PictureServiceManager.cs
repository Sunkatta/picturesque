using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Picturesque.DB;
using Picturesque.Domain;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picturesque.Services
{
    public sealed class PictureServiceManager : IPictureServiceManager
    {
        private readonly PicturesqueDbContext _ctx;
        private readonly IMapper _mapper;
        private readonly ICategoryServiceManager _categoryServiceManager;

        public PictureServiceManager(
            PicturesqueDbContext ctx, 
            IMapper mapper,
            ICategoryServiceManager categoryServiceManager
        )
        {
            _ctx = ctx;
            _mapper = mapper;
            _categoryServiceManager = categoryServiceManager;
        }

        public async Task AddPictureToCategory(Picture picture, string categoryName)
        {
            var category = 
                await _categoryServiceManager.GetRawCategoryByName(categoryName);

            var pictureCategory =
                new PicturesCategories(
                    category.Id,
                    picture.Id
                    );

            await _ctx.AddAsync(pictureCategory);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeletePictureAsync(Picture picture)
        {
            _ctx.Pictures.Remove(picture);
            await _ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<PictureView>> GetPicturesAsync()
        {
            var pics =
                await _ctx.PicturesCategories
                .Select(x => 
                    new PictureView(
                        x.PictureId,
                        x.Picture.Img2Base64,
                        x.Category.Name,
                        x.CategoryId
                        )
                    )
                .ToListAsync();

            return pics;
        }

        public async Task<Picture> GetRawPictureById(string id)
        {
            return
                await _ctx.Pictures
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdatePictureCategoryAsync(string categoryId, string pictureId)
        {
            var pictureCategory =
                await _ctx.PicturesCategories
                .FirstOrDefaultAsync(c => c.PictureId == pictureId);

            _ctx.PicturesCategories.Remove(pictureCategory);

            pictureCategory = new PicturesCategories(categoryId, pictureId);

            await _ctx.PicturesCategories.AddAsync(pictureCategory);
            await _ctx.SaveChangesAsync();
        }

        public async Task UploadPicturesAsync(List<IFormFile> files)
        {
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    using (var image = Image.Load(file.OpenReadStream()))
                    {
                        image.Mutate(x => x.Resize(256, 256));
                        string img2Base64 = image.ToBase64String(PngFormat.Instance);
                        Picture picture = new Picture(img2Base64);
                        await _ctx.Pictures.AddAsync(picture);
                        await AddPictureToCategory(picture, "No Category");
                    }
                }
            }

            await _ctx.SaveChangesAsync();
        }
    }
}
