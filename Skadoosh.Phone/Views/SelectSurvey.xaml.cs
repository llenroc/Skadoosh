using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Skadoosh.Common.DomainModels;
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            //if (NavigationContext.QueryString.ContainsKey("obj"))
            //{
            //    if (NavigationContext.QueryString["obj"] == "1")
            //    {
            //        var vm = new ParticipateLiveVM();
            //        this.DataContext = vm;
            //        App.ApplicationVM = vm;
            //    }
            //    else
            //    {
            //        var vm = new ParticipateStaticVM();
            //        this.DataContext = vm;
            //        App.ApplicationVM = vm;
            //    }
            //}
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
    }
}