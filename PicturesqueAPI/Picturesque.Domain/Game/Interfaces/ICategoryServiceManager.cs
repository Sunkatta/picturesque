using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Picturesque.Domain
{
    public interface ICategoryServiceManager
    {
        Task CreateCategoryAsync(Category category);
        Task DeleteCategoryAsync(Category category);
        Task<IEnumerable<CategoryView>> GetCategoriesAsync();
        Task<Category> GetRawCategoryById(string id);
        Task UpdateCategoryAsync(Category category);
    }
}
