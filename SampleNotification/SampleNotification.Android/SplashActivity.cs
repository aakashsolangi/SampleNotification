using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using AndroidX.AppCompat.App;
using static Xamarin.Essentials.Platform;

namespace SampleNotification.Droid
{
    [Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        static readonly string TAG = "X:" + typeof(SplashActivity).Name;

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
            
        }

        //If we start the app simply this method runs normally 
        //but when Yapped on notification and we try to start activity from notification intent service it did not run
        //Sometime: if no other then this app is opened and app is in back ground the notifiation tapped works and run this activity
        //but if any pther app in opened and this app is in back ground tapped on notifcation did not invoke splas activity
        //Killed mode notfication tapped never works
        
        //I tried to open another activity as well but it did not run for me
        protected override void OnResume()
        {
            base.OnResume();
            var mainActivity = new Android.Content.Intent(Application.Context, typeof(MainActivity));
            if (Intent?.Extras != null)
            {
                mainActivity.PutExtras(Intent.Extras);
            }


            StartActivity(mainActivity);
        }

        public override void OnBackPressed() { }
    }
}