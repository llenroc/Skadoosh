﻿using Skadoosh.Common.DomainModels;
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

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Skadoosh.Store.Views.Presenter
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class EditQuestion : Skadoosh.Store.Common.LayoutAwarePage
    {
        private PresenterVM VM
        {
            get { return (PresenterVM)this.DataContext; }
            set { this.DataContext = value; }
        }

        public EditQuestion()
        {
            this.InitializeComponent();
            this.BottomAppBar.IsOpen = true;
            this.BottomAppBar.IsSticky = true;
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
            VM.ErrorMessage = string.Empty;
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

        private async void SaveQuestion(object sender, RoutedEventArgs e)
        {
            await VM.UpdateQuestion(); ;
            Frame.GoBack();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            VM.CurrentQuestion.Options.Add(new Skadoosh.Common.DomainModels.Option() { QuestionId = VM.CurrentQuestion.Id });
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            VM.Logout();
            Frame.Navigate(typeof(Home), VM);
        }
        private void ShowHelp(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Help), new ParticipateStaticVM());
        }

        private void deleteOption(object sender, RoutedEventArgs e)
        {
            Button b = (Button)e.OriginalSource;
            Option o = (Option)b.DataContext;
            o.IsDeleted = !o.IsDeleted;
        }
    }
}
