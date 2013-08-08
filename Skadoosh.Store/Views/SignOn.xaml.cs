using Microsoft.WindowsAzure.MobileServices;
using Skadoosh.Common.DomainModels;
using Skadoosh.Common.ViewModels;
using Skadoosh.Store.Views.Participate;
using Skadoosh.Store.Views.Presenter;
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

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Skadoosh.Store.Views
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class SignOn : Skadoosh.Store.Common.LayoutAwarePage
    {
        private NotifyBase baseVM;

        public SignOn()
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
            baseVM = (NotifyBase)navigationParameter; 
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
                case "btnFaceBook":
                    Login(MobileServiceAuthenticationProvider.Facebook);
                    break;
                case "btnGoogle":
                    Login(MobileServiceAuthenticationProvider.Google);
                    break;
                case "btnTwitter":
                    Login(MobileServiceAuthenticationProvider.Twitter);
                    break;
                case "btnWindows":
                    Login(MobileServiceAuthenticationProvider.MicrosoftAccount);
                    break;
            }
        }

        private async void Login(MobileServiceAuthenticationProvider provider)
        {
            MobileServiceUser user = null;
            while (user == null)
            {
                try
                {
                    await baseVM.AzureClient.LoginAsync(provider);
                    if (baseVM.AzureClient.CurrentUser != null)
                    {
                        var vmName = baseVM.GetType().Name;
                        if (vmName == "ParticipateStaticVM")
                        {

                            //Frame.Navigate(typeof(SurveySelect), baseVM);
                        }
                        else
                        {
                            var profile = await baseVM.ProfileExists();
                            if (profile)
                            {
                                Frame.Navigate(typeof(SurveyLibrary), baseVM);
                            }
                            else
                            {
                                Frame.Navigate(typeof(PresenterProfile), baseVM);
                            }
                        }
                        
                    }

                }
                catch (InvalidOperationException e)
                {
                    //message = e + "You must log in. Login Required";
                }

            }

        }
    }
}
