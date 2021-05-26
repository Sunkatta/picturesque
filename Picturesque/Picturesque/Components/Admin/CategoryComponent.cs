using Microsoft.AspNetCore.Components;
using Picturesque.Models;
using Picturesque.Models.Constants;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Picturesque.Components
{
    public class CategoryComponent : ComponentBase
    {
        [Inject]
        protected MatBlazor.IMatToaster Toaster { get; set; }

        protected Category[] categories;
        protected Category category = new Category();

        protected bool addingCategory = false;
        protected bool editingCategory = false;

        protected string token;
        protected string oldCategoryName;

        protected HttpClient client;

        public async Task GetCategories()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            categories = await client.GetJsonAsync<Category[]>(ApiConstants.ApiUrl + "Category");
        }

        protected void OnAddCategory()
        {
            addingCategory = !addingCategory;
            editingCategory = false;
            category = new Category();
        }

        protected void OnEditCategory(string categoryId)
        {
            editingCategory = !editingCategory;
            addingCategory = false;
            category = categories.FirstOrDefault(c => c.Id == categoryId);
            oldCategoryName = category.Name;
        }

        protected async Task AddCategory()
        {
            try
            {
                await client.PostJsonAsync(ApiConstants.ApiUrl + "Category/CreateCategory", category);
                categories = null;
                categories = await client.GetJsonAsync<Category[]>(ApiConstants.ApiUrl + "Category");
                addingCategory = false;
                category = new Category();
                Toaster.Add("Category added successfully", MatBlazor.MatToastType.Success);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("400"))
                {
                    Toaster.Add("Category already exists", MatBlazor.MatToastType.Danger);
                }
            }

        }

        protected async Task EditCategory()
        {
            try
            {
                await client.PostJsonAsync(ApiConstants.ApiUrl + "Category/UpdateCategory", category);
                categories = null;
                categories = await client.GetJsonAsync<Category[]>(ApiConstants.ApiUrl + "Category");
                editingCategory = false;
                category = new Category();
                Toaster.Add("Category updated successfully", MatBlazor.MatToastType.Success);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("400"))
                {
                    category.Name = oldCategoryName;
                    Toaster.Add("Category already exists", MatBlazor.MatToastType.Danger);
                }
            }

        }

        protected async Task DeleteCategory(string categoryId)
        {
            category = categories.FirstOrDefault(c => c.Id == categoryId);
            categories = await client.PostJsonAsync<Category[]>(ApiConstants.ApiUrl + "Category/DeleteCategory", category);
            category = new Category();
            Toaster.Add("Category deleted successfully", MatBlazor.MatToastType.Success);
        }
    }
}
