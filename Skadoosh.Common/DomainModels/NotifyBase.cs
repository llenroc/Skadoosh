using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Skadoosh.Common.DomainModels
{

    public class Global
    {
        private static readonly string azureKey = "cWjMTkNykoYJWqzkUeYFWjOcgLdwUs85";
        private static readonly string azureUrl = "https://skadoosh.azure-mobile.net/";
        public static MobileServiceClient MobileService = new MobileServiceClient(azureUrl, azureKey);
    }


    public abstract class NotifyBase : INotifyPropertyChanged
    {
       
        public event PropertyChangedEventHandler PropertyChanged;

        public void Notify(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }

    public class ParticipateBase : ViewModelBase
    {
        private string channelSelected;
        private Survey selectedSurvey;

        public Survey SelectedSurvey
        {
            get { return selectedSurvey; }
            set { selectedSurvey = value; Notify("SelectedSurvey"); }
        }

        public string ChannelSelected
        {
            get
            {
                return channelSelected;
            }
            set
            {
                channelSelected = value; Notify("ChannelSelected");
            }
        }

        public async Task<bool> FindSurvey()
        {
            var list = AzureClient.GetTable<Survey>().Where(x => x.ChannelName == ChannelSelected).ToListAsync();
            var collection = await list;
            var survey = collection.FirstOrDefault();
            if (survey != null)
            {
                SelectedSurvey = survey;
                return true;
            }
            else
            {
                return false;
            }
        }
    }


    public abstract class ViewModelBase : NotifyBase
    {

        public MobileServiceClient AzureClient
        {
            get
            {
                return Global.MobileService;
            }
            set
            {
                Global.MobileService = value;
            }
        }


        public async Task<bool> ProfileExists()
        {
            var list = await AzureClient.GetTable<AccountUser>().Where(x => x.UserId == AzureClient.CurrentUser.UserId).ToListAsync();

            if (list != null && list.FirstOrDefault() != null)
            {
                return true;
            }
            else{
                return false;
            }
        }

    }
}
