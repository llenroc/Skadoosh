﻿using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using Skadoosh.Common.ViewModels;
using Skadoosh.Common.DomainModels;

namespace Skadoosh.Phone.Views
{
    public partial class ParticipateStatic : PhoneApplicationPage
    {
        private ParticipateStaticVM VM
        {
            get { return (ParticipateStaticVM) this.DataContext; }
        }

        public ParticipateStatic()
        {
            InitializeComponent();
            this.Loaded += (e, a) =>
            {
                this.DataContext = (ParticipateStaticVM)App.ApplicationVM;
            };
        }

        private void GoBack(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void NextQuestion(object sender, EventArgs e)
        {
            VM.NextQuestion();
        }


        private void BackQuestion(object sender, EventArgs e)
        {
            VM.BackQuestion();
        }

        private void ItemTapped(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var opt = (Option)((StackPanel)sender).DataContext;
            if (VM.CurrentQuestion.IsMultiSelect)
            {
                if (opt.IsSelected)
                    opt.IsSelected = false;
                else
                    opt.IsSelected = true;
            }
            else
            {
                foreach (var obj in VM.CurrentQuestion.Options)
                {
                    obj.IsSelected = false;
                }
                opt.IsSelected = true;
            }

        }

        private async void GoToHome(object sender, System.Windows.Input.GestureEventArgs e)
        {
            StopAndSaveSurvey();
        }

        private void QuitSurvey(object sender, EventArgs e)
        {
            StopAndSaveSurvey();
        }

        private async void StopAndSaveSurvey()
        {
            string message = "Do you want to save and quit the survey or return?";
            string caption = "Exiting Survey";
            var buttons = MessageBoxButton.OKCancel;
            MessageBoxResult result = MessageBox.Show(message, caption, buttons);
            if (result != MessageBoxResult.Cancel)
            {
                await VM.SaveSurveyResponses();
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            }

        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/Help.xaml", UriKind.Relative));
        }
    }
}