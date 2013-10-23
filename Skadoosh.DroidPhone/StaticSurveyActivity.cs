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

namespace Skadoosh.DroidPhone
{
    [Activity(Label = "Static Survey", Icon = "@drawable/ic_launcher")]
    public class StaticSurveyActivity : Activity
    {
        private ParticipateStaticVM VM;
        private RadioGroup radioGroup;
        private List<CheckBox> checkboxes;
        private Button btnNext;
        private Button btnQuit;
        private LinearLayout layout;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.StaticSurvey);
            VM = (ParticipateStaticVM)AppModel.VM;
            layout = FindViewById<LinearLayout>(Resource.Id.optionLayout);
            btnNext = FindViewById<Button>(Resource.Id.btnNext);
            btnNext.Click += (e, a) =>
            {
                ClearComponents();
                VM.NextQuestion();
                InitComponents();
            };
            btnQuit = FindViewById<Button>(Resource.Id.btnQuit);
            btnQuit.Click += (e, a) =>
            {

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

    }
}