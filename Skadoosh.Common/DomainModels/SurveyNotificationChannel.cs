using System;
using System.Runtime.Serialization;

namespace Skadoosh.Common.DomainModels
{
    public class SurveyNotificationChannel : NotifyBase
    {
        private int _id;
        private string _channelName;
        private string _urlNotification;
        private DateTime _channelExpirationDate;
        private string _clientType;

        public int Id
        {
            get { return _id; }
            set { _id = value; Notify("Id"); }
        }
        public string ChannelName
        {
            get { return _channelName; }
            set { _channelName = value; Notify("ChannelName"); }
        }
        public string UrlNotification
        {
            get { return _urlNotification; }
            set { _urlNotification = value; Notify("UrlNotification"); }
        }
        public DateTime ChannelExpirationDate
        {
            get { return _channelExpirationDate; }
            set { _channelExpirationDate = value; Notify("ChannelExpirationDate"); }
        }
        public string ClientType
        {
            get { return _clientType; }
            set { _clientType = value; Notify("ClientType"); }
        }
        [IgnoreDataMember]
        public bool IsValid
        {
            get
            {
                return (!string.IsNullOrEmpty(ChannelName) && !string.IsNullOrEmpty(UrlNotification));
            }
        }
        
    }
}