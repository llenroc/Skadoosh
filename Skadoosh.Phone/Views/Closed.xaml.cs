using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Skadoosh.Phone.Views
{
    public partial class Closed : PhoneApplicationPage
    {
        public Closed()
        {
            InitializeComponent();
        }
        private void GoHome(object sender, EventArgs e)
        {
            Home();
        }
        private async void GoToHome(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Home();
        }
        private void Home()
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
    }
}