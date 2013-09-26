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

    public sealed partial class QuestionPieChart : Skadoosh.Store.Common.LayoutAwarePage
    {
        private PrintManager _printManager;
        private PrintDocument _doc;
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
                    _printManager = PrintManager.GetForCurrentView();
                    _printManager.PrintTaskRequested += _printManager_PrintTaskRequested;
                    InitDocument();
                }
            };
            this.Unloaded += (e, a) =>
            {
                _printManager = PrintManager.GetForCurrentView();
                _printManager.PrintTaskRequested -= _printManager_PrintTaskRequested;
                _doc = null;
            };
        }

        private void InitDocument()
        {
            _doc = new PrintDocument();
            _doc.Paginate += (sender, args) =>
            {
                _doc.SetPreviewPageCount(1, PreviewPageCountType.Final);
            };
            _doc.AddPages += (sender, args) =>
            {
                _doc.AddPage(this);
                _doc.AddPagesComplete();
            };
            _doc.GetPreviewPage += (sender, args) =>
            {
                _doc.SetPreviewPage(args.PageNumber, this);
            };
        }
        private void _printManager_PrintTaskRequested(PrintManager sender, PrintTaskRequestedEventArgs args)
        {
            var printTask = args.Request.CreatePrintTask("Pie Chart Survey Results ",
                async (requestedArgs) =>
                {
                    Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        requestedArgs.SetSource(_doc.DocumentSource);
                    });

                });

            printTask.Options.Orientation = PrintOrientation.Landscape;
        }


        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            VM = (PresenterVM)navigationParameter;
        }


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
