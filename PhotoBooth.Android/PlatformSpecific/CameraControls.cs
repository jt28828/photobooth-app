using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Android.Hardware;
using Android.Hardware.Camera2;
using Android.Support.V4.Content;
using Android.Util;
using Java.Util;
using PhotoBooth.Droid.PlatformSpecific;
using PhotoBooth.Droid.PlatformSpecific.Listeners;
using PhotoBooth.PlatformSpecific;

[assembly: Xamarin.Forms.Dependency(typeof(CameraControls))]
namespace PhotoBooth.Droid.PlatformSpecific
{
    class CameraControls : ICameraControls
    {
        public async Task<Stream> TakePhotoAsync()
        {
            var shutterListener = new CameraShutterListener();
            var rawHandler = new CameraCaptureListener();
            var jpegHandler = new CameraCaptureListener();

            var frontCamera = Camera.Open(1);
            frontCamera.TakePicture(shutterListener, rawHandler, jpegHandler);

            var image = jpegHandler.ImageData;

            var stream = new MemoryStream(image)
            {
                Position = 0
            };
            return stream;
        }
        
    }
}