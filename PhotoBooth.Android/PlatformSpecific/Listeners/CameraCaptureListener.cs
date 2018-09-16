using System;
using Android.App;
using Android.Content;
using Android.Hardware;
using Android.Hardware.Camera2;
using static Android.Hardware.Camera;

namespace PhotoBooth.Droid.PlatformSpecific.Listeners
{
    public class CameraCaptureListener : Java.Lang.Object, IPictureCallback
    {
        public byte[] ImageData;

        public IntPtr Handle => new IntPtr();

        public void Dispose()
        {
            // Cleanup here
        }

        public void OnPictureTaken(byte[] data, Camera camera)
        {
            ImageData = data;
        }
    }
}