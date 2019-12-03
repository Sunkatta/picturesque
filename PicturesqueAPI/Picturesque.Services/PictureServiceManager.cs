using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Picturesque.DB;
using Picturesque.Domain;
using System;
using System.Collections.Generic;
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
    }
}
