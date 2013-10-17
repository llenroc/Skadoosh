using System.Collections.Specialized;
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
using System.ComponentModel;


namespace Skadoosh.Store.Views
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class SignOn : Skadoosh.Store.Common.LayoutAwarePage, INotifyPropertyChanged
    {
        private PresenterVM vm;
        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; Notify("IsBusy"); }
        }

        public SignOn()
        {
            this.InitializeComponent();
            this.Loaded += (e, a) =>
            {

                this.DataContext = this;
            };

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
            vm = (PresenterVM)navigationParameter;
            vm.ErrorMessage = string.Empty;
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
                    vm.ExpLogin.AccountFileName = "SkadooshFaceBook";
                    Login(MobileServiceAuthenticationProvider.Facebook);
                    break;
                case "btnGoogle":
                    vm.ExpLogin.AccountFileName = "SkadooshFaceBook";
                    Login(MobileServiceAuthenticationProvider.Google);
                    break;
                case "btnTwitter":
                    vm.ExpLogin.AccountFileName = "SkadooshFaceBook";
                    Login(MobileServiceAuthenticationProvider.Twitter);
                    break;
                case "btnWindows":
                    vm.ExpLogin.AccountFileName = "SkadooshFaceBook";
                    Login(MobileServiceAuthenticationProvider.MicrosoftAccount);
                    break;
            }
        }

        private async void Login(MobileServiceAuthenticationProvider provider)
        {

            try
            {

                await vm.AzureClient.LoginAsync(provider);
            
                IsBusy = true;

                if (vm.AzureClient.CurrentUser != null)
                {
                    var profile = await vm.ProfileExists();
                    IsBusy = false;
                    if (profile)
                    {
                        Frame.Navigate(typeof(SurveyLibrary), vm);
                    }
                    else
                    {
                        Frame.Navigate(typeof(PresenterProfile), vm);
                    }
                }
                IsBusy = false;
            }
            catch (InvalidOperationException e)
            {
                IsBusy = false;
                //message = e + "You must log in. Login Required";
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Notify(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        private void ExpressLogin(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ExpressLogin), vm);
        }
    }
}
