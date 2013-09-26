using SharpDX.Direct2D1;
using Skadoosh.Common.DomainModels;
using Skadoosh.Common.ViewModels;
using Skadoosh.Store.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;
using WinRTXamlToolkit.Composition;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Graphics.Printing;


// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Skadoosh.Store.Views.Presenter
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class QuestionBarChart : Skadoosh.Store.Common.LayoutAwarePage
    {
        private PresenterVM VM
        {
            get { return (PresenterVM)this.DataContext; }
            set { this.DataContext = value; }
        }
        public QuestionBarChart()
        {
            this.InitializeComponent();
            this.Loaded += async (e, a) =>
            {
                if (VM.CurrentQuestion != null)
                {
                    var list = await VM.GetResponsesForCurrentQuestion();
                    CalculateBarChart(list);
                }
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
            VM = (PresenterVM)navigationParameter;
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
        private void CalculateBarChart(List<Responses> list)
        {

            var items = new List<NameValueItem>();
            foreach (var opt in VM.CurrentQuestion.Options)
            {
                var cnt = list.Count(x => x.OptionId == opt.Id);
                items.Add(new NameValueItem { Name = opt.OptionText, Value = cnt });
            }
   
            ((ColumnSeries)this.BarChart.Series[0]).ItemsSource = items;
        }

        private async void RefreshData(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (VM.CurrentQuestion != null)
            {
                ((ColumnSeries) this.BarChart.Series[0]).ItemsSource = null;
                var list = await VM.GetResponsesForCurrentQuestion();
                CalculateBarChart(list);
            }
        }

        private async void PrintChart()
        {
            var bitmap = await WriteableBitmapRenderExtensions.Render(this.BarChart);
          


        }


    }
}
