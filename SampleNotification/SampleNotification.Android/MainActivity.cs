using Android.App;
using Android.Content.PM;
using Android.Gms.Common;
using Android.OS;
using SampleNotification.Droid.Services;
using Android.Content;
using System;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Android;
using Acr.UserDialogs;

namespace SampleNotification.Droid
{
    [Activity(Label = "SampleNotification", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = false, NoHistory =false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

        public const string CHANNEL_ID = "push";
        public override void OnBackPressed()
        {
            base.OnBackPressed();
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
        }


        protected override void OnResume()
        {
            base.OnResume();


        }

        protected override void OnCreate(Bundle bundle)
        {


            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            Rg.Plugins.Popup.Popup.Init(this);
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, bundle);
            base.OnCreate(bundle);
            Xamarin.Essentials.Platform.Init(this, bundle);

            this.Window.SetFlags(WindowManagerFlags.Secure, WindowManagerFlags.Secure);
            if (!global::Xamarin.Forms.Forms.IsInitialized)
            {
                global::Xamarin.Forms.Forms.Init(this, bundle);
            }

            UserDialogs.Init(this);
            LoadApplication(new App());

            //handle if launched from push notification actions

            var action = Intent?.Extras?.GetString("action");
            var message = Intent?.Extras?.GetString("message");


            if (action == "default")
            {
                _ = App.HandleNotificationOnForeground(message);
            }

            if (action == "accept")
            {

            }

            if (action == "deny")
            {

            }


            if (!IsPlayServicesAvailable()) return;


            WakefulIntentService.SendWakefulWork(Application.Context, typeof(RegistrationIntentService));
        }


        private static View FindView(View view, string contentDescription)
        {
            if (view is ViewGroup group)
            {
                for (int i = 0; i < group.ChildCount; i++)
                {
                    if (!(group.GetChildAt(i) is ViewGroup))
                    {
                        if (group.GetChildAt(i).ContentDescription == contentDescription)
                        {
                            return group.GetChildAt(i);
                        }
                    }
                    View vChild = group.GetChildAt(i);
                    var result = FindView(vChild, contentDescription);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }

            return null;
        }



        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

        }

        public bool IsPlayServicesAvailable()
        {
            var resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                {
                    var x = GoogleApiAvailability.Instance.GetErrorString(resultCode);
                }
                else
                {

                    Finish();
                }
                return false;
            }
            else
            {
                return true;
            }
        }

        protected override void OnStart()
        {
            base.OnStart();
            const int requestLocationId = 0;

            string[] notiPermission =
            {
                Manifest.Permission.PostNotifications
            };

            if ((int)Build.VERSION.SdkInt < 33) return;

            if (this.CheckSelfPermission(Manifest.Permission.PostNotifications) != Permission.Granted)
            {
                this.RequestPermissions(notiPermission, requestLocationId);
            }
        }
    }
}

