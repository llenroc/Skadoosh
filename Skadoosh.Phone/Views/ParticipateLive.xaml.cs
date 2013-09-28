using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Notification;
using Skadoosh.Common.ViewModels;
using Skadoosh.Phone.Common;
using Coding4Fun.Toolkit.Controls;
using System.Windows.Media.Imaging;
using System.IO;
using System.Text;
using Skadoosh.Common.DomainModels;

namespace Skadoosh.Phone.Views
{
    public partial class ParticipateLive : PhoneApplicationPage
    {
        private DispatcherTimer _timer;
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
                if (App.NotificationChannel != null)
                {
                    App.NotificationChannel.HttpNotificationReceived -= NotificationChannel_HttpNotificationReceived;
                    App.NotificationChannel.Close();
                    App.NotificationChannel.Dispose();
                }
                App.NotificationChannel = WinPhoneNotification.GetNotificationChannel(VM.CurrentSurvey.ChannelName);
                App.NotificationChannel.ChannelUriUpdated += async (ee, aa) =>
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
            App.NotificationChannel.HttpNotificationReceived += NotificationChannel_HttpNotificationReceived;
            await VM.RegisterForNotification(App.NotificationChannel.ChannelUri.ToString(), "WinPhone", VM.CurrentSurvey.ChannelName);   
        }

        void NotificationChannel_HttpNotificationReceived(object sender, HttpNotificationEventArgs e)
        {
            var title = string.Empty;
            var message = string.Empty;
            using (var reader = new StreamReader(e.Notification.Body, Encoding.UTF8))
            {
                var temp = reader.ReadToEnd();
                var data = temp.Split(';');
                title = data[1];
                message = data[0];
            }

            Dispatcher.BeginInvoke(async () =>
            {
                var toast = new ToastPrompt()
                {
                    Background = new SolidColorBrush(Colors.White),
                    Foreground = new SolidColorBrush(Colors.Black),
                    Title = title,
                    FontSize = 30,
                    Message = message,
                    TextOrientation = System.Windows.Controls.Orientation.Vertical,
                    ImageSource = new BitmapImage(new Uri("/Assets/ApplicationIcon.png", UriKind.RelativeOrAbsolute))
                };

                toast.Show();

                _timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 5) };
                _timer.Tick += timer_Tick;
                _timer.Start();
            });
        }

        private async void timer_Tick(object sender, object e)
        {
            _timer.Tick -= timer_Tick;
            _timer.Stop();
            _timer = null;
            await VM.SaveCurrentQuestionResponses();
            var result = await VM.LoadCurrentQuestionForSurvey();
            if (result == 0)
            {
                NavigationService.Navigate(new Uri("/Views/Closed.xaml", UriKind.Relative));
                
            }
        }
        private void ItemTapped(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var opt = (Option)((StackPanel)sender).DataContext;
            if (VM.CurrentQuestion.IsMultiSelect)
            {
                if (opt.IsSelected)
                    opt.IsSelected = false;
                else
                    opt.IsSelected = true;
            }
            else
            {
                foreach (var obj in VM.CurrentQuestion.Options)
                {
                    obj.IsSelected = false;
                }
                opt.IsSelected = true;
            }

        }
        private async void GoToHome(object sender, System.Windows.Input.GestureEventArgs e)
        {
            StopAndSaveSurvey();
        }

        private void QuitSurvey(object sender, EventArgs e)
        {
            StopAndSaveSurvey();
        }

        private async void StopAndSaveSurvey()
        {
            string message = "Do you want to save and quit the survey or return?";
            string caption = "Exiting Survey";
            var buttons = MessageBoxButton.OKCancel;
            MessageBoxResult result = MessageBox.Show(message, caption, buttons);
            if (result != MessageBoxResult.Cancel)
            {
                await VM.SaveCurrentQuestionResponses();
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            }

        }
    }
}