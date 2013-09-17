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
        public event EventHandler LoadingCompleted;

        private ObservableCollection<Survey> surveyCollection;
        private Survey currentSurvey;
        private Question currentQuestion;
        public bool IsLoading { get; set; }
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

        #region Survey Code
        public async void DeleteCurrentSurvey()
        {
            var table = AzureClient.GetTable<Survey>();
            if (CurrentSurvey != null && CurrentSurvey.Id != 0)
            {
                DeleteQuestionBySurvey(currentSurvey.Id);
                await table.DeleteAsync(CurrentSurvey);
                SurveyCollection.Remove(CurrentSurvey);
                CurrentSurvey = null;
            }
        }
        public async void UpdateSurvey()
        {
            var table = AzureClient.GetTable<Survey>();
            if (CurrentSurvey.Id == 0)
            {
                await table.InsertAsync(CurrentSurvey);
                surveyCollection.Add(CurrentSurvey);
            }
            else
            {
                await table.UpdateAsync(CurrentSurvey);
            }

        } 
        #endregion

        #region Question Code
        public async void DeleteQuestionBySurvey(int surveyId)
        {
            var table = AzureClient.GetTable<Question>();
            var questions = await table.Where(x => x.SurveyId == surveyId).ToListAsync();
            foreach (var q in questions)
            {
                DeleteOptionByQuestionId(q.Id);
                await table.DeleteAsync(q);
            }
        }
        public async void DeleteCurrentQuestion()
        {
            var table = AzureClient.GetTable<Question>();
            DeleteOptionByQuestionId(CurrentQuestion.Id);
            await table.DeleteAsync(CurrentQuestion);
            currentSurvey.Questions.Remove(CurrentQuestion);
            CurrentQuestion = null;
        }
        public async void UpdateQuestion()
        {
            var table = AzureClient.GetTable<Question>();
            var options = AzureClient.GetTable<Option>();
            if (CurrentQuestion.IsNew)
            {
                await table.InsertAsync(CurrentQuestion);
                CurrentSurvey.Questions.Add(CurrentQuestion);
                foreach (var opt in CurrentQuestion.Options)
                {
                    opt.QuestionId = CurrentQuestion.Id;
                }
            }
            else
            {

                await table.UpdateAsync(CurrentQuestion);
                var optTable = AzureClient.GetTable<Option>();
                for (int x = currentQuestion.Options.Count - 1; x > -1; x--)
                {
                    var opt = CurrentQuestion.Options[x];
                    if (opt.IsDeleted)
                    {
                        await optTable.DeleteAsync(opt);
                        CurrentQuestion.Options.Remove(opt);
                    }
                }
            }
            UpdateOptions();
        } 
        #endregion

        #region Option Code
        public async void DeleteOptionByQuestionId(int questionId)
        {
            var table = AzureClient.GetTable<Option>();
            var opts = await table.Where(x => x.QuestionId == questionId).ToListAsync();
            foreach (var opt in opts)
            {
                DeleteReponsesByOptionId(opt.Id);
                await table.DeleteAsync(opt);
            }
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
        #endregion

        #region Response Code
        public async void DeleteReponsesByOptionId(int optionId)
        {
            var table = AzureClient.GetTable<Responses>();
            var responses = await table.Where(x => x.OptionId == optionId).ToListAsync();
            foreach (var response in responses)
            {
                await table.DeleteAsync(response);
            }
        } 
        #endregion

        
        public async Task<int> LoadSurveysForCurrentUser()
        {
            IsLoading = true;
            int collectionCount = 0;
            SurveyCollection.Clear();
            var list = await AzureClient.GetTable<Survey>().Where(x => x.AccountUserId == User.Id).ToListAsync();
            if (list != null && list.Any())
            {
                foreach (var item in list)
                {
                    SurveyCollection.Add(item);
                }
                collectionCount = SurveyCollection.Count;
            }

            IsLoading = false;
            if (LoadingCompleted != null)
            {
                LoadingCompleted(null, null);
            }
            return collectionCount;
        }
        public async void LoadQuestionsForCurrentSurvey()
        {
            if (CurrentSurvey != null)
            {
                IsLoading = true;
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
                IsLoading = false;
                if (LoadingCompleted != null)
                {
                    LoadingCompleted(null, null);
                }
            }
        }     
    }
}
