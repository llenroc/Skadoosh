using Skadoosh.Common.DomainModels;
using Skadoosh.Common.ViewModels;
using Skadoosh.Store.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Skadoosh.Common.Util;
using Windows.Storage;
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
        protected override async void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            VM = (PresenterVM)navigationParameter;
            VM.ErrorMessage = string.Empty;
            await VM.LoadSurveysForCurrentUser();
            if(!VM.SurveyCollection.Any())
                CollectionIsEmptyNotification();
        }


        private void CreateSurvey(object sender, RoutedEventArgs e)
        {
            VM.CurrentSurvey = new Survey() { AccountUserId = VM.User.Id };
            Frame.Navigate(typeof(EditSurvery), VM);
        }


        private async void CollectionIsEmptyNotification()
        {
            var messageDialog = new MessageDialog("There are no surveys created in this library. Open the application bar up to create a survey","Empty Library");
            messageDialog.Commands.Add(new UICommand(
                "I Understand",
                new UICommandInvokedHandler(this.CommandInvokedHandler)));
            messageDialog.DefaultCommandIndex = 0;
            messageDialog.CancelCommandIndex = 1;
            await messageDialog.ShowAsync();
        }

        private void CommandInvokedHandler(IUICommand command)
        {
           

        }

        private void SurveySelected(object sender, DoubleTappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(SurveyQuestions), VM);
        }

        private void EditSurvey(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EditSurvery), VM);
        }

        private async void DeleteSurvey(object sender, RoutedEventArgs e)
        {
            var msg = new MessageDialog("Are you sure you want to delete this survey?", "Delete Verification");
            msg.Commands.Add(new UICommand("Proceed", async (a) =>
            {
                await VM.DeleteCurrentSurvey().ConfigureAwait(true);
                if (!VM.SurveyCollection.Any())
                    CollectionIsEmptyNotification();
            }));
            msg.Commands.Add(new UICommand("Cancel", (a) =>
            {

            }));

            await msg.ShowAsync();
        }

        private void StartSurvey(object sender, RoutedEventArgs e)
        {
            VM.StartSurvey();
        }

        private void StopSurvey(object sender, RoutedEventArgs e)
        {
            VM.StopSurvey();
        }

        private void ShowAppBar(object sender, RightTappedRoutedEventArgs e)
        {
            bottomAppBar.IsOpen = true;
        }

        private void GoToHome(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Home), VM);
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            VM.Logout();
            Frame.Navigate(typeof(Home), VM);
        }

        private async void ExportData(object sender, RoutedEventArgs e)
        {
            var list = await VM.GetResponseCSVForCurrentSurvey();
            var exporter = new CsvExport<ResponseCSV>(list);
            var content = exporter.ExportToString();
            var storageFolder = KnownFolders.DocumentsLibrary;
            var fileName = VM.CurrentSurvey.SurveyTitle.Replace(" ", string.Empty) + ".csv";
            var file = await storageFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, content);


            var msg = new MessageDialog("The document was saved to your document library. Do you want to view it?", "Document Saved");
            msg.Commands.Add(new UICommand("Preview File", async (a) =>
            {
                var newFile = await storageFolder.GetFileAsync(fileName);
                Windows.System.Launcher.LaunchFileAsync(newFile);
            }));
            msg.Commands.Add(new UICommand("No", (a) =>
            {

            }));
            await msg.ShowAsync();

        }
        private void ShowHelp(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Help), new ParticipateStaticVM());
        }

        private void UpdateProfile(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PresenterProfile), VM);
        }

    }
}
