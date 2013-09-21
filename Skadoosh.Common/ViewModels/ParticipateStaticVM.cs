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
        private Survey currentSurvey;
        private Question currentQuestion;
        private string channelName;
        private string errorMessage;
        private bool isLastQuestion;
        private string currentPosition;

        public string CurrentPostition
        {
            get { return currentPosition; }
            set { currentPosition = value; Notify("CurrentPostition"); }
        }
        
        public bool IsLastQuestion
        {
            get { return isLastQuestion; }
            set { isLastQuestion = value; Notify("IsLastQuestion"); }
        }

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; Notify("ErrorMessage"); }
        }
        
        public string ChannelName
        {
            get { return channelName; }
            set { channelName = value; Notify("ChannelName"); }
        }
        
        public Question CurrentQuestion
        {
            get { return currentQuestion; }
            set
            {
                currentQuestion = value;
                IsLastQuestion = (CurrentSurvey.Questions.Last().Id == value.Id);
                CurrentPostition = (CurrentSurvey.Questions.IndexOf(value) + 1) + " of " + CurrentSurvey.Questions.Count;
                Notify("CurrentQuestion");
            }
        }

        public Survey CurrentSurvey
        {
            get { return currentSurvey; }
            set
            {
                currentSurvey = value;
                Notify("CurrentSurvey");
            }
        }

        public ParticipateStaticVM()
        {
          
        }

        public async Task<int> SaveSurveyResponses()
        {
            var table = AzureClient.GetTable<Responses>();
            foreach (var q in CurrentSurvey.Questions)
            {
                foreach (var op in q.Options.Where(x => x.IsSelected))
                {
                    var r = new Responses() { OptionId = op.Id, QuestionId = q.Id, SurveyId = CurrentSurvey.Id };
                    if (CurrentSurvey.RequiresUserName)
                        r.UserName = User.LastName + ", " + User.FirstName;
                    await table.InsertAsync(r);
                }
            }
            return 0;
        }
        public async Task<int> LoadSurveysForCurrentChannel()
        {
            if (!string.IsNullOrEmpty(this.ChannelName))
            {
                var results = await AzureClient.GetTable<Survey>().Where(x => x.ChannelName == ChannelName).ToListAsync();
                if (results != null && results.Any())
                {
                    CurrentSurvey = results.First();
                    await LoadQuestionsForCurrentSurvey();
                    
                }
            }
            return 0;
        }

        public async Task<int> LoadQuestionsForCurrentSurvey()
        {
            if (CurrentSurvey != null)
            {
                currentSurvey.Questions.Clear();
                var results = await AzureClient.GetTable<Question>().Where(x => x.SurveyId == CurrentSurvey.Id).ToListAsync();

                foreach (var q in results)
                {
                    var subResults = await AzureClient.GetTable<Option>().Where(x => x.QuestionId == q.Id).ToListAsync();
                    foreach (var o in subResults)
                    {
                        q.Options.Add(o);
                    }
                    currentSurvey.Questions.Add(q);
                }
                CurrentQuestion = CurrentSurvey.Questions.First();
                return CurrentSurvey.Questions.Count;
            }
            return 0;
        }

        public async Task<int> FindSurveyCurrentChannel()
        {
            if (!string.IsNullOrEmpty(ChannelName))
            {
                var results = await AzureClient.GetTable<Survey>().Where(x => x.ChannelName == ChannelName).ToListAsync();
                if (results != null && results.Any())
                {
                    var survey = results.First();
                    if (!survey.IsLiveSurvey)
                    {
                        if (survey.RequiresUserName && !string.IsNullOrEmpty(User.FirstName) && !string.IsNullOrEmpty(User.LastName))
                        {
                            ErrorMessage = string.Empty;
                            CurrentSurvey = survey;
                            await LoadQuestionsForCurrentSurvey();
                            return 1;
                        }
                        else
                        {
                            ErrorMessage = "This Survey Requires Your First And Last Name";
                        }
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
            return 0;
        }
    }
}
