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
using Skadoosh.Common.DomainModels;
using Skadoosh.Common.ViewModels;

namespace Skadoosh.Droid
{
    [Activity(Label = "Skadoosh", Icon = "@drawable/ic_launcher")]
    public class SelectSurveyActivity : Activity
    {
        private ViewModelBase vm;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.SelectSurvey);

            //switch (Intent.GetStringExtra("VM"))
            //{
            //    case "ParticipateLiveVM":
            //        vm = new ParticipateLiveVM();
            //        break;
            //    case "ParticipateStaticVM":
            //        vm = new ParticipateStaticVM();
            //        break;
            //}
        }
    }
}