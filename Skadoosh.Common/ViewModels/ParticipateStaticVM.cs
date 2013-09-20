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
                return CurrentSurvey.Questions.Count;
            }
            return 0;
        }
    }
}
