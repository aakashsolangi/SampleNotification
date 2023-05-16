using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;

namespace SampleNotification.Droid.Services
{
    public abstract class WakefulIntentService : IntentService
    {
        protected abstract void DoWakefulWork(Intent intent);

        public static string NAME = "sample.notification.android.tasks.WakefulIntentService";

        static volatile PowerManager.WakeLock lockStatic;

        static object locker = new object();

        static PowerManager.WakeLock GetLock(Context context)
        {
            lock (locker)
            {
                if (lockStatic == null)
                {
                    var manager = (PowerManager)context.GetSystemService(Context.PowerService);
                    lockStatic = manager.NewWakeLock(WakeLockFlags.Partial, NAME);
                    lockStatic.SetReferenceCounted(true);
                }
                return lockStatic;
            }
        }

        public static void SendWakefulWork(Context context, Intent intent)
        {
            GetLock(context.ApplicationContext).Acquire();
            context.StartService(intent);
        }

        public static void SendWakefulWork(Context context, Type classService)
        {
            SendWakefulWork(context, new Intent(context, classService));
        }

        protected WakefulIntentService(string name) : base(name)
        {
             SetIntentRedelivery(true);
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            var wakeLock = GetLock(ApplicationContext);

            if (!lockStatic.IsHeld || (flags & StartCommandFlags.Redelivery) != 0)
            {
                wakeLock.Acquire();
            }

            base.OnStartCommand(intent, flags, startId);

            return StartCommandResult.RedeliverIntent;
        }

        protected override void OnHandleIntent(Intent intent)
        {
            try
            {
                DoWakefulWork(intent);
            }
            finally
            {
                var wakeLock = GetLock(ApplicationContext);

                if (wakeLock.IsHeld)
                {
                    try
                    {
                        wakeLock.Release();
                    }
                    catch (Exception ex)
                    {
                        Log.Error(Class.SimpleName, "Exception when releasing wakelock", ex);
                    }
                }
            }
        }
    }
}
