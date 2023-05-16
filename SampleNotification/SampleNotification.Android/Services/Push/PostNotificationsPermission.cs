using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SampleNotification.Droid.Services.Push;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Xamarin.Essentials.Permissions;

namespace SampleNotification.Droid.Services.Push
{
    internal class PostNotificationsPermission : BasePlatformPermission
    {
        public override (string androidPermission, bool isRuntime)[] RequiredPermissions =>
            new List<(string androidPermission, bool isRuntime)>
            {
                (Manifest.Permission.PostNotifications, true)
            }.ToArray();
    }
}