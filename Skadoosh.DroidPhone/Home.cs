using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;


namespace skadoosh.DroidPhone
{

    [Activity(Label = "skadoosh", Icon = "@drawable/ic_launcher")]
    public class Home : ActivityBase
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Home);
            this.SetOrientationBackground(Resource.Id.HomeFrame);

            var liveButton = FindViewById<ImageButton>(Resource.Id.btnLive);
            liveButton.Click += (e, a) =>
            {
                var intent = new Intent(this, typeof(SelectSurveyActivity));
                intent.PutExtra("VM", "ParticipateLiveVM");
                StartActivity(intent);
            };
            var staticButton = FindViewById<ImageButton>(Resource.Id.btnStatic);
            staticButton.Click += (e, a) =>
            {
                var intent = new Intent(this, typeof(SelectSurveyActivity));
                intent.PutExtra("VM", "ParticipateStaticVM");
                StartActivity(intent);
            };

        }
    }
}

