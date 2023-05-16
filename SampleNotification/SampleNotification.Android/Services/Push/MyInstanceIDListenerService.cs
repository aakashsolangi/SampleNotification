using Android.App;
using Android.Content;
using Firebase.Iid;
using Firebase.Messaging;

namespace SampleNotification.Droid.Services
{
    [Service(Exported = false), IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class MyInstanceIDListenerService : FirebaseInstanceIdService
    {
        public override void OnTokenRefresh()
        {
            WakefulIntentService.SendWakefulWork(this, typeof(RegistrationIntentService));
        }

       
    }
}