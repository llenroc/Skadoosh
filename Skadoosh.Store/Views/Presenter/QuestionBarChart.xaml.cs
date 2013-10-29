using Windows.UI.Core;
using Windows.UI.Xaml.Printing;
using Skadoosh.Common.ViewModels;
using Skadoosh.Store.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Graphics.Printing;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;





// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237


namespace Skadoosh.Store.Views.Presenter
{


    public sealed partial class QuestionBarChart : BasePrintPage
    {
        private string url = "http://www.azdevelop.net/skadoosh/Chart/BarChart/{0}?w={1}&h={2}";

        private PresenterVM vm;

        public QuestionBarChart()
        {
            this.InitializeComponent();
            this.Loaded += (e, a) =>
            {
                var vm = (PresenterVM)VM;
                if (vm.CurrentQuestion != null)
                {
                    var ht = itemListView.ActualHeight;
                    var wd = itemListView.ActualWidth;
                    var imgUrl = string.Format(url, vm.CurrentQuestion.Id, wd, ht);

                    vm.IsBusy = true;
                    BarChart.ImageOpened += BarChart_ImageOpened;
                    BarChart.Source = new BitmapImage(new Uri(imgUrl));
                   
                }
                this.BottomAppBar.IsOpen = true;
                this.BottomAppBar.IsSticky = true;
            };

        }

        void BarChart_ImageOpened(object sender, RoutedEventArgs e)
        {
            vm.IsBusy = false;
        }

        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private void RefreshData(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (vm.CurrentQuestion != null)
            {
                vm.IsBusy = true;
                var ht = itemListView.ActualHeight;
                var wd = itemListView.ActualWidth;
                var imgUrl = string.Format(url, vm.CurrentQuestion.Id, wd, ht);
                BarChart.Source = new BitmapImage(new Uri(imgUrl));
            }
        }
        protected override void PreparetPrintContent()
        {
            if (firstPage == null)
            {
                vm = (PresenterVM)VM;
                vm.IsBusy = true;
                var ht = 400; // itemListView.ActualHeight;
                var wd = 300; // itemListView.ActualWidth;
                var imgUrl = string.Format(url, vm.CurrentQuestion.Id, wd, ht);
                firstPage = new ReportPrint() { ImageUrl = imgUrl };
            }
            PrintingRoot.Children.Add(firstPage);
            PrintingRoot.InvalidateMeasure();
            PrintingRoot.UpdateLayout();
        }
        private async void PrintChart(object s, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await PrintManager.ShowPrintUIAsync();
        }
        private void Logout(object sender, RoutedEventArgs e)
        {
            vm.Logout();
            Frame.Navigate(typeof(Home), vm);
        }
        private void ShowHelp(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Help), new ParticipateStaticVM());
        }
    }
}

