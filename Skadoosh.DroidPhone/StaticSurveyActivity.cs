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
    [Activity(Label = "Static Survey", Icon = "@drawable/ic_launcher")]
    public class StaticSurveyActivity : ActivityBase
    {
        private ProgressDialog progress;
        private ParticipateStaticVM VM;
        private RadioGroup radioGroup;
        private List<CheckBox> checkboxes;
        private Button btnNext;
        private Button btnBack;
        private Button btnQuit;
        private LinearLayout layout;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.StaticSurvey);
            this.SetOrientationBackground(Resource.Id.StaticSurvey);
            VM = (ParticipateStaticVM)AppModel.VM;
            layout = FindViewById<LinearLayout>(Resource.Id.optionLayout);
            btnNext = FindViewById<Button>(Resource.Id.btnNext);
            btnBack = FindViewById<Button>(Resource.Id.btnBack);
            btnQuit = FindViewById<Button>(Resource.Id.btnQuit);
            btnNext.Click += (e, a) =>
            {
                ClearComponents();
                VM.NextQuestion();
                InitComponents();
            };
            btnBack.Click += (e, a) =>
            {
                ClearComponents();
                VM.BackQuestion();
                InitComponents();
            };
            btnQuit.Click += (e, a) =>
            {
                
                var builder1 = new AlertDialog.Builder(new ContextThemeWrapper(this, Resource.Style.AlertDialogCustom));
                builder1.SetTitle("Exiting...");
                builder1.SetMessage("Do you want to save your changes");
                builder1.SetPositiveButton("Yes", async delegate
                {
                    ShowLoading();
                    var result = await VM.SaveSurveyResponses();
                    this.progress.Dismiss();
                    ClearComponents();
                    this.Finish();
                    StartActivity(typeof(Home));
                });
                builder1.SetNegativeButton("No", delegate {
                    ClearComponents();
                    this.Finish();
                    StartActivity(typeof(Home));
                });
                builder1.SetNeutralButton("Cancel", delegate {
                    
                });
                var alert1 = builder1.Show();
                ChangeDialogColor(alert1);
                


            };
            InitComponents();      
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


        private void InitComponents()
        {
            this.Title = "Static Survey: " + VM.CurrentPostition;
            var txtQuestion = FindViewById<TextView>(Resource.Id.txtQuesationText);
            
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

        private void ShowLoading()
        {
            progress = new ProgressDialog(this);
            progress.Indeterminate = true;
            progress.SetProgressStyle(ProgressDialogStyle.Spinner);
            progress.SetMessage("Saving your responses. Please wait...");
            progress.SetCancelable(false);
            progress.Show();
        }

    }
}