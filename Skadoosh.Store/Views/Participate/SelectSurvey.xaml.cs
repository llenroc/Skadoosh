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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Skadoosh.Store.Views.Participate
{
    public sealed partial class SelectSurvey : UserControl
    {
        public SelectSurvey()
        {
            this.InitializeComponent();
        }

        public void ShowLogin()
        {
            this.logincontrol1.IsOpen = true;
        }
    }
}
