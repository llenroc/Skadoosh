using Skadoosh.Common.DomainModels;
using Skadoosh.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;


// The Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234233

namespace Skadoosh.Store.Views.Presenter
{
    /// <summary>
    /// A page that displays a collection of item previews.  In the Split Application this page
    /// is used to display and select one of the available groups.
    /// </summary>
    public sealed partial class SurveyQuestions : Skadoosh.Store.Common.LayoutAwarePage
    {
        private PresenterVM VM
        {
            get { return (PresenterVM)this.DataContext; }
            set { this.DataContext = value; }
        }

        public SurveyQuestions()
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
            //await VM.LoadQuestionsForCurrentSurvey();
            if (!VM.CurrentSurvey.Questions.Any())
                CollectionIsEmptyNotification();
        }

        private async void CollectionIsEmptyNotification()
        {
            var messageDialog = new MessageDialog("There are no question created for this survey. Open the application bar up to create questions", "No Questions");
            messageDialog.Commands.Add(new UICommand("I Understand", new UICommandInvokedHandler((command) => { })));
            messageDialog.DefaultCommandIndex = 0;
            messageDialog.CancelCommandIndex = 1;
            await messageDialog.ShowAsync();
        }

        //private void CommandInvokedHandler(IUICommand command)
        //{


        //}
        private void CreateQuestion(object sender, RoutedEventArgs e)
        {
            VM.CurrentQuestion = new Question() { SurveyId = VM.CurrentSurvey.Id };
            Frame.Navigate(typeof(EditQuestion), VM);
        }

        private void EditQuestion(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EditQuestion), VM);
        }

        private async void DeleteQuestion(object sender, RoutedEventArgs e)
        {
            var msg = new MessageDialog("Are you sure you want to delete this question?", "Delete Verification");
            msg.Commands.Add(new UICommand("Proceed", async (a) =>
            {
                VM.DeleteCurrentQuestion();
            }));
            msg.Commands.Add(new UICommand("Cancel", (a) =>
            {

            }));

            await msg.ShowAsync();
        }

        private void SetActive(object sender, RoutedEventArgs e)
        {
            VM.SetQuestionActive();
        }

        private void ShowAppBar(object sender, RightTappedRoutedEventArgs e)
        {
            BottomAppBar.IsOpen = true;
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            VM.Logout();
            Frame.Navigate(typeof(Home), VM);
        }

        private void ViewResults(object sender, RoutedEventArgs e)
        {
            if (VM.CurrentQuestion != null)
            {
                if (VM.CurrentQuestion.IsMultiSelect)
                {
                    Frame.Navigate(typeof(QuestionBarChart), VM);
                }
                else
                {
                    Frame.Navigate(typeof(QuestionPieChart), VM);
                }
            }
            
        }
        private void ShowHelp(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Help), new ParticipateStaticVM());
        }
    }
}
