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
using skadoosh.DroidPhone;
using Skadoosh.Common.ViewModels;
using PushSharp.Client;
using Android.Util;

namespace skadoosh.DroidPhone
{
    [Activity(Label = "Live Survey", Icon = "@drawable/ic_launcher")]
    public class LiveSurveyActivity : ActivityBase
    {
        const string TAG = "PushSharp-GCM";
        private ParticipateLiveVM VM;
        private bool registered = false;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.LiveSurvey);
            this.SetOrientationBackground(Resource.Id.LiveSurvey);
            VM = (ParticipateLiveVM)AppModel.VM;
            PushClient.CheckDevice(this);
            PushClient.CheckManifest(this);
          
            var txtGCM = FindViewById<TextView>(Resource.Id.txtGMCCllient);
            var txtPush = FindViewById<TextView>(Resource.Id.txtPushClient);

            if (!registered)
            {
                Log.Info(TAG, "Registering...");

                //Call to register
                PushClient.Register(this, PushHandlerBroadcastReceiver.SENDER_IDS);
                var registrationId = PushClient.GetRegistrationId(this);
                txtGCM.Text = "GCMID-> " + registrationId;
            }
            else
            {
                Log.Info(TAG, "Unregistering...");

                //Call to unregister
                PushClient.UnRegister(this);
            }

        }
    }
}