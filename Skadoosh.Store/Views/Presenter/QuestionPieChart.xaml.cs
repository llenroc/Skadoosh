using System.Reflection;
using Windows.UI.Core;
using Windows.UI.Xaml.Printing;
using Skadoosh.Common.DomainModels;
using Skadoosh.Common.ViewModels;
using Skadoosh.Store.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Controls;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;
using Windows.Graphics.Printing;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;


// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Skadoosh.Store.Views.Presenter
{

    public sealed partial class QuestionPieChart : BasePrintPage
    {
        private string url = "http://www.azdevelop.net/skadoosh/Chart/pieChart/{0}?w={1}&h={2}";
        private PresenterVM vm;

        public QuestionPieChart()
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
                    PieChart.Source = new BitmapImage(new Uri(imgUrl));        
                }
            };
        }

        private void RefreshData(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (vm.CurrentQuestion != null)
            {
                var ht = itemListView.ActualHeight;
                var wd = itemListView.ActualWidth;
                var imgUrl = string.Format(url, vm.CurrentQuestion.Id, wd, ht);
                PieChart.Source = new BitmapImage(new Uri(imgUrl));
            }
        }
        protected override void PreparetPrintContent()
        {
            if (firstPage == null)
            {
                vm = (PresenterVM)VM;
                var ht = 400;// itemListView.ActualHeight;
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
