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
using Photobooth.Droid.PlatformSpecific;
using Photobooth.Droid.PlatformSpecific.Listeners;
using Photobooth.PlatformSpecific;

[assembly: Xamarin.Forms.Dependency(typeof(CameraControls))]
namespace Photobooth.Droid.PlatformSpecific
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