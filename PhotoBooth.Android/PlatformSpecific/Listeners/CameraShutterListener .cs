using System;
using Android.App;
using Android.Content;
using Android.Hardware.Camera2;
using static Android.Hardware.Camera;

namespace PhotoBooth.Droid.PlatformSpecific.Listeners
{
    public class CameraShutterListener : Java.Lang.Object, IShutterCallback
    {
        public IntPtr Handle => new IntPtr();

        public void Dispose()
        {
            // Who cares about the shutter
        }

        public void OnShutter()
        {
            // Who cares about the shutter
        }
    }
}