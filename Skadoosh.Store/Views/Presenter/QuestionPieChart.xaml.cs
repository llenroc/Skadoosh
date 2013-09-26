using Windows.UI.Core;
using Windows.UI.Xaml.Printing;
using Skadoosh.Common.DomainModels;
using Skadoosh.Common.ViewModels;
using Skadoosh.Store.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Controls;
using WinRTXamlToolkit.Composition;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;
using Windows.Graphics.Printing;
using Windows.UI.Xaml;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Skadoosh.Store.Views.Presenter
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class QuestionPieChart : Skadoosh.Store.Common.LayoutAwarePage
    {
        private PrintManager _printManager;
        private PresenterVM VM
        {
            get { return (PresenterVM)this.DataContext; }
            set { this.DataContext = value; }
        }
        public QuestionPieChart()
        {
            this.InitializeComponent();
            this.Loaded += async (e, a) =>
            {
                if (VM.CurrentQuestion != null)
                {
                    var list = await VM.GetResponsesForCurrentQuestion();
                    CalculatePieChart(list);
                    RegisterPrinter();
                }
            };
        }

        public void RegisterPrinter()
        {
            var pd = new PrintDocument();
            pd.Paginate += (sender, args) =>
            {
                // Set the number of pages to preview
                pd.SetPreviewPageCount(1, PreviewPageCountType.Final);
            };
            pd.AddPages += (sender, args) =>
            {
                // Add a page/document to the print list
                pd.AddPage(this);
                pd.AddPagesComplete();
            };
            pd.GetPreviewPage += (sender, args) =>
            {
                // Indicate the page number to display in the preview window
                pd.SetPreviewPage(args.PageNumber, this);
            };

            _printManager = PrintManager.GetForCurrentView();
            _printManager.PrintTaskRequested += (sender, args) =>
            {
                var printTask = args.Request.CreatePrintTask("My First WinRT Impression ",
                    async (requestedArgs) =>
                    {
                        Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                        {
                            requestedArgs.SetSource(pd.DocumentSource);
                        });

                    });

                printTask.Options.Orientation = PrintOrientation.Landscape;

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
        private void CalculatePieChart(List<Responses> list)
        {
            var items = new List<NameValueItem>();
            foreach (var opt in VM.CurrentQuestion.Options)
            {
                var cnt = list.Count(x => x.OptionId == opt.Id);
                items.Add(new NameValueItem { Name = opt.OptionText, Value = cnt });
            }

            ((PieSeries)this.PieChart.Series[0]).ItemsSource = items;
        }

        private async void RefreshData(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (VM.CurrentQuestion != null)
            {
                ((ColumnSeries)this.PieChart.Series[0]).ItemsSource = null;
                var list = await VM.GetResponsesForCurrentQuestion();
                CalculatePieChart(list);
            }
        }

        private async void PrintChart(object s, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await PrintManager.ShowPrintUIAsync();
        }


    }
}
