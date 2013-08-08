

using Skadoosh.Common.DomainModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Skadoosh.Common.ViewModels
{

    public class SkadooshVM : NotifyBase
    {


        private AccountUser currentUser;
        private ObservableCollection<Survey> surveys;
        private Question currentQuestion;
        private Survey currentSurvey;
        private ObservableCollection<Option> currentOptions;

        public ObservableCollection<Option> CurrentOptions
        {
            get { return currentOptions; }
            set { SetProperty<ObservableCollection<Option>>(ref currentOptions, value); }
        }

        public Survey CurrentSurvey
        {
            get { return currentSurvey; }
            set { SetProperty<Survey>(ref currentSurvey, value); }
        }

        public Question CurrentQuestion
        {
            get { return currentQuestion; }
            set { SetProperty<Question>(ref currentQuestion, value); }
        }
     
        //public IMobileServiceClient AzureClient
        //{
        //    get { return MobileService; }
        //}
        public AccountUser CurrentUser
        {
            get { return currentUser; }
            set { SetProperty<AccountUser>(ref currentUser, value); }
        }

        public ObservableCollection<Survey> Surveys
        {
            get { return surveys; }
            set { SetProperty<ObservableCollection<Survey>>(ref surveys, value); }
        }


        public SkadooshVM()
        {
            UnitTestTableInitialize();
            CurrentOptions = new ObservableCollection<Option>();
            Surveys = new ObservableCollection<Survey>();
        }

        private async void LoadSurveys()
        {
            //var table = MobileService.GetTable<Survey>();
           // List<Survey> results = await table.ToListAsync();
        }


        public async void UnitTestTableInitialize()
        {
            string azureKey = "cWjMTkNykoYJWqzkUeYFWjOcgLdwUs85";
            string azureUrl = "https://skadoosh.azure-mobile.net/";
            MobileServiceClient MobileService = new MobileServiceClient(azureUrl, azureKey);
            var s = new Survey() { SurveyTitle = "test", ChannelName="Ttrest", Description="asgsdga" };
            var table = MobileService.GetTable<Survey>();
            await table.InsertAsync(s);

            var question = new Question() { QuestionText = "Choose test test?", SurveyId = s.Id, QuestionType = 2 };
            await MobileService.GetTable<Question>().InsertAsync(question);


            var options = new List<Option>();
            options.Add(new Option() { OptionText = "Rain", QuestionId = question.Id });
            options.Add(new Option() { OptionText = "Snow", QuestionId = question.Id });
            options.Add(new Option() { OptionText = "Fire", QuestionId = question.Id });
            foreach (var opt in options)
            {
                await MobileService.GetTable<Option>().InsertAsync(opt);
            }
        }

        public async void SetMobileUser(MobileServiceUser mUser)
        {
            //var results = await MobileService.GetTable<AccountUser>().Where(x => x.UserId == mUser.UserId).ToCollectionAsync();
            //this.CurrentUser = results.FirstOrDefault();
            //if (this.CurrentUser == null)
            //{
            //    var newUser = mUser.CreateWith();
            //}
            
        }
        //public async void Login(MobileServiceAuthenticationProvider provider)
        //{
        //    MobileServiceUser user = null;
        //    while (user == null)
        //    {
        //        try
        //        {
        //            user = await MobileService.LoginAsync(provider);
        //            var results = await MobileService.GetTable<AccountUser>().Where(x => x.UserId == user.UserId).ToCollectionAsync();
        //            this.CurrentUser = results.FirstOrDefault();
        //            if (this.CurrentUser == null)
        //            {
        //                var newUser = user.CreateWith();
        //            }

        //        }
        //        catch (InvalidOperationException e)
        //        {
        //            //message = e + "You must log in. Login Required";
        //        }

        //    }

        //}

    }
}
