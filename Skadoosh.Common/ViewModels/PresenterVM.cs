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
        #region private variables
        private ObservableCollection<Survey> _surveyCollection;
        private Survey _currentSurvey;
        private Question _currentQuestion;
        private bool _isSurveySelected;
        private bool _isQuestionSelected;
        private bool _canSetActive;
        private bool _canStartSurvey;
        private bool _canStopSurvey;
        private string _errorMessage;
         
        #endregion

        #region properties

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; Notify("ErrorMessage"); }
        }
        public bool CanStopSurvey
        {
            get { return _canStopSurvey; }
            set { _canStopSurvey = value; Notify("CanStopSurvey"); }
        }

        public bool CanStartSurvey
        {
            get { return _canStartSurvey; }
            set { _canStartSurvey = value; Notify("CanStartSurvey"); }
        }

        public bool CanSetActive
        {
            get { return _canSetActive; }
            set { _canSetActive = value; Notify("CanSetActive"); }
        }

        public bool IsQuestionSelected
        {
            get { return _isQuestionSelected; }
            set { _isQuestionSelected = value; Notify("IsQuestionSelected"); }
        }

        public bool IsSurveySelected
        {
            get { return _isSurveySelected; }
            set { _isSurveySelected = value; Notify("IsSurveySelected"); }
        }

        public bool IsLoading { get; set; }

        public Question CurrentQuestion
        {
            get { return _currentQuestion; }
            set
            {
                _currentQuestion = value;
                IsQuestionSelected = value != null ? true : false;
                CanSetActive = (value != null && CurrentSurvey.IsLiveSurvey) ? true : false;
                Notify("CurrentQuestion");
            }
        }

        public Survey CurrentSurvey
        {
            get { return _currentSurvey; }
            set
            {
                _currentSurvey = value;
                IsSurveySelected = value != null ? true : false;
                CanStartSurvey = (value != null && CurrentSurvey.IsLiveSurvey && !CurrentSurvey.IsActive);
                CanStopSurvey = (value != null && CurrentSurvey.IsLiveSurvey && CurrentSurvey.IsActive);
                Notify("CurrentSurvey");
            }
        }

        public ObservableCollection<Survey> SurveyCollection
        {
            get { return _surveyCollection; }
            set { _surveyCollection = value; Notify("SurveyCollection"); }
        } 
        #endregion

        #region Ctor
        public PresenterVM()
        {
            SurveyCollection = new ObservableCollection<Survey>();
        } 
        #endregion

        #region Survey Code
        public async Task<int> DeleteCurrentSurvey()
        {
            var table = AzureClient.GetTable<Survey>();
            if (CurrentSurvey != null && CurrentSurvey.Id != 0)
            {
                await DeleteQuestionBySurvey(_currentSurvey.Id);
                await table.DeleteAsync(CurrentSurvey);
                SurveyCollection.Remove(CurrentSurvey);
                CurrentSurvey = null;
                return SurveyCollection.Count;
            }
            return 0;
        }
        public async Task<bool> ChannelIsAvailable()
        {
            if (CurrentSurvey != null && !string.IsNullOrEmpty(CurrentSurvey.ChannelName))
            {
                var list = await AzureClient.GetTable<Survey>().Where(x => x.ChannelName.ToUpper() == CurrentSurvey.ChannelName.ToUpper()).ToListAsync();
                if (list.Any())
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        public async Task<int> UpdateSurvey()
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

            return 0;
        }
        public async void StartSurvey()
        {
            if (CurrentSurvey.IsLiveSurvey)
            {
                CurrentSurvey.IsActive = true;
                var table = AzureClient.GetTable<Survey>();
                await table.UpdateAsync(CurrentSurvey);
                CanStartSurvey = (CurrentSurvey.IsLiveSurvey && !CurrentSurvey.IsActive);
                CanStopSurvey = (CurrentSurvey.IsLiveSurvey && CurrentSurvey.IsActive);
            }
        }
        public async void StopSurvey()
        {
            if (CurrentSurvey.IsLiveSurvey)
            {
                CurrentSurvey.IsActive = false;
                var table = AzureClient.GetTable<Survey>();
                await table.UpdateAsync(CurrentSurvey);

                var qTable = AzureClient.GetTable<Question>();
                var actives = await qTable.Where(x => x.SurveyId == CurrentSurvey.Id && x.IsActive == true).ToListAsync();
                foreach (var q in actives)
                {
                    q.IsActive = false;
                    await qTable.UpdateAsync(q);
                }
                
                
                CanStartSurvey = (CurrentSurvey.IsLiveSurvey && !CurrentSurvey.IsActive);
                CanStopSurvey = (CurrentSurvey.IsLiveSurvey && CurrentSurvey.IsActive);
            }
        }
        public async Task<int> LoadSurveysForCurrentUser()
        {
            if (User != null)
            {
                SurveyCollection.Clear();
                var results = await AzureClient.GetTable<Survey>().Where(x => x.AccountUserId == User.Id).ToListAsync();
                foreach (var item in results)
                {
                    SurveyCollection.Add(item);
                }
                return SurveyCollection.Count;
            }
            return 0;
        }
        #endregion

        #region Question Code
        public async Task<int> DeleteQuestionBySurvey(int surveyId)
        {
            var table = AzureClient.GetTable<Question>();
            var questions = await table.Where(x => x.SurveyId == surveyId).ToListAsync();
            foreach (var q in questions)
            {
                await DeleteOptionByQuestionId(q.Id);
                await table.DeleteAsync(q).ConfigureAwait(true);
            }
            return 0;
        }
        public async void DeleteCurrentQuestion()
        {
            var table = AzureClient.GetTable<Question>();
            await DeleteOptionByQuestionId(CurrentQuestion.Id);
            await table.DeleteAsync(CurrentQuestion);
            _currentSurvey.Questions.Remove(CurrentQuestion);
            CurrentQuestion = null;
        }
        public async Task<int> UpdateQuestion()
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
                for (int x = _currentQuestion.Options.Count - 1; x > -1; x--)
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
            return CurrentQuestion.Options.Count;
        }
        public async void SetQuestionActive()
        {
            if (CurrentQuestion != null)
            {
                var table = AzureClient.GetTable<Question>();
                var activeQuestion = CurrentSurvey.Questions.FirstOrDefault(x => x.IsActive == true);
                if (activeQuestion != null && activeQuestion.Id != CurrentQuestion.Id)
                {
                    activeQuestion.IsActive = false;
                    CurrentQuestion.IsActive = true;
                    await table.UpdateAsync(activeQuestion);
                    await table.UpdateAsync(CurrentQuestion);
                }
                else
                {
                    CurrentQuestion.IsActive = true;
                    await table.UpdateAsync(CurrentQuestion);
                }
            }
        }
        public async Task<int> LoadQuestionsForCurrentSurvey()
        {
            if (CurrentSurvey != null)
            {
                _currentSurvey.Questions.Clear();
                var results = await AzureClient.GetTable<Question>().Where(x => x.SurveyId == CurrentSurvey.Id).ToListAsync();

                foreach (var q in results)
                {
                    var subResults = await AzureClient.GetTable<Option>().Where(x => x.QuestionId == q.Id).ToListAsync();
                    foreach (var o in subResults)
                    {
                        q.Options.Add(o);
                    }
                    _currentSurvey.Questions.Add(q);
                }
                return CurrentSurvey.Questions.Count;
            }
            return 0;
        }
        #endregion

        #region Option Code
        public async Task<int> DeleteOptionByQuestionId(int questionId)
        {
            var table = AzureClient.GetTable<Option>();
            var opts = await table.Where(x => x.QuestionId == questionId).ToListAsync();
            foreach (var opt in opts)
            {
                await DeleteReponsesByOptionId(opt.Id);
                await table.DeleteAsync(opt);
            }
            return 0;
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
                        if (!string.IsNullOrEmpty(opt.OptionText))
                        {
                            await table.UpdateAsync(opt);
                        }
                        else
                        {
                            await table.DeleteAsync(opt);
                            CurrentQuestion.Options.Remove(opt);
                        }
                    }
                }
            }

        }  
        #endregion

        #region Response Code

        public async Task<int> DeleteReponsesByOptionId(int optionId)
        {
            var table = AzureClient.GetTable<Responses>();
            var responses = await table.Where(x => x.OptionId == optionId).ToListAsync();
            foreach (var response in responses)
            {
                await table.DeleteAsync(response).ConfigureAwait(true);
            }
            return 0;
        } 
        #endregion

    }
}
