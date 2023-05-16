using System.Collections.Generic;
using Android.App;
using Android.Content;
using FreshMvvm;
using SampleNotification.Services;

namespace SampleNotification.Droid.Services
{
    [Service]
    public class DenyIntentService : IntentService
    {
        public DenyIntentService() : base("DenyIntentService")
        {
        }

        protected override async void OnHandleIntent(Intent intent)
        {
            var notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
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