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

    public class ViewModelBase : NotifyBase
    {

        private AccountUser user;

        public AccountUser User
        {
            get { return user; }
            set { user = value; Notify("User"); }
        }


        public bool IsLoggedOn
        {
            get
            {
                return (User!=null && User.Id != 0 && !string.IsNullOrEmpty(this.User.UserId));
            }
        }
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

        public ViewModelBase()
        {
            User = new AccountUser();
        }

        public void Logout()
        {
            AzureClient.Logout();
            this.User = null;
        }
        public async Task<bool> CreateProfile()
        {
            var list = await AzureClient.GetTable<AccountUser>().Where(x => x.UserId == User.UserId).ToListAsync();
            if (list == null || list.FirstOrDefault() == null)
            {
                var table = AzureClient.GetTable<AccountUser>();
                await table.InsertAsync(User);
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> ProfileExists()
        {
            var list = await AzureClient.GetTable<AccountUser>().Where(x => x.UserId == AzureClient.CurrentUser.UserId).ToListAsync();

            if (list != null && list.FirstOrDefault() != null)
            {
                User = list.First();
                return true;
            }
            else{
                User.UserId = AzureClient.CurrentUser.UserId;
                return false;
            }
        }

        public async Task<int> RegisterForNotification(string deviceUri, string deviceType, string channelName)
        {
            var item = new SurveyNotificationChannel
            {
                UrlNotification = deviceUri,
                ChannelName = channelName,
                ChannelExpirationDate = DateTime.Now.AddDays(30),
                ClientType = deviceType
            };
            var table = AzureClient.GetTable<SurveyNotificationChannel>();
            var existing = await table.Where(x => x.UrlNotification == deviceUri).ToListAsync();
            if (existing.Any())
            {
                item = existing.First();
                await table.DeleteAsync(item);
            }
            await table.InsertAsync(item);
            return item.Id;
        }

    }
}
