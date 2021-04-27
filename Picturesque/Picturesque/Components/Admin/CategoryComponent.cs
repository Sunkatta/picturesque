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
        public Category[] categories;
        public Category category = new Category();
        public bool addingCategory = false;
        public bool editingCategory = false;
        public string errorMessage;
        public string token;

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
            errorMessage = string.Empty;
            editingCategory = false;
            category = new Category();
        }

        protected void OnEditCategory(string categoryId)
        {
            editingCategory = !editingCategory;
            addingCategory = false;
            category = categories.FirstOrDefault(c => c.Id == categoryId);
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
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("400"))
                {
                    errorMessage = "Category already exists";
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
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("400"))
                {
                    errorMessage = "Category already exists";
                }
            }

        }

        protected async Task DeleteCategory(string categoryId)
        {
            category = categories.FirstOrDefault(c => c.Id == categoryId);
            categories = await client.PostJsonAsync<Category[]>(ApiConstants.ApiUrl + "Category/DeleteCategory", category);
            category = new Category();
        }
    }
}
