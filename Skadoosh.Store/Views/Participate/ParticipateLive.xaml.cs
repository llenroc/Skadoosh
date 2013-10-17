using Windows.UI.Core;
using Skadoosh.Common.ViewModels;
using Skadoosh.Store.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Networking.PushNotifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Popups;
using Skadoosh.Common.DomainModels;
using Windows.UI.ApplicationSettings;
using Windows.System;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Skadoosh.Store.Views.Participate
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class ParticipateLive : Skadoosh.Store.Common.LayoutAwarePage
    {
        private PushNotificationChannel _notificationChannel;
        private DispatcherTimer _timer;

        private ParticipateLiveVM VM
        {
            get { return (ParticipateLiveVM)this.DataContext; }
            set { this.DataContext = value; }
        }
        public ParticipateLive()
        {
            this.InitializeComponent();

        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override async void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            VM = (ParticipateLiveVM)navigationParameter;
            _notificationChannel = await Win8Notification.GetNotificationChannel();
            _notificationChannel.PushNotificationReceived += notificationChannel_PushNotificationReceived; 
            await VM.RegisterForNotification(_notificationChannel.Uri, "Win8", VM.CurrentSurvey.ChannelName);
        }

        private async void notificationChannel_PushNotificationReceived(PushNotificationChannel sender, PushNotificationReceivedEventArgs args)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
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
            await VM.FindSurveyCurrentChannel();
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }



        private  void ExitSurvey(object sender, RoutedEventArgs e)
        {
            ExitSurveyNow();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var items = ((ListView)sender).SelectedItems;
            foreach (var opt in VM.CurrentQuestion.Options)
            {
                opt.IsSelected = false;
            }
            foreach (var item in items)
            {
                var opt = (Option)item;
                VM.CurrentQuestion.Options.First(x => x.Id == opt.Id).IsSelected = true;
            } 
        }
        private void ShowHelp(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Help), new ParticipateStaticVM());
        }

        private  void GoToHome(object sender, RoutedEventArgs e)
        {
            ExitSurveyNow();
        }


        private async void ExitSurveyNow()
        {
            if (VM.CurrentSurvey.IsActive)
            {
                var msg =
                    new MessageDialog(
                        "You are exiting the survey. Results are automatically saved, exit or return to the survey?",
                        "Exit Survey Notification");
                msg.Commands.Add(new UICommand("Exit", async (a) =>
                {
                    await VM.SaveCurrentQuestionResponses();
                    _notificationChannel.PushNotificationReceived -= notificationChannel_PushNotificationReceived;
                    await VM.UnRegisterForNotification(_notificationChannel.Uri);
                    Frame.Navigate(typeof(Home), VM);
                }));
                msg.Commands.Add(new UICommand("Return to Survey", (a) =>
                {

                }));
                await msg.ShowAsync();
            }
            else
            {
                Frame.Navigate(typeof(Home), VM);
            }
        }
    }
}
