using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Notification;
using Microsoft.Phone.Shell;
using Skadoosh.Common.ViewModels;
using Skadoosh.Phone.Common;

namespace Skadoosh.Phone.Views
{
    public partial class ParticipateLive : PhoneApplicationPage
    {
        private HttpNotificationChannel _notificationChannel;

        private ParticipateLiveVM VM
        {
            get { return (ParticipateLiveVM)this.DataContext; }
        }
        public ParticipateLive()
        {
            InitializeComponent();
            this.Loaded += async(e, a) =>
            {
                this.DataContext = (ParticipateLiveVM)App.ApplicationVM;
                _notificationChannel = WinPhoneNotification.GetNotificationChannel(VM.CurrentSurvey.ChannelName);
                _notificationChannel.HttpNotificationReceived += _notificationChannel_HttpNotificationReceived;
                _notificationChannel.ChannelUriUpdated += async (ee, aa) =>
                {
                    Dispatcher.BeginInvoke(async () =>
                    {
                         Register();
                    });
                    
                };


            };
        }

        private async void Register()
        {
            await VM.RegisterForNotification(_notificationChannel.ChannelUri.ToString(), "WinPhone", VM.CurrentSurvey.ChannelName);
      
        }
        void _notificationChannel_HttpNotificationReceived(object sender, HttpNotificationEventArgs e)
        {
            var x = 10;
        }

        private void GoBack(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}