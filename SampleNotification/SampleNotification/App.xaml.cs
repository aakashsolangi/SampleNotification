using Acr.UserDialogs;
using FreshMvvm;
using SampleNotification.Navigation;
using SampleNotification.PageModels;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampleNotification
{
    public partial class App : Application
    {
        public static bool IsInBackground = true;
        public static object ActivityContext { get; set; }
        public App()
        {
            InitializeComponent();
            IsInBackground = true;
            var page = FreshPageModelResolver.ResolvePageModel<MyTestPageModel>();
            MainPage = new SeNavigationPage(page);
        }

        protected override void OnStart()
        {
            IsInBackground = false;
        }

        protected override void OnSleep()
        {
            IsInBackground = true;
        }

        protected override void OnResume()
        {
            IsInBackground = false;
        }

        public static async Task HandleNotificationOnForeground(string message)
        {

            await UserDialogs.Instance.ConfirmAsync(message,"Test Notification","Ok");
            return;




        }
    }
}
