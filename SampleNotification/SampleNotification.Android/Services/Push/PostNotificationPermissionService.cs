using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SampleNotification.Services;
using SampleNotification.Droid.Services.Push;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(PostNotificationPermissionService))]
namespace SampleNotification.Droid.Services.Push
{
    public class PostNotificationPermissionService : IPostNotificationPermissionService
    {
        public async Task<bool> CheckAndRequestPermissions()
        {
            //Tiramisu is Android v13
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Tiramisu)
            {
                var status = await Xamarin.Essentials.Permissions.CheckStatusAsync<PostNotificationsPermission>();
                if (status == PermissionStatus.Granted)
                {
                    return true;
                }
                status = await Xamarin.Essentials.Permissions.RequestAsync<PostNotificationsPermission>();
                return status == PermissionStatus.Granted;
            }
            return true;
        }
    }
}