using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.PushNotifications;

namespace Skadoosh.Store.Common
{
    public class Win8Notification
    {
        public static async Task<PushNotificationChannel> GetNotificationChannel()
        {
            var currentChannel =
                await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
            return currentChannel;
            
        }
    }
}
