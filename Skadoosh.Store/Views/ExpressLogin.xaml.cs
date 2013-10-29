using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237
using Skadoosh.Common.ViewModels;
using Skadoosh.Store.Views.Presenter;
using Windows.UI.ApplicationSettings;
using Windows.System;

namespace Skadoosh.Store.Views
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class ExpressLogin : Skadoosh.Store.Common.LayoutAwarePage
    {
        private PresenterVM vm;

        public ExpressLogin()
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
            vm = (PresenterVM)navigationParameter;
            vm.ErrorMessage = string.Empty;
            this.DataContext = vm;
            this.BottomAppBar.IsOpen = true;
            this.BottomAppBar.IsSticky = true;
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

        private async void Login(object sender, RoutedEventArgs e)
        {
            DoExpLogin();
        }

        private void enterKeyed(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter  && vm.IsBusy==false)
            {
                DoExpLogin();
            }
        }

        private async void DoExpLogin()
        {
            var msu = await vm.GetExpressLoginClient();
            if (msu != null)
            {
                vm.AzureClient.CurrentUser = msu;
                var profile = await vm.ProfileExists();
                if (profile)
                {
                    Frame.Navigate(typeof(SurveyLibrary), vm);
                }
                else
                {
                    Frame.Navigate(typeof(PresenterProfile), vm);
                }
            }
            else
            {
                vm.ErrorMessage = "Express login does not exist";
            }
        }
    }
}
