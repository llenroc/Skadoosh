
using Skadoosh.Common.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skadoosh.Common.ViewModels
{
    public class ParticipateLiveVM : ViewModelBase
    {
        private Survey _currentSurvey;
        private Question _currentQuestion;
        private string _channelName;
        private string _errorMessage;
        private bool _isBusy;
        private bool _isClosed;

        public bool IsClosed
        {
            get { return _isClosed; }
            set { _isClosed = value; Notify("IsClosed"); }
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
        
        public string ChannelName
        {
            get { return _channelName; }
            set { _channelName = value; Notify("ChannelName"); }
        }
        
        public Question CurrentQuestion
        {
            get { return _currentQuestion; }
            set
            {
                _currentQuestion = value;
                Notify("CurrentQuestion");
            }
        }

        public Survey CurrentSurvey
        {
            get { return _currentSurvey; }
            set
            {
                _currentSurvey = value;
                Notify("CurrentSurvey");
            }
        }


        public async Task<int> SaveCurrentQuestionResponses()
        {
            IsBusy = true;
            var table = AzureClient.GetTable<Responses>();
            foreach (var q in CurrentSurvey.Questions)
            {
                foreach (var op in q.Options.Where(x => x.IsSelected))
                {
                    var r = new Responses()
                    {
                        OptionId = op.Id,
                        QuestionId = q.Id,
                        SurveyId = CurrentSurvey.Id,
                        DateEntered = DateTime.Now.ToString()
                    };
                    if (CurrentSurvey.RequiresUserName)
                        r.UserName = User.LastName + ", " + User.FirstName;
                    await table.InsertAsync(r);
                }
            }
            IsBusy = false;
            return 0;
        }

        public async Task<int> LoadCurrentQuestionForSurvey()
        {
            if (CurrentSurvey != null)
            {
                IsBusy = true;
                CurrentSurvey.Questions.Clear();
                var results = await AzureClient.GetTable<Question>().Where(x => x.SurveyId == CurrentSurvey.Id && x.IsActive==true).ToListAsync();
                if (results != null && results.Count > 0)
                {
                    CurrentQuestion = results.First();
                    var subResults =
                        await
                            AzureClient.GetTable<Option>().Where(x => x.QuestionId == CurrentQuestion.Id).ToListAsync();
                    foreach (var o in subResults)
                    {
                        CurrentQuestion.Options.Add(o);
                    }
                    CurrentSurvey.Questions.Add(CurrentQuestion);
                    IsBusy = false;
                    return 1;
                }
                else
                {
                    CurrentSurvey.IsActive = false;
                    IsClosed = true;
                    IsBusy = false;
                    return 0;
                }
            }
            return 0;
        }



        public async Task<int> FindSurveyCurrentChannel()
        {
            
            if (!string.IsNullOrEmpty(ChannelName))
            {
                IsBusy = true;

                var results = await AzureClient.GetTable<Survey>().Where(x => x.ChannelName == ChannelName).ToListAsync();
                if (results != null && results.Count>0)
                {
                    var survey = results.First();
                    if (survey.IsLiveSurvey)
                    {
                        if (survey.RequiresUserName)
                        {

                            if (string.IsNullOrEmpty(User.FirstName) || string.IsNullOrEmpty(User.LastName))
                            {
                                ErrorMessage = "This Survey Requires Your First And Last Name";
                                return 0;
                            }

                        }

                        ErrorMessage = string.Empty;
                        CurrentSurvey = survey;
                        await LoadCurrentQuestionForSurvey();
                        return 1;
                    }
                    else
                    {
                        ErrorMessage = "Please Enter A Survey Code Associated To A Live Survey";
                    }
                }
                else
                {
                   
                    ErrorMessage = "Could Not Find Survey. Is The Code Correct?";
                }
            }
            else
            {
                ErrorMessage = "A Survey Code Must Be Entered!";

            }
            IsBusy = false;
            return 0;
        }
    }
    
}
