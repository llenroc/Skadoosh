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
        private ProgressDialog progress;


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

            if (AppModel.GCMNote != null)
            {
                CleanGCMNote();
            }
            ShowLoading();
            AppModel.GCMNote = new GCMNotifier();
            AppModel.GCMNote.RegistrationUpdated += GCMNote_RegistrationUpdated;
            PushClient.Register(this, PushHandlerBroadcastReceiver.SENDER_IDS);
        }

        private void CleanGCMNote()
        {
            AppModel.GCMNote.RegistrationUpdated -= GCMNote_RegistrationUpdated;
            AppModel.GCMNote = null;
        }

        void GCMNote_RegistrationUpdated(string registrationId)
        {
            progress.Dismiss();
            CleanGCMNote();
        }

        private void ShowLoading()
        {
            progress = new ProgressDialog(this);
            progress.Indeterminate = true;
            progress.SetProgressStyle(ProgressDialogStyle.Spinner);
            progress.SetMessage("Preparing Question Notifications. Please wait...");
            progress.SetCancelable(false);
            progress.Show();
        }

    }
}