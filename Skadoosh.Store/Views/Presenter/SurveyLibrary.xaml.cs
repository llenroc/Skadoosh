using Skadoosh.Common.DomainModels;
using Skadoosh.Common.ViewModels;
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

// The Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234233

namespace Skadoosh.Store.Views.Presenter
{
    /// <summary>
    /// A page that displays a collection of item previews.  In the Split Application this page
    /// is used to display and select one of the available groups.
    /// </summary>
    public sealed partial class SurveyLibrary : Skadoosh.Store.Common.LayoutAwarePage
    {
        private PresenterVM VM
        {
            get { return (PresenterVM)this.DataContext; }
            set { this.DataContext = value; }
        }

        public SurveyLibrary()
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
            VM = (PresenterVM)navigationParameter; 
        }

        private void CreateSurvey(object sender, RoutedEventArgs e)
        {
            var x = "Here";
        }


        private void SurveySelected(object sender, TappedRoutedEventArgs e)
        {
            VM.CurrentSurvey = ((Grid)sender).DataContext as Survey;
            VM.LoadQuestionsForCurrentSurvey();
            Frame.Navigate(typeof(QuestionView), VM);
        }
    }
}
