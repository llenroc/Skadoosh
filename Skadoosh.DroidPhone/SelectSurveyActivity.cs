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

namespace skadoosh.DroidPhone
{
    [Activity(Label = "skadoosh - Survey Select", Icon = "@drawable/ic_launcher")]
    public class SelectSurveyActivity : ActivityBase
    {
        private ProgressDialog progress;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.SelectSurvey);
            this.SetOrientationBackground(Resource.Id.SelectFrame);

            var btnStart = FindViewById<Button>(Resource.Id.btnStart);
            var txtFirstName = FindViewById<TextView>(Resource.Id.txtFirstName);
            var txtlastName = FindViewById<TextView>(Resource.Id.txtLastName);
            var txtSurveyCode = FindViewById<TextView>(Resource.Id.txtSurveyCode);

            btnStart.Click += async(e, a) =>
            {

                try
                {
                    switch (Intent.GetStringExtra("VM"))
                    {
                        case "ParticipateLiveVM":
                            ShowLoading();
                            var lvm = new ParticipateLiveVM();
                            lvm.User.FirstName = txtFirstName.Text;
                            lvm.User.LastName = txtlastName.Text;
                            lvm.ChannelName = txtSurveyCode.Text;
                            var lr = await lvm.FindSurveyCurrentChannel();
                            progress.Dismiss();
                            if (lr == 1)
                            {
                                AppModel.VM = lvm;
                                StartActivity(typeof(LiveSurveyActivity));
                            }
                            else
                            {
                                var builder1 = new AlertDialog.Builder(new ContextThemeWrapper(this, Resource.Style.AlertDialogCustom));
                                builder1.SetTitle("Oops, somethings wrong");
                                builder1.SetMessage(lvm.ErrorMessage);
                                builder1.SetPositiveButton("OK", delegate { });
                                var alert1 = builder1.Show();
                                ChangeDialogColor(alert1);

                            }
                            break;
                        case "ParticipateStaticVM":
                            ShowLoading();
                            var svm = new ParticipateStaticVM();
                            svm.User.FirstName = txtFirstName.Text;
                            svm.User.LastName = txtlastName.Text;
                            svm.ChannelName = txtSurveyCode.Text;
                            var sr = await svm.FindSurveyCurrentChannel();
                            progress.Dismiss();
                            if (sr == 1)
                            {
                                AppModel.VM = svm;
                                StartActivity(typeof(StaticSurveyActivity));
                            }
                            else
                            {
                                var builder2 = new AlertDialog.Builder(new ContextThemeWrapper(this, Resource.Style.AlertDialogCustom));
                                builder2.SetTitle("Oops, somethings wrong");
                                builder2.SetMessage(svm.ErrorMessage);
                                builder2.SetPositiveButton("OK", delegate { });
                                var alert2 = builder2.Show();
                                ChangeDialogColor(alert2);
                            }
                            break;
                    }
                }
                catch (Exception ex)
                {
                    var builder1 = new AlertDialog.Builder(new ContextThemeWrapper(this, Resource.Style.AlertDialogCustom));
                    builder1.SetTitle("Error...");
                    builder1.SetMessage(ex.Message);
                    builder1.SetPositiveButton("Ok", delegate
                    {

                    });
                    var alert1 = builder1.Show();
                    ChangeDialogColor(alert1);
                }

            };
        }

        private void ShowLoading()
        {
            progress = new ProgressDialog(this);
            progress.Indeterminate = true;
            progress.SetProgressStyle(ProgressDialogStyle.Spinner);
            progress.SetMessage("Performing search. Please wait...");
            progress.SetCancelable(false);
            progress.Show();
        }
    }
}