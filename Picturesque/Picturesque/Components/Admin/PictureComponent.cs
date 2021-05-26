using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Picturesque.Models;
using Picturesque.Models.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Picturesque.Components
{
    public class PictureComponent : ComponentBase
    {
        [Inject]
        protected MatBlazor.IMatToaster Toaster { get; set; }

        protected List<Picture> pictures;
        protected Picture picture = new Picture();
        protected Category[] categories;
        protected bool addingPicture = false;
        protected bool editingPicture = false;
        protected string token;

        protected HttpClient client;

        public async Task GetPicturesAndCategories()
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            pictures = await GetPictures();
            categories = await client.GetJsonAsync<Category[]>(ApiConstants.ApiUrl + "Category");
        }

        protected async Task<List<Picture>> GetPictures()
        {
            return await client.GetJsonAsync<List<Picture>>(ApiConstants.ApiUrl + "Picture");
        }

        protected void OnAddPicture()
        {
            addingPicture = !addingPicture;
            editingPicture = false;
        }

        protected void OnEditPicture(string pictureId)
        {
            editingPicture = !editingPicture;
            picture = pictures.FirstOrDefault(c => c.Id == pictureId);
        }

        protected async Task AddCategoryToPicture()
        {
            Picture oldPic = picture;
            var response = await client.PostAsync(
                    ApiConstants.ApiUrl + "Picture/UpdatePictureCategory",
                    new StringContent(JsonConvert.SerializeObject(picture), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                picture.CategoryName = categories.FirstOrDefault(x => x.Id == picture.CategoryId).Name;
                pictures[pictures.FindIndex(i => i.Id == picture.Id)] = picture;
                editingPicture = false;
                picture = new Picture();
                Toaster.Add("Picture category updated successfully", MatBlazor.MatToastType.Success);
            }
            else
            {
                pictures[pictures.FindIndex(i => i.Id == picture.Id)] = oldPic;
                Toaster.Add("There was an error while updating picture category", MatBlazor.MatToastType.Danger);
            }
        }

        protected async Task DeletePicture(string pictureId)
        {
            picture = pictures.FirstOrDefault(c => c.Id == pictureId);
            pictures = await client.PostJsonAsync<List<Picture>>(ApiConstants.ApiUrl + "Picture/DeletePicture", picture);
            picture = new Picture();
            Toaster.Add("Picture deleted successfully", MatBlazor.MatToastType.Success);
        }
    }
}
