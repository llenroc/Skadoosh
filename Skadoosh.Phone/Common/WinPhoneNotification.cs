using System.Net.Http;
using Microsoft.Phone.Notification;
using Microsoft.WindowsAzure.MobileServices;
using Skadoosh.Common.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skadoosh.Phone.Common
{
    public class WinPhoneNotification
    {
        public static HttpNotificationChannel GetNotificationChannel(string channelName)
        {
            var currentChannel = HttpNotificationChannel.Find(channelName);
            if (currentChannel == null)
            {
                currentChannel = new HttpNotificationChannel(channelName);
                currentChannel.Open();     
                //currentChannel.BindToShellTile();
                //currentChannel.BindToShellToast();
            }
            return currentChannel;
        }
    }
}
