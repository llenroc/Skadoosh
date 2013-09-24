using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Skadoosh.Common.ViewModels;
using Skadoosh.Phone.Views;

namespace Skadoosh.Phone
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void NavigateToSelectSurvey(object sender, GestureEventArgs e)
        {
            switch (((Grid)sender).Name)
            {

                case "btnParticipate":
                    App.ApplicationVM = new ParticipateLiveVM();
                    NavigationService.Navigate(new Uri("/Views/SelectSurvey.xaml?obj=1", UriKind.Relative));

                    break;
                case "btnGroupInvite":
                    App.ApplicationVM = new ParticipateStaticVM();
                    NavigationService.Navigate(new Uri("/Views/SelectSurvey.xaml?obj=2", UriKind.Relative));
                    break;
            }
        }
    }
}