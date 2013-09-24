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
    public partial class ParticipateStatic : PhoneApplicationPage
    {
        private ParticipateStaticVM VM
        {
            get { return (ParticipateStaticVM) this.DataContext; }
        }

        public ParticipateStatic()
        {
            InitializeComponent();
            this.Loaded += (e, a) =>
            {
                this.DataContext = (ParticipateStaticVM)App.ApplicationVM;
            };
        }

        private void GoBack(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}