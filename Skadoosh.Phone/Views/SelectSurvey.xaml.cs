using System;
using Microsoft.Phone.Controls;
using Skadoosh.Common.ViewModels;

namespace Skadoosh.Phone.Views
{
    public partial class SelectSurvey : PhoneApplicationPage
    {
  
        public SelectSurvey()
        {
            InitializeComponent();
            this.Loaded += (e, a) =>
            {
                this.DataContext = App.ApplicationVM;
            };
        }


        private void GoBack(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.GoBack();
        }

        private async void FindSurvey(object sender, EventArgs e)
        {
            if (App.ApplicationVM is ParticipateStaticVM)
            {
                var result = await ((ParticipateStaticVM)this.DataContext).FindSurveyCurrentChannel();
                if (result == 1)
                {
                    NavigationService.Navigate(new Uri("/Views/ParticipateStatic.xaml", UriKind.Relative));
                }
            }
            else
            {
                var result = await ((ParticipateLiveVM)this.DataContext).FindSurveyCurrentChannel();
                if (result == 1)
                {
                    NavigationService.Navigate(new Uri("/Views/ParticipateLive.xaml", UriKind.Relative));
                }
            }
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/Help.xaml", UriKind.Relative));
        }
    }
}