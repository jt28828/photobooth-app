using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PhotoBooth.Services
{
    public static class HttpService
    {
        private static readonly HttpClient _http = new HttpClient();

        /// <summary>
        /// Stores a captured image on the server
        /// </summary>
        /// <param name="name">The name of the person uploading the photo</param>
        /// <param name="image">The image to send with the JSON</param>
        /// <returns>A bool indicating success</returns>
        public static async Task<bool> PostAsync(string name, byte[] image)
        {
            var uri = new Uri(Constants.ServerUrl);
            var requestBody = CreateRequestBody(name, image);

            var response = await _http.PostAsync(uri, requestBody);
            return response.IsSuccessStatusCode;
        }

        /// <summary> Creates a request body from the provided data.</summary>
        private static MultipartFormDataContent CreateRequestBody(string name, byte[] image)
        {
            var requestBody = new MultipartFormDataContent();
            var nameContent = new StringContent(name);
            var fileContent = new ByteArrayContent(image);
            fileContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("image/png");
            requestBody.Add(fileContent, "image", "image.png");
            requestBody.Add(nameContent, "name");
            return requestBody;
        }
    }
}
