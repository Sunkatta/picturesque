using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Picturesque.DB;
using Picturesque.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Picturesque.Services
{
    public sealed class CategoryServiceManager : ICategoryServiceManager
    {
        private readonly PicturesqueDbContext _ctx;
        private readonly IMapper _mapper;

        public CategoryServiceManager(PicturesqueDbContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryView>> GetCategoriesAsync()
        {
            var rawCategories =
                await _ctx.Categories.ToListAsync();
            var categories = _mapper.Map<List<CategoryView>>(rawCategories);

            return categories;
        }
    }
}
