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


namespace Skadoosh.DroidPhone
{
    [Activity(Label = "Skadoosh - Survey Select", Icon = "@drawable/ic_launcher")]
    public class SelectSurveyActivity : ActivityBase
    {

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
              
         
                switch (Intent.GetStringExtra("VM"))
                {
                    case "ParticipateLiveVM":
                        var lvm = new ParticipateLiveVM();
                        lvm.User.FirstName = txtFirstName.Text;
                        lvm.User.LastName = txtlastName.Text;
                        lvm.ChannelName = txtSurveyCode.Text;
                        var lr = await lvm.FindSurveyCurrentChannel();
                        if (lr == 1)
                        {

                        }
                        else
                        {
                            //var builder  = new AlertDialog.Builder(new ContextThemeWrapper(this, Resource.Style.AlertDialogCustom));
                            ////var builder = new AlertDialog.Builder(this);    
                            //builder.SetTitle("Error");
                            //builder.SetMessage("Can't connect to the database.");
                            //builder.SetCancelable(false);
                            //builder.SetPositiveButton("OK", delegate { Finish(); });
                 
                            //var alert = builder.Show();
                          
                            QustomDialogBuilder qustomDialogBuilder = new QustomDialogBuilder(this);
                          
                            qustomDialogBuilder.setTitle("Set IP Address");
                            qustomDialogBuilder.setTitleColor("#FF4F00");
                            qustomDialogBuilder.setDividerColor("#FF4F00");
                            qustomDialogBuilder.setMessage("You are now entering the 10th dimension.");
                           
                            qustomDialogBuilder.setCustomView(Resource.Layout.Alert, this);


                            qustomDialogBuilder.show();
                            //alert.SetView(alert.LayoutInflater.Inflate(Resource.Layout.Alert, null));
                            //alert.SetView(alert.LayoutInflater.Inflate(Resource.Layout.Alert, null));
                            //alert.SetTitle("Error");
                            //alert.SetMessage("help");
                           
                            //alert.Show();
                         
                            //var alert = new AlertDialog.Builder.c
                            //this.LayoutInflater.Inflate(Resource.Layout.CustomAlert,
                            //var alertBuilder = new AlertDialog.Builder(this);
                            
                       
                            //alert.SetTitle("Error");
                            //alert.SetMessage(lvm.ErrorMessage);
                            //var dialog = alert.Show();
                            //dialog.SetDividerColor();
                           // this.SetDialogDecoratorBar(dialog);
                        }
                        break;
                    case "ParticipateStaticVM":
                        var svm = new ParticipateStaticVM();
                        svm.User.FirstName = txtFirstName.Text;
                        svm.User.LastName = txtlastName.Text;
                        svm.ChannelName = txtSurveyCode.Text;
                        var sr = await svm.FindSurveyCurrentChannel();
                        if (sr == 1)
                        {

                        }
                        else
                        {
                            //var alert = new AlertDialog.Builder(new ContextThemeWrapper(this, Resource.Style.AlertDialogCustom));
                            //alert.SetTitle("Error");
                            //alert.SetMessage(svm.ErrorMessage);
                            //var dialog = alert.Show();
                            ////this.SetDialogDecoratorBar(dialog);
                        }
                        break;
                }

            };
        }
    }
}