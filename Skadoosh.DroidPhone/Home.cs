using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Skadoosh.DroidPhone
{
    [Activity(Label = "Skadoosh", Icon = "@drawable/ic_launcher")]
    public class Home : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Home);


        }
    }
}

