using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SampleNotification.Services
{
    public interface IPostNotificationPermissionService
    {
        Task<bool> CheckAndRequestPermissions();
    }
}
