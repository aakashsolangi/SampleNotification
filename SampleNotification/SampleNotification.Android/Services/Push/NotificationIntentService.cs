using System.Collections.Generic;
using Android.App;
using Android.Content;
using FreshMvvm;
using SampleNotification.Droid;

namespace SampleNotification.Droid.Services
{
    //This will handle "default" background push notification action.
    //User did not accept or deny, but tapped the message
    [Service]
    public class NotificationIntentService : IntentService
    {
        public NotificationIntentService() : base("NotificationIntentService")
        {

        }


        //If the app is in back ground it works fine but as soon as it starts activity nothing happens on splash screen
        protected override async void OnHandleIntent(Intent intent)
        {
            try
            {

                var notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
                notificationManager.Cancel(0);

                var action = intent.Extras.GetString("action"); 

                if (string.IsNullOrWhiteSpace(action))
                {
                    action = "default";
                }

                var newIntent = new Intent(this, typeof(SplashActivity));

                if (intent?.Extras != null)
                {
                    newIntent.PutExtras(intent.Extras);
                }

                newIntent.AddFlags(ActivityFlags.NewTask);
                StartActivity(newIntent);

            }
            catch (System.Exception ex)
            {
               
            }
        }

        
    }
}