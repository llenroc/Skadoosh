using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Skadoosh.Common.ViewModels;

namespace Skadoosh.Phone.Views
{
    public partial class ParticipateLive : PhoneApplicationPage
    {
        private ParticipateLiveVM VM
        {
            get { return (ParticipateLiveVM)this.DataContext; }
        }
        public ParticipateLive()
        {
            InitializeComponent();
            this.Loaded += (e, a) =>
            {
                this.DataContext = (ParticipateLiveVM)App.ApplicationVM;
            };
        }

        private void GoBack(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}