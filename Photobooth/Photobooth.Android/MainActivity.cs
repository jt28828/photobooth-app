using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android;

namespace Photobooth.Droid
{
    [Activity(Label = "Photobooth", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private const int CAMERA_REQUEST = 0;
        public static Context AppContext;

        protected override void OnCreate(Bundle bundle)
        {
            AppContext = ApplicationContext;
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            CheckCamera();
        }

        private void CheckCamera()
        {
            //Check to see if any permission in our group is available, if one, then all are
            const string permission = Manifest.Permission.Camera;
            if (CheckSelfPermission(permission) != (int)Permission.Granted)
            {
                RequestCamera();
            }
            else
            {
                LoadApplication(new App());
            }
        }

        private void RequestCamera()
        {
            string[] permissions = new[] { Manifest.Permission.Camera };

            //Finally request permissions with the list of permissions and Id
            RequestPermissions(permissions, CAMERA_REQUEST);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            switch (requestCode)
            {
                case CAMERA_REQUEST:
                    {
                        if (grantResults[0] == Permission.Granted)
                        {
                            LoadApplication(new App());
                        }
                    }
                    break;
            }
        }
    }
}

