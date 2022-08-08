using Microsoft.AspNetCore.Http;
using MSCourse.Shared.Dtos;
using MSCourse.Web.Models.PhotoStockModels;
using MSCourse.Web.Services.Interfaces;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MSCourse.Web.Services
{
    public class PhotoStockService : IPhotoStockService
    {
        private HttpClient _httpClient;

        public PhotoStockService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> DeletePhoto(string url)
        {
            if (url == null)
            {
                return false;
            }

            var response = await _httpClient.DeleteAsync($"photos?photoUrl={url}");

            return response.IsSuccessStatusCode;
        }

        public async Task<PhotoViewModel> UploadPhoto(IFormFile photo)
        {
            if (photo == null || photo.Length <= 0)
            {
                return null;
            }

            // asd32s-sdfs324sd-23sdfsd-23dsf.jpg olarak geri döndürür.
            var randomFilename = $"{Guid.NewGuid().ToString()}{Path.GetExtension(photo.FileName)}";

            using var ms = new MemoryStream();

            await photo.CopyToAsync(ms);

            var multipartContent = new MultipartFormDataContent();

            multipartContent.Add(new ByteArrayContent(ms.ToArray()), "photo", randomFilename);

            var response = await _httpClient.PostAsync("photos", multipartContent);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var result = await response.Content.ReadFromJsonAsync<Response<PhotoViewModel>>();

            return result.Data;
        }
    }
}
