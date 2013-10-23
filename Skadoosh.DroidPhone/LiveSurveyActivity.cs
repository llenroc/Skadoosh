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
using Skadoosh.Common.ViewModels;

namespace Skadoosh.DroidPhone
{
    [Activity(Label = "Live Survey", Icon = "@drawable/ic_launcher")]
    public class LiveSurveyActivity : Activity
    {
        private ParticipateLiveVM VM;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.LiveSurvey);
            VM = (ParticipateLiveVM)AppModel.VM;
            // Create your application here
        }
    }
}