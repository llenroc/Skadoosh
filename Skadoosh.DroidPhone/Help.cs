using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace skadoosh.DroidPhone
{
    [Activity(Label = "Skadoosh Help", Icon = "@drawable/ic_launcher")]
    public class Help : ActivityBase
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Help);
            this.SetOrientationBackground(Resource.Id.HelpFrame);
            // Create your application here
        }
    }
}