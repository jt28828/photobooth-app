using System.Threading.Tasks;
using PhotoBooth.Services;

namespace PhotoBooth.ViewModels
{
    public class MainPageViewModel
    {
        public Task<byte[]> CaptureImageAsync()
        {

        }

        /// <summary>
        /// Uploads an image to the server.
        /// </summary>
        /// <param name="uploader">The uploader's name</param>
        /// <param name="image">The image to upload</param>
        public async Task UploadImageAsync(string uploader, byte[] image)
        {
            var success = await HttpService.PostAsync(uploader, image);

            if (success)
            {
                await App.Current.MainPage.DisplayAlert("Success", "Your image has been uploaded to the website", "Ok");
            }  else
            {
                await App.Current.MainPage.DisplayAlert("Error", "An error occurred updloading your image. It has still been saved on the tablet", "Ok");
            }
        }
    }
}
