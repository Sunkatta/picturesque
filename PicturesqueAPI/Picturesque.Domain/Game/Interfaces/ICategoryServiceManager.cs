using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Picturesque.Domain
{
    public interface ICategoryServiceManager
    {
        Task<IEnumerable<CategoryView>> GetCategoriesAsync();
    }
}
