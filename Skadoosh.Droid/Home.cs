using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Skadoosh.Common.ViewModels;

namespace Skadoosh.Droid
{
    [Activity(Label = "Skadoosh", MainLauncher = true, Icon = "@drawable/ic_launcher")]
    public class Home : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            try
            {
               
                SetContentView(Resource.Layout.Home);

                //var liveButton = FindViewById<Button>(Resource.Id.btnLive);
                //liveButton.Click += (e, a) =>
                //{
                //    var intent = new Intent(this, typeof(SelectSurveyActivity));
                //    intent.PutExtra("VM", "ParticipateLiveVM");
                //    StartActivity(intent);
                //};
                //var staticButton = FindViewById<Button>(Resource.Id.btnStatic);
                //staticButton.Click += (e, a) =>
                //{
                //    var intent = new Intent(this, typeof(SelectSurveyActivity));
                //    intent.PutExtra("VM", "ParticipateStaticVM");
                //    StartActivity(intent);
                //};
            }
            catch (Exception ex)
            {
                var m = ex.Message;
            }

        }
    }
}

