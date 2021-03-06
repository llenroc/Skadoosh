﻿
using Skadoosh.Common.DomainModels;
using Skadoosh.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.ApplicationSettings;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Skadoosh.Store.Views.Participate
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class ParticipateStatic : Skadoosh.Store.Common.LayoutAwarePage
    {
        private ParticipateStaticVM VM
        {
            get { return (ParticipateStaticVM)this.DataContext; }
            set { this.DataContext = value; }
        }

        public ParticipateStatic()
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
            VM = (ParticipateStaticVM)navigationParameter;
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

        private async void GoToHome(object sender, RoutedEventArgs e)
        {
            var msg = new MessageDialog("You are exiting the survey. Do you want to save the results, cancel or return to the survey?", "Exit Survey Notification");
            msg.Commands.Add(new UICommand("Save", async(a) => {
                await VM.SaveSurveyResponses();
                Frame.Navigate(typeof(Home), VM);
            }));
            msg.Commands.Add(new UICommand("Cancel", (a) => {
                Frame.Navigate(typeof(Home), VM);
            }));
            msg.Commands.Add(new UICommand("Return to Survey", (a) => { 
              
            }));
            await msg.ShowAsync();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var items = ((ListView)sender).SelectedItems;
            foreach (var opt in VM.CurrentQuestion.Options)
            {
                opt.IsSelected = false;
            }
            foreach(var item in items){
                var opt = (Option)item;
                VM.CurrentQuestion.Options.First(x=>x.Id==opt.Id).IsSelected=true;
            } 
        }

        private void ShowHelp(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Help), new ParticipateStaticVM());
        }

        private void BackQuestion(object sender, RoutedEventArgs e)
        {
            VM.BackQuestion();
        }

        private void ForwardQuestion(object sender, RoutedEventArgs e)
        {
            VM.NextQuestion();
        }
    }
}
