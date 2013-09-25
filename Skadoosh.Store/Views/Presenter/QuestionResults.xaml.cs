using Windows.UI;
using Windows.UI.Xaml.Media;
using Skadoosh.Common.DomainModels;
using Skadoosh.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Controls;

using WinRTXamlToolkit.Controls.DataVisualization.Charting;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Skadoosh.Store.Views.Presenter
{
    public class NameValueItem
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }

    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class QuestionResults : Skadoosh.Store.Common.LayoutAwarePage
    {
        private Random _random = new Random();

        private PresenterVM VM
        {
            get { return (PresenterVM)this.DataContext; }
            set { this.DataContext = value; }
        }

        public QuestionResults()
        {
            this.InitializeComponent();
            this.Loaded += async (e, a) =>
            {
                if (VM.CurrentQuestion != null)
                {
                    var list = await VM.GetResponsesForCurrentQuestion();
                    if (VM.CurrentQuestion.IsMultiSelect)
                    {
                        CalculateBarChart(list);
                    }
                    else
                    {
                        CalculatePieChart(list);
                    }
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

        private void CalculatePieChart(List<Responses> list )
        {
            var items = new List<NameValueItem>();
            foreach (var opt in VM.CurrentQuestion.Options)
            {
                var cnt = list.Count(x => x.OptionId == opt.Id);
               // var percent = ((double)cnt / (double)list.Count) * (double)100;
                items.Add(new NameValueItem { Name = opt.OptionText, Value = cnt });
            }

            ((PieSeries)this.PieChart.Series[0]).ItemsSource = items;
        }
        private void CalculateBarChart(List<Responses> list)
        {
            
        }
    }
}
