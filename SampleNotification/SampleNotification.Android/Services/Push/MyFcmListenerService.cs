using Android.App;
using Android.Content;
using Android.OS;
using FreshMvvm;
using SampleNotification.Services;
using System;
using System.Collections.Generic;
using Android.Support.V4.App;
using Android.Media;
using Firebase.Messaging;

namespace SampleNotification.Droid.Services
{
    [Service(Exported = false), IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFcmListenerService : FirebaseMessagingService
    {

        public override async void OnMessageReceived(RemoteMessage message)
        {
            try
            {
                message.Data.TryGetValue("text", out var messageText);
                message.Data.TryGetValue("title", out var title);


                if (!App.IsInBackground)
                {
                    await App.HandleNotificationOnForeground(messageText);

                    return;
                }
                SendNotification(string.IsNullOrWhiteSpace(title) ? "Sample App" : title,
                                 messageText);

            }
            catch (Exception e)
            {

            }
        }

        private void SendNotification(string title,
                                      string message)
        {

            try
            {

                var notificationServiceIntent = new Intent(this, typeof(NotificationIntentService));
                notificationServiceIntent.PutExtra("action", "default");
                notificationServiceIntent.PutExtra("message", message);


                PendingIntent notificationIntent;
                if (Build.VERSION.SdkInt >= BuildVersionCodes.S)
                {
                    notificationServiceIntent.AddFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop | ActivityFlags.NewTask);
                    PendingIntentFlags pendingflags = PendingIntentFlags.Immutable;
                    notificationIntent = PendingIntent.GetService(this, 0, notificationServiceIntent, pendingflags);
                }
                else
                {
                    notificationServiceIntent.AddFlags(ActivityFlags.ClearTop);
                    PendingIntentFlags pendingflags = PendingIntentFlags.OneShot;
                    notificationIntent = PendingIntent.GetService(this, 0, notificationServiceIntent, pendingflags);
                }

                var acceptserviceIntent = new Intent(this, typeof(AcceptIntentService));
                acceptserviceIntent.PutExtra("action", "accept");
                acceptserviceIntent.PutExtra("title", title);
                acceptserviceIntent.AddFlags(ActivityFlags.ClearTop);

                PendingIntent acceptIntent;
                if (Build.VERSION.SdkInt >= BuildVersionCodes.S)
                {
                    PendingIntentFlags acceptflags = PendingIntentFlags.Immutable;
                    acceptIntent = PendingIntent.GetService(this, 0, acceptserviceIntent, acceptflags);
                }
                else
                {
                    PendingIntentFlags acceptflags = PendingIntentFlags.OneShot;
                    acceptIntent = PendingIntent.GetService(this, 0, acceptserviceIntent, acceptflags);
                }

                try
                {
                    if (((KeyguardManager)GetSystemService(KeyguardService)).IsKeyguardLocked)
                    {
                        acceptserviceIntent = new Intent(this, typeof(NotificationIntentService));
                        acceptserviceIntent.PutExtra("action", "accept");
                        acceptserviceIntent.PutExtra("message", message);
                        acceptserviceIntent.AddFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);


                        if (Build.VERSION.SdkInt >= BuildVersionCodes.S)
                        {
                            PendingIntentFlags acceptflags = PendingIntentFlags.UpdateCurrent | PendingIntentFlags.Immutable;
                            acceptIntent = PendingIntent.GetService(this, 1, acceptserviceIntent, acceptflags);
                        }
                        else
                        {
                            PendingIntentFlags acceptflags = PendingIntentFlags.UpdateCurrent;
                            acceptIntent = PendingIntent.GetService(this, 1, acceptserviceIntent, acceptflags);
                        }
                    }
                }
                catch { }

                var denyServiceIntent = new Intent(this, typeof(DenyIntentService));
                denyServiceIntent.PutExtra("action", "deny");
                denyServiceIntent.AddFlags(ActivityFlags.ClearTop);

                PendingIntent denyIntent;
                if (Build.VERSION.SdkInt >= BuildVersionCodes.S)
                {
                    PendingIntentFlags denyflags = PendingIntentFlags.Immutable;
                    denyIntent = PendingIntent.GetService(this, 0, denyServiceIntent, denyflags);
                }
                else
                {
                    PendingIntentFlags denyflags = PendingIntentFlags.OneShot;
                    denyIntent = PendingIntent.GetService(this, 0, denyServiceIntent, denyflags);
                }

                string channelId = "push";
                var notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
                if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
                {
                    var channel = notificationManager.GetNotificationChannel(channelId);

                    if (channel == null)
                    {
                        channel = new NotificationChannel(channelId,
                                    "Sample Notification App",
                                    NotificationImportance.Max);
                        notificationManager.CreateNotificationChannel(channel);
                    }
                }

                var notificationBuilder = new AndroidX.Core.App.NotificationCompat.Builder(this, channelId)
                    .SetDefaults(AndroidX.Core.App.NotificationCompat.DefaultAll)
                    .SetPriority((int)NotificationPriority.Max)
                    .SetSmallIcon(Resource.Drawable.icon)
                    .SetContentTitle(title)
                    .SetContentText(message)
                    .SetStyle(new AndroidX.Core.App.NotificationCompat.BigTextStyle().BigText(message))
                    .SetAutoCancel(true)
                    .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))
                    .SetContentIntent(notificationIntent);
               

                notificationManager.Notify(0, notificationBuilder.Build());
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}