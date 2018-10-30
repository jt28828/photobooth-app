using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Photobooth.Services;

namespace Photobooth.ViewModels
{
    public class MainPageViewModel
    {
        private Storage _storageInstance;

        public MainPageViewModel()
        {
            _storageInstance = Storage.Instance;
            _storageInstance.PropertyChanged += OnNewImageTaken;
        }

        /// <summary>
        /// Called when an image is taken.
        /// </summary>
        private void OnNewImageTaken(object sender, PropertyChangedEventArgs e) => QuestionUserUpload(_storageInstance.CurrentImage);

        /// <summary>
        /// Question the user about whether they want their image uploaded or not
        /// </summary>
        private async void QuestionUserUpload(Stream image)
        {
            var uploadResponse = await App.Current.MainPage.DisplayAlert("Upload photo?", "Do you want to upload this photo to our website?", "Yes", "No");

            if (uploadResponse)
            {
                await UploadImageAsync(image);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Done", "Photo wasn't uploaded", "Ok");
            }
        }

        /// <summary>
        /// Uploads an image to the server.
        /// </summary>
        /// <param name="uploader">The uploader's name</param>
        /// <param name="image">The image to upload</param>
        public async Task UploadImageAsync(Stream image)
        {
            var success = await HttpService.PostAsync(image);

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
