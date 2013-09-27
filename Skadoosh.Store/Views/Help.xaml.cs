using MyToolkit.Multimedia;
using Skadoosh.Store.Common;
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

namespace Skadoosh.Store.Views
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class Help : Skadoosh.Store.Common.LayoutAwarePage
    {
        private string videoID = "irUgRmFlVf0";
        public Help()
        {
            this.InitializeComponent();
            this.Loaded += (e, a) =>
            {
                try
                {
                    var ht = this.itemGridView.ActualHeight - 20;
                    var ratio = (double)480 / (double)800;
                    var wd = Math.Round(ht / ratio);

                    string html = string.Format(@"<iframe width=""{0}"" height=""{1}"" src=""http://www.youtube.com/embed/{2}?rel=0"" frameborder=""0"" allowfullscreen></iframe>", wd, ht, videoID);
                    this.youTube.Width = wd + 20;
                    this.youTube.Height = ht + 20;
                    this.youTube.NavigateToString(html);         
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
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
    }
    public class YoutubeItem : BindableBase
    {
        private int _width;

        private int _height;

        private int _frameBorder;

        private string _source;

        /// <summary> 
        /// Gets or sets the width. 
        /// </summary> 
        /// <value> 
        /// The width. 
        /// </value> 
        public int Width
        {
            get
            {
                return this._width;
            }
            set
            {
                this.SetProperty(ref _width, value);
            }
        }

        /// <summary> 
        /// Gets or sets the height. 
        /// </summary> 
        /// <value> 
        /// The height. 
        /// </value> 
        public int Height
        {
            get
            {
                return this._height;
            }
            set
            {
                this.SetProperty(ref _height, value);
            }
        }

        /// <summary> 
        /// Gets or sets the frame border. 
        /// </summary> 
        /// <value> 
        /// The frame border. 
        /// </value> 
        public int FrameBorder
        {
            get
            {
                return this._frameBorder;
            }
            set
            {
                this.SetProperty(ref _frameBorder, value);
            }
        }

        /// <summary> 
        /// Gets or sets the source. 
        /// </summary> 
        /// <value> 
        /// The source. 
        /// </value> 
        public string Source
        {
            get
            {
                return this._source;
            }
            set
            {

                this.SetProperty(ref _source, value);
            }
        }

        /// <summary> 
        /// Gets the content. 
        /// </summary> 
        public string Content
        {
            get
            {
                return
                    string.Format(
                        @"<iframe width='{0}' height='{1}' src='{2}' frameborder='{3}'></iframe>",
                        this.Width,
                        this.Height,
                        this.Source,
                        this.FrameBorder);
            }
        }
    } 

}
