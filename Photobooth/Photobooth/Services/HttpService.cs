using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Photobooth.Services
{
    public static class HttpService
    {
        private static readonly HttpClient _http = new HttpClient();

        /// <summary>
        /// Stores a captured image on the server
        /// </summary>
        /// <param name="image">The image to send with the JSON</param>
        /// <returns>A bool indicating success</returns>
        public static async Task<bool> PostAsync(Stream image)
        {
            var uri = new Uri(Constants.ServerUrl);
            var requestBody = CreateRequestBody(image);

            var response = await _http.PostAsync(uri, requestBody);
            return response.IsSuccessStatusCode;
        }

        /// <summary> Creates a request body from the provided data.</summary>
        private static MultipartFormDataContent CreateRequestBody(Stream image)
        {
            var requestBody = new MultipartFormDataContent();
            var fileContent = new StreamContent(image);
            fileContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("image/jpg");
            requestBody.Add(fileContent, "image", "image.jpg");
            return requestBody;
        }
    }
}
