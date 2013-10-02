using Skadoosh.Common.DomainModels;
using Skadoosh.Common.ViewModels;
using Skadoosh.Store.Views.Participate;
using Skadoosh.Store.Views.Presenter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Channels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Connectivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Skadoosh.Store.Views
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class Home : Skadoosh.Store.Common.LayoutAwarePage
    {
        private ViewModelBase baseVM;

        public Home()
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
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            if(navigationParameter!=null && navigationParameter!="")
                baseVM = (ViewModelBase)navigationParameter;

            if (!InternetConnected)
            {
                errorMsg.Visibility = Windows.UI.Xaml.Visibility.Visible;
                gridButtons.IsEnabled = false;
            }
            else
            {
                errorMsg.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                gridButtons.IsEnabled = true;
            }
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

        private void itemTapped(object sender, TappedRoutedEventArgs e)
        {
            switch (((Grid)sender).Name)
            {
                case "btnPresenter":
                    if (baseVM != null && baseVM.IsLoggedOn)
                    {
                        Frame.Navigate(typeof(SurveyLibrary), baseVM);
                    }
                    else
                    {
                        Frame.Navigate(typeof(SignOn), new PresenterVM());
                    }
                    break;
                case "btnParticipate":
                    Frame.Navigate(typeof(SelectSurvey), new ParticipateLiveVM());
                    break;
                case "btnGroupInvite":
                    Frame.Navigate(typeof(SelectSurvey), new ParticipateStaticVM());
                    break;
            }
        }

        private void ShowHelp(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Help), new ParticipateStaticVM());
        }


        private bool InternetConnected
        {
            get
            {            
                var profile = NetworkInformation.GetInternetConnectionProfile();
                if (profile == null)
                    return false;
                if (profile.GetNetworkConnectivityLevel() != NetworkConnectivityLevel.InternetAccess)
                {
                    return false;
                }
                return true;
            }
        }


    }
}
