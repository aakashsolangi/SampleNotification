using System.Collections.Generic;
using Android.App;
using Android.Content;
using FreshMvvm;
using SampleNotification.Services;

namespace SampleNotification.Droid.Services
{
    [Service]
    public class AcceptIntentService : IntentService
    {
        public AcceptIntentService() : base("AcceptIntentService")
        {
        }

        protected override async void OnHandleIntent(Intent intent)
        {
            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.Cancel(0);

            try
            {
                var payload = intent.Extras.GetString("message");

                await App.HandleNotificationOnForeground(payload);

              
            }
            catch (System.Exception e)
            {

            }
        }
    }
}