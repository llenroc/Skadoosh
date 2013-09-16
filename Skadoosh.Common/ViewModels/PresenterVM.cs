using Skadoosh.Common.DomainModels;
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

        private Question currentQuestion;

        public Question CurrentQuestion
        {
            get { return currentQuestion; }
            set
            {
                currentQuestion = value;
                Notify("CurrentQuestion");
            }
        }
        

        
        public Survey CurrentSurvey
        {
            get { return currentSurvey; }
            set
            {
                currentSurvey = value;
                LoadQuestionsForCurrentSurvey();
                Notify("CurrentSurvey");
            }
        }
        
        public ObservableCollection<Survey> SurveyCollection
        {
            get { return surveyCollection; }
            set { surveyCollection = value; Notify("SurveyCollection"); }
        }

        public PresenterVM()
        {
            SurveyCollection = new ObservableCollection<Survey>();
        }

        public async void DeleteSurvey()
        {
            var table = AzureClient.GetTable<Survey>();
            if (CurrentSurvey != null && CurrentSurvey.Id != 0)
            {
                DeleteQuestionBySurvey(currentSurvey.Id);
                await table.DeleteAsync(CurrentSurvey);
            }
        }
        public async void DeleteQuestionBySurvey(int surveyId)
        {
            var table = AzureClient.GetTable<Question>();
            var questions = await table.Where(x => x.SurveyId == surveyId).ToListAsync();
            foreach (var q in questions)
            {
                await table.DeleteAsync(q);
            }
        }
        public async void DeleteQuestion(int id)
        {
            var table = AzureClient.GetTable<Option>();
            var question = table.LookupAsync(id).Result;
            DeleteOptionByQuestionId(question.Id);
            await table.DeleteAsync(question);
        }
        public async void DeleteOptionByQuestionId(int questionId)
        {
            var table = AzureClient.GetTable<Option>();
            var opts = await table.Where(x => x.QuestionId == questionId).ToListAsync();
            foreach (var opt in opts)
            {
               await  table.DeleteAsync(opt);
            }
        }
        public async void DeleteOption(int id)
        {
            var table = AzureClient.GetTable<Option>();
            var opt = table.LookupAsync(id).Result;     
            await table.DeleteAsync(opt);
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
            var options = AzureClient.GetTable<Option>();
            if (CurrentQuestion.IsNew)
            {
                await table.InsertAsync(CurrentQuestion);
                foreach (var opt in CurrentQuestion.Options)
                {
                    opt.QuestionId = CurrentQuestion.Id;
                }
            }
            else
            {

                await table.UpdateAsync(CurrentQuestion);
                var list = await AzureClient.GetTable<Option>().Where(x => x.QuestionId == CurrentQuestion.Id).ToListAsync();
                var existingIds = list.Select(x => x.QuestionId).ToArray();
                var loadedIds = CurrentQuestion.Options.Select(x => x.QuestionId).ToArray();
                foreach (var id in existingIds.Where(x => !loadedIds.Contains(x)))
                {
                    var opt = list.First(x => x.QuestionId == id);
                    await options.DeleteAsync(opt);
                }
            }
            UpdateOptions();
        }
        public async void UpdateOptions()
        {
            var table = AzureClient.GetTable<Option>();
            foreach (var opt in CurrentQuestion.Options)
            {
                if (opt.QuestionId != 0)
                {
                    if (opt.IsNew)
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

        public async Task<int> LoadSurveysForCurrentUser()
        {
            SurveyCollection.Clear();
            var list = await AzureClient.GetTable<Survey>().Where(x => x.AccountUserId == User.Id).ToListAsync();
            if (list != null && list.Any())
            {
                foreach (var item in list)
                {
                    SurveyCollection.Add(item);
                }
                return SurveyCollection.Count;
            }
            else
            {
                return 0;
            }
        }
        public async void LoadQuestionsForCurrentSurvey()
        {
            if (CurrentSurvey != null)
            {
                currentSurvey.Questions.Clear();
                var qList = await AzureClient.GetTable<Question>().Where(x => x.SurveyId == CurrentSurvey.Id).ToListAsync();
                if (qList != null && qList.Any())
                {
                    foreach (var q in qList)
                    {
                        var optList = await AzureClient.GetTable<Option>().Where(x => x.QuestionId == q.Id).ToListAsync();
                        if (optList != null && optList.Any())
                        {
                            foreach (var o in optList)
                            {
                                q.Options.Add(o);
                            }
                        }
                        currentSurvey.Questions.Add(q);
                    }
                }
            }
        }
       
        
    }
}
