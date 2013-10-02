

using Microsoft.WindowsAzure.MobileServices;
using Skadoosh.Common.DomainModels;
using Statera.Xamarin.Common;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        private bool _isBusy;
        private ExpressLogin _expLogin;

        #endregion

        #region properties

        public ExpressLogin ExpLogin
        {
            get { return _expLogin; }
            set { _expLogin = value; Notify("ExpLogin"); }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; Notify("IsBusy"); }
        }
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

        //public bool IsLoading { get; set; }

        public Question CurrentQuestion
        {
            get { return _currentQuestion; }
            set
            {
                _currentQuestion = value;
                IsQuestionSelected = value != null ? true : false;
                CanSetActive = (value != null && CurrentSurvey.IsLiveSurvey && CurrentSurvey.IsActive) ? true : false;
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
            ExpLogin = new ExpressLogin();
        } 
        #endregion

        #region Survey Code
        public async Task<int> DeleteCurrentSurvey()
        {
            IsBusy = true;
            var table = AzureClient.GetTable<Survey>();
            if (CurrentSurvey != null && CurrentSurvey.Id != 0)
            {
                await DeleteQuestionBySurvey(_currentSurvey.Id);
                await table.DeleteAsync(CurrentSurvey);
                SurveyCollection.Remove(CurrentSurvey);
                CurrentSurvey = null;
                return SurveyCollection.Count;
            }
            IsBusy = false;
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
                IsBusy = true;
                var channelTable = AzureClient.GetTable<SurveyNotificationChannel>();
                var channelList = await channelTable.Where(x => x.ChannelName == CurrentSurvey.ChannelName).ToListAsync();
                foreach (var item in channelList)
                {
                    await channelTable.DeleteAsync(item);
                }


                CurrentSurvey.IsActive = true;
                var table = AzureClient.GetTable<Survey>();
                await table.UpdateAsync(CurrentSurvey);


                var qTable = AzureClient.GetTable<Question>();
                var activeQuestions = await qTable.Where(x => x.SurveyId == CurrentSurvey.Id && x.IsActive==true).ToListAsync();
                if (!activeQuestions.Any())
                {
                    var list = await qTable.Where(x => x.SurveyId == CurrentSurvey.Id).Take(1).ToListAsync();
                    var q = list.First();
                    q.IsActive = true;
                    await qTable.UpdateAsync(q);
                }

                CanStartSurvey = (CurrentSurvey.IsLiveSurvey && !CurrentSurvey.IsActive);
                CanStopSurvey = (CurrentSurvey.IsLiveSurvey && CurrentSurvey.IsActive);
                IsBusy = false;
            }
        }
        public async void StopSurvey()
        {
            if (CurrentSurvey.IsLiveSurvey)
            {
                IsBusy = true;
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
                IsBusy = false;
            }
        }
        public async Task<int> LoadSurveysForCurrentUser()
        {
            if (User != null)
            {
                IsBusy = true;
                SurveyCollection.Clear();
                var results = await AzureClient.GetTable<Survey>().Where(x => x.AccountUserId == User.Id).ToListAsync();
                foreach (var item in results)
                {
                    SurveyCollection.Add(item);
                }
                IsBusy = false;
                return SurveyCollection.Count;
            }
            return 0;
        }
        #endregion

        #region Question Code
        public async Task<int> DeleteQuestionBySurvey(int surveyId)
        {
            IsBusy = true;
            var table = AzureClient.GetTable<Question>();
            var questions = await table.Where(x => x.SurveyId == surveyId).ToListAsync();
            foreach (var q in questions)
            {
                await DeleteOptionByQuestionId(q.Id);
                await table.DeleteAsync(q).ConfigureAwait(true);
            }
            IsBusy = false;
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
                IsBusy = true;
                CurrentSurvey.Questions.Clear();
                var results = await AzureClient.GetTable<Question>().Where(x => x.SurveyId == CurrentSurvey.Id).ToListAsync();

                foreach (var q in results)
                {
                    var subResults = await AzureClient.GetTable<Option>().Where(x => x.QuestionId == q.Id).ToListAsync();
                    foreach (var o in subResults)
                    {
                        q.Options.Add(o);
                    }
                    CurrentSurvey.Questions.Add(q);
                }
                IsBusy = false;
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

        public async Task<List<Responses>> GetAllResponsesForCurrentSurvey()
        {
            if (CurrentSurvey != null)
            {
                var table = AzureClient.GetTable<Responses>();
                var responses = await table.Where(x => x.SurveyId == CurrentSurvey.Id).ToListAsync();
                return responses;
            }
            return new List<Responses>();
        }

        public async Task<List<Responses>>  GetResponsesForCurrentQuestion()
        {
            if (CurrentQuestion != null)
            {
                IsBusy = true;
                var table = AzureClient.GetTable<Responses>();
                var responses = await table.Where(x => x.QuestionId == CurrentQuestion.Id).ToListAsync();
                IsBusy = false;
                return responses;
            }
            return new List<Responses>();
        }

        public async Task<List<ResponseCSV>> GetResponseCSVForCurrentSurvey()
        {
            
            var list = new List<ResponseCSV>();
            var response = await GetAllResponsesForCurrentSurvey();
            if (response != null && response.Any())
            {
                await LoadQuestionsForCurrentSurvey();
                foreach (var r in response)
                {
                    var csv = new ResponseCSV()
                    {
                        DateEntered = r.DateEntered,
                        Id = r.Id,
                        Survey = CurrentSurvey.SurveyTitle,
                        Question = CurrentSurvey.Questions.First(x => x.Id == r.QuestionId).QuestionText,
                        Option = CurrentSurvey.Questions.First(x => x.Id == r.QuestionId).Options.First(x => x.Id == r.OptionId).OptionText,
                        UserName=r.UserName
                    };
                    list.Add(csv);
                }
            }

            return list;
        } 
        #endregion


        #region Express Login

        public async Task<bool> CreateExpressLogin()
        {
            if (ExpLogin != null && !string.IsNullOrEmpty(ExpLogin.Password))
            {
                var list = await GetExpressLogins();

                if (list == null)
                    list = new List<ExpressLogin>();

                var el = new ExpressLogin()
                {
                    Id = this.User.Id,
                    UserId = this.AzureClient.CurrentUser.UserId,
                    MobileServiceAuthenticationToken = this.AzureClient.CurrentUser.MobileServiceAuthenticationToken,
                    Password = ExpLogin.Password

                };

                if (list.Any(x => x.Password == el.Password))
                    return false;

                if (list.Any(x => x.Id == el.Id))
                {
                    var existingItem = list.First(x => x.Id == el.Id);
                    var index = list.IndexOf(existingItem);
                    list[index] = el;
                }
                else
                {
                    list.Add(el);
                }
                var lss = new LocalStorageService();
                lss.SaveIsolatedStorage<List<ExpressLogin>>("SkadooshLogin", list);
                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task<List<ExpressLogin>> GetExpressLogins()
        {
            var lss = new LocalStorageService();
            var eps = await lss.GetIsolatedStorage<List<ExpressLogin>>("SkadooshLogin");
            return eps;
        }

        public async Task<MobileServiceUser> GetExpressLoginClient()
        {
            if (ExpLogin != null && !string.IsNullOrEmpty(ExpLogin.Password))
            {
                IsBusy = true;
                var list = await GetExpressLogins();
                if (list != null && list.Any())
                {
                    var ep = list.FirstOrDefault(x => x.Password == ExpLogin.Password);
                    if (ep != null)
                    {
                        var msu = new MobileServiceUser(ep.UserId);
                        msu.MobileServiceAuthenticationToken = ep.MobileServiceAuthenticationToken;
                        IsBusy = false;
                        return msu;
                    }
                    else
                    {
                        IsBusy = false;
                        return null;
                    }
                }
                else
                {
                    IsBusy = false;
                    return null;
                }
            }
            else
            {
                IsBusy = false;
                return null;
            }
        } 

        #endregion
    }
}
