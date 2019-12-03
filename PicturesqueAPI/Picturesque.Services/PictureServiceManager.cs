using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Picturesque.DB;
using Picturesque.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picturesque.Services
{
    public sealed class PictureServiceManager : IPictureServiceManager
    {
        private readonly PicturesqueDbContext _ctx;
        private readonly IMapper _mapper;

        public PictureServiceManager(PicturesqueDbContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
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

        public async Task UploadPicturesAsync(List<IFormFile> files)
        {
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        string img2Base64 = "data:image/jpeg;base64," + Convert.ToBase64String(fileBytes);
                        Picture picture = new Picture(img2Base64);
                        await _ctx.Pictures.AddAsync(picture);
                    }
                }
            }

            await _ctx.SaveChangesAsync();
        }
    }
}
