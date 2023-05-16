using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Firebase.Iid;


namespace SampleNotification.Droid.Services
{
    [Service(Exported = false)]
    public class RegistrationIntentService : WakefulIntentService
    {
        private static readonly object Locker = new object();

        public RegistrationIntentService() : base("RegistrationIntentService") { }

        protected override void DoWakefulWork(Intent intent)
        {
            try
            {
                lock (Locker)
                {
                    var token = FirebaseInstanceId.Instance.Token;

                    _ = SendRegistrationToAppServer(token);
                }
            }
            catch (TaskCanceledException) { }
            catch (Exception e)
            {

                return;
            }
        }

        private async System.Threading.Tasks.Task SendRegistrationToAppServer(string token)
        {
            var tokenToRegister = token;
        }
    }
}