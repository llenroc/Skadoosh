using Skadoosh.Common.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skadoosh.Common.ViewModels
{
    public class ParticipateStaticVM : ViewModelBase
    {
        private Survey _currentSurvey;
        private Question _currentQuestion;
        private string _channelName;
        private string _errorMessage;
        private bool _isLastQuestion;
        private string _currentPosition;
        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; Notify("IsBusy"); }
        }
        public string CurrentPostition
        {
            get { return _currentPosition; }
            set { _currentPosition = value; Notify("CurrentPostition"); }
        }
        
        public bool IsLastQuestion
        {
            get { return _isLastQuestion; }
            set { _isLastQuestion = value; Notify("IsLastQuestion"); }
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
                IsLastQuestion = (CurrentSurvey.Questions.Last().Id == value.Id);
                CurrentPostition = (CurrentSurvey.Questions.IndexOf(value) + 1) + " of " + CurrentSurvey.Questions.Count;
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

        public ParticipateStaticVM()
        {
          
        }

        public async Task<int> SaveSurveyResponses()
        {
            IsBusy = true;
            var table = AzureClient.GetTable<Responses>();
            foreach (var q in CurrentSurvey.Questions)
            {
                foreach (var op in q.Options.Where(x => x.IsSelected))
                {
                    var r = new Responses() { OptionId = op.Id, QuestionId = q.Id, SurveyId = CurrentSurvey.Id, DateEntered = DateTime.Now.ToString() };
                    if (CurrentSurvey.RequiresUserName)
                        r.UserName = User.LastName + ", " + User.FirstName;
                    await table.InsertAsync(r);
                }
            }
            IsBusy = false;
            return 0;
        }

        public void NextQuestion()
        {
            var index = CurrentSurvey.Questions.IndexOf(CurrentQuestion);
            index++;
            if (index < CurrentSurvey.Questions.Count)
            {
                CurrentQuestion = CurrentSurvey.Questions[index];

            }
        }
        public void BackQuestion()
        {
            var index = CurrentSurvey.Questions.IndexOf(CurrentQuestion);
            index--;
            if (index >-1)
            {
                CurrentQuestion = CurrentSurvey.Questions[index];

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
                CurrentQuestion = CurrentSurvey.Questions.First();
                return CurrentSurvey.Questions.Count;
            }
            IsBusy = false;
            return 0;
        }

        public async Task<int> FindSurveyCurrentChannel()
        {
        
            IsBusy = true;
            if (!string.IsNullOrEmpty(ChannelName))
            {
                var results = await AzureClient.GetTable<Survey>().Where(x => x.ChannelName == ChannelName).ToListAsync();
                if (results != null && results.Count>0)
                {
                    var survey = results.First();
                    if (!survey.IsLiveSurvey)
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
                        await LoadQuestionsForCurrentSurvey();
                        IsBusy = false;
                        return 1;
                    }
                    else
                    {
                        ErrorMessage = "Please Enter A Survey Code Associated To A Static Survey";
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
