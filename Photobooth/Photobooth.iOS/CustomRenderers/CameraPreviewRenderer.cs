
using System;
using System.IO;
using Foundation;
using Photobooth;
using Photobooth.CustomViews;
using Photobooth.iOS.CustomRenderers;
using Photobooth.iOS.Views;
using Photobooth.Services;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CameraPreview), typeof(CameraPreviewRenderer))]
namespace Photobooth.iOS.CustomRenderers
{
    public class CameraPreviewRenderer : ViewRenderer<CameraPreview, UICameraPreview>
    {
        private UICameraPreview _uiCameraPreview;
        private CameraPreview _currentPreviewInstance;
        private Storage _storageInstance;

        protected override void OnElementChanged(ElementChangedEventArgs<CameraPreview> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                // Pretty much the constructor in here once the actual view is ready
                _currentPreviewInstance = e.NewElement;
                _uiCameraPreview = new UICameraPreview(e.NewElement.Camera);
                _storageInstance = Storage.Instance;
                e.NewElement.OnTakePhotoCommand += TakePicture;
                SetNativeControl(_uiCameraPreview);
            }
            if (e.OldElement != null)
            {
                // Unsubscribe
                _uiCameraPreview.Tapped -= OnCameraPreviewTapped;
            }
            if (e.NewElement != null)
            {
                // Subscribe
                _uiCameraPreview.Tapped += OnCameraPreviewTapped;
            }
        }

        private void OnCameraPreviewTapped(object sender, EventArgs e)
        {
            if (_uiCameraPreview.IsPreviewing)
            {
                _uiCameraPreview.CaptureSession.StopRunning();
                _uiCameraPreview.IsPreviewing = false;
            }
            else
            {
                _uiCameraPreview.CaptureSession.StartRunning();
                _uiCameraPreview.IsPreviewing = true;
            }
        }

        /// <summary>
        /// Called when the picture should be taken
        /// </summary>
        private void TakePicture()
        {
            // Take the image and save to the Tablet
            var image = _uiCameraPreview.Capture();
            image.SaveToPhotosAlbum(null);

            // Get the image as a JPEG stream and store in the app for sending to the server
            Stream imageStream = image.AsJPEG().AsStream();
            _storageInstance.CurrentImage = imageStream;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Control.CaptureSession.Dispose();
                Control.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}