using Skadoosh.Common.DomainModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Skadoosh.Store.Views.Participate
{
    public sealed partial class SelectSurvey : UserControl
    {
        public event EventHandler PopupClosing;

        public bool IsCancel { get; set; }
        public SelectSurvey()
        {
            this.InitializeComponent();
        }

        public void ShowLogin(ParticipateBase vm)
        {
            this.message.Visibility = Visibility.Collapsed;
            this.DataContext = vm;
            this.logincontrol1.IsOpen = true;
        }

        private void tapCancel(object sender, TappedRoutedEventArgs e)
        {
            if (PopupClosing != null)
            {
                this.logincontrol1.IsOpen = false;
                IsCancel = true;
                PopupClosing(null, null);
            }
        }

        private async void tapSelected(object sender, TappedRoutedEventArgs e)
        {

            var vm = (ParticipateBase)this.DataContext;
            if (!string.IsNullOrEmpty(vm.ChannelSelected))
            {
                var result = await vm.FindSurvey();
                if (result && PopupClosing != null)
                {
                    IsCancel = false;
                    this.logincontrol1.IsOpen = false;
                    PopupClosing(null, null);
                }
                else
                {
                    this.message.Visibility = Visibility.Visible;
                }

            }
            else
            {
                if (PopupClosing != null)
                {
                    this.logincontrol1.IsOpen = false;
                    IsCancel = true;
                    PopupClosing(null, null);
                }
            }

        }
    }
}
