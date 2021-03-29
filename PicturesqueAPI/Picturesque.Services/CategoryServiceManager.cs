using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Picturesque.DB;
using Picturesque.Domain;
using System.Collections.Generic;
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

        public async Task CreateCategoryAsync(Category category)
        {
            await _ctx.Categories.AddAsync(category);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(Category category)
        {
            _ctx.Categories.Remove(category);
            await _ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<CategoryView>> GetCategoriesAsync()
        {
            List<Category> rawCategories = await _ctx.Categories.ToListAsync();
            List<CategoryView> categories = _mapper.Map<List<CategoryView>>(rawCategories);

            return categories;
        }

        public async Task<Category> GetRawCategoryById(string id)
        {
            return await _ctx.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category> GetRawCategoryByName(string name)
        {
            return await _ctx.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            _ctx.Entry(category).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
        }
    }
}
