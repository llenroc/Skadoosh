﻿using Skadoosh.Common.DomainModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skadoosh.Common.ViewModels
{
    public class PresenterVM : ViewModelBase
    {

        private ObservableCollection<Survey> surveyCollection;
        private Survey currentSurvey;
        private ObservableCollection<Question> questionCollection;
        private Question currentQuestion;
        private ObservableCollection<Option> optionCollection;

        public ObservableCollection<Option> OptionCollection
        {
            get { return optionCollection; }
            set { optionCollection = value; Notify("OptionCollection");}
        }
        
        public Question CurrentQuestion
        {
            get { return currentQuestion; }
            set { currentQuestion = value; Notify("CurrentQuestion");}
        }
        
        public ObservableCollection<Question> QuestionCollection
        {
            get { return questionCollection; }
            set { questionCollection = value; Notify("QuestionCollection");}
        }
        
        public Survey CurrentSurvey
        {
            get { return currentSurvey; }
            set { currentSurvey = value; Notify("CurrentSurvey");}
        }
        
        public ObservableCollection<Survey> SurveyCollection
        {
            get { return surveyCollection; }
            set { surveyCollection = value; Notify("SurveyCollection"); }
        }

        public PresenterVM()
        {
            SurveyCollection = new ObservableCollection<Survey>();


             
            SurveyCollection.Add(new Survey() { Id = 1, SurveyTitle = "Test1" });
            SurveyCollection.Add(new Survey() { Id = 2, SurveyTitle = "Test2" });
            SurveyCollection.Add(new Survey() { Id = 3, SurveyTitle = "Test3" });
            SurveyCollection.Add(new Survey() { Id = 4, SurveyTitle = "Test4" });
            SurveyCollection.Add(new Survey() { Id = 5, SurveyTitle = "Test5" });
            SurveyCollection.Add(new Survey() { Id = 6, SurveyTitle = "Test6" });
            SurveyCollection.Add(new Survey() { Id = 7, SurveyTitle = "Test7" });
            SurveyCollection.Add(new Survey() { Id = 8, SurveyTitle = "Test8" });

            QuestionCollection = new ObservableCollection<Question>();
            questionCollection.Add(new Question() { Id = 1, QuestionText = "What is your favorite Color?", QuestionType = 1, SurveyId = 1 });
            OptionCollection = new ObservableCollection<Option>();
        }


        public async void UpdateSurvey()
        {
            var table = AzureClient.GetTable<Survey>();
            if (CurrentSurvey.Id == 0)
            {
                await table.InsertAsync(CurrentSurvey);
            }
            else
            {
                await table.UpdateAsync(CurrentSurvey);
            }
        }
        public async void UpdateQuestion()
        {
            var table = AzureClient.GetTable<Question>();
            if (CurrentSurvey.Id == 0)
            {
                await table.InsertAsync(CurrentQuestion);
            }
            else
            {
                await table.UpdateAsync(CurrentQuestion);
            }
        }
        public async void UpdateOptions()
        {
            var table = AzureClient.GetTable<Option>();
            foreach (var opt in OptionCollection)
            {
                if (opt.QuestionId != 0)
                {
                    if (opt.Id == 0)
                    {
                        await table.InsertAsync(opt);
                    }
                    else
                    {
                        await table.UpdateAsync(opt);
                    }
                }
            }

        }

        public async void LoadAvailbleCollections()
        {
            SurveyCollection.Clear();
            var list = await AzureClient.GetTable<Survey>().Where(x => x.AccountUserId == User.Id).ToListAsync();
            if (list != null && list.Any())
            {
                foreach (var item in list)
                {
                    SurveyCollection.Add(item);
                }
            }
        }
        public async void LoadQuestionsForCurrentSurvey()
        {
            if (CurrentSurvey != null)
            {
                //QuestionCollection.Clear();
                //var list = await AzureClient.GetTable<Question>().Where(x => x.SurveyId == CurrentSurvey.Id).ToListAsync();
                //if (list != null && list.Any())
                //{
                //    foreach (var item in list)
                //    {
                //        QuestionCollection.Add(item);
                //    }
                //}
            }
        }
        public async void LoadOptionsForCurrentQuestion()
        {
            if (CurrentQuestion != null)
            {
                OptionCollection.Clear();
                var list = await AzureClient.GetTable<Option>().Where(x => x.QuestionId == CurrentQuestion.Id).ToListAsync();
                if (list != null && list.Any())
                {
                    foreach (var item in list)
                    {
                        OptionCollection.Add(item);
                    }
                }
            }
        }
        
    }
}
