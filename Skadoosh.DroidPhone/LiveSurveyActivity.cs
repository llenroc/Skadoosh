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
using System.Threading;

namespace skadoosh.DroidPhone
{



    [Activity(Label = "Live Survey", Icon = "@drawable/ic_launcher")]
    public class LiveSurveyActivity : ActivityBase
    {
        const string TAG = "skadooshclient";
        private ParticipateLiveVM VM;
        private ProgressDialog progress;

        private RadioGroup radioGroup;
        private List<CheckBox> checkboxes;
        private LinearLayout layout;
        private bool isRegistered;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.LiveSurvey);
            this.SetOrientationBackground(Resource.Id.LiveSurvey);
            VM = (ParticipateLiveVM)AppModel.VM;
            layout = FindViewById<LinearLayout>(Resource.Id.liveOptionLayout);

            PushClient.CheckDevice(this);
            PushClient.CheckManifest(this);
            if (AppModel.GCMNote != null)
            {
                CleanGCMNote();
            }
            ShowLoading();
            AppModel.GCMNote = new GCMNotifier();
            AppModel.GCMNote.RegistrationUpdated += GCMNote_RegistrationUpdated;
            PushClient.Register(this, PushHandlerBroadcastReceiver.SENDER_IDS);

            VM.PropertyChanged += (e, a) =>
            {
                RunOnUiThread( async () =>
                {
                    if (a.PropertyName == "NotificationMessage")
                    {
                        Toast.MakeText(this, VM.NotificationMessage, ToastLength.Long).Show();
                        Thread.Sleep(5000);
                        await VM.SaveCurrentQuestionResponses();
                        await VM.LoadCurrentQuestionForSurvey();
                        ClearComponents();
                        InitComponents();
                    }
                    
                });
            };
        }



        private void CleanGCMNote()
        {
            AppModel.GCMNote.RegistrationUpdated -= GCMNote_RegistrationUpdated;
            AppModel.GCMNote = null;
        }

        void GCMNote_RegistrationUpdated(string registrationId)
        {
            RunOnUiThread(async () =>
                {
                    progress.Dismiss();
                    CleanGCMNote();
                    if (!isRegistered)
                    {
                        isRegistered = true;
                        await VM.RegisterForNotification(registrationId, "DroidPhone", VM.ChannelName);
                        InitComponents();
                        
                    }
                }
            );
        }

        public void DisplayToastMessage()
        {
            Toast.MakeText(this, "Hello there", ToastLength.Long).Show();
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

        void button_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            var selectedId = ((Button)sender).Id;
            VM.CurrentQuestion.Options.First(x => x.Id == selectedId).IsSelected = e.IsChecked;
        }

        void radioGroup_CheckedChange(object sender, RadioGroup.CheckedChangeEventArgs e)
        {
            var selectedId = e.CheckedId;
            var opt = VM.CurrentQuestion.Options.FirstOrDefault(x => x.IsSelected == true);
            if (opt != null)
                opt.IsSelected = false;
            VM.CurrentQuestion.Options.First(x => x.Id == selectedId).IsSelected = true;
        }

        protected override void OnStop()
        {
    
            base.OnStop();
        }

        private void InitComponents()
        {
            this.Title = "Live Survey";
            var txtQuestion = FindViewById<TextView>(Resource.Id.txtLiveQuestion);

            txtQuestion.Text = VM.CurrentQuestion.QuestionText;
            if (VM.CurrentQuestion.IsMultiSelect)
            {
                foreach (var opt in VM.CurrentQuestion.Options)
                {
                    var button = new CheckBox(this);
                    button.Text = opt.OptionText;
                    button.Checked = opt.IsSelected;
                    button.Id = opt.Id;
                    button.CheckedChange += button_CheckedChange;
                    layout.AddView(button);
                }
            }
            else
            {
                radioGroup = new RadioGroup(this);
                foreach (var opt in VM.CurrentQuestion.Options)
                {
                    var button = new RadioButton(this);
                    button.Text = opt.OptionText;
                    button.Checked = opt.IsSelected;
                    button.Id = opt.Id;
                    radioGroup.AddView(button);

                }
                layout.AddView(radioGroup);
                radioGroup.CheckedChange += radioGroup_CheckedChange;

            }
        }

        private void ClearComponents()
        {
            if (radioGroup != null)
            {
                radioGroup.CheckedChange += radioGroup_CheckedChange;
                radioGroup = null;
            }
            if (checkboxes != null && checkboxes.Count > 0)
            {
                foreach (var button in checkboxes)
                {
                    button.CheckedChange -= button_CheckedChange;
                }
                checkboxes.Clear();
                checkboxes = null;
            }
            layout.RemoveAllViews();

        }

    }
}