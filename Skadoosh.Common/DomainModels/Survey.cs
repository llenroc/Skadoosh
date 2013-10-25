
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Skadoosh.Common.DomainModels
{


    public static class skadooshExtensions
    {
        public static AccountUser CreateWith(this MobileServiceUser mobileUser)
        {
            return new AccountUser() { UserId = mobileUser.UserId };
     
        }
    }


    public class Survey : NotifyBase
    {
        private int _id;
        private int _accountUserId;
        private string _channelName;
        private string _surveyTitle;
        private string _description;
        private bool _isActive;
        private bool _isLiveSurvey;
        private ObservableCollection<Question> _questions;
        private bool _requiresUserName;

        public int Id
        {
            get { return _id; }
            set { _id = value; Notify("Id"); }
        }
        public int AccountUserId
        {
            get { return _accountUserId; }
            set { _accountUserId = value; Notify("AccountUserId"); }
        }
        public string ChannelName
        {
            get { return _channelName; }
            set { _channelName = value; Notify("ChannelName"); }
        }
        public string SurveyTitle
        {
            get { return _surveyTitle; }
            set { _surveyTitle = value; Notify("SurveyTitle"); }
        }
        public string Description
        {
            get { return _description; }
            set { _description = value; Notify("Description"); }
        }
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; Notify("IsActive"); }
        }
        public bool IsLiveSurvey
        {
            get { return _isLiveSurvey; }
            set { _isLiveSurvey = value; Notify("IsLiveSurvey"); }
        }
        public bool RequiresUserName
        {
            get { return _requiresUserName; }
            set { _requiresUserName = value; Notify("RequiresUserName"); }
        }

        [IgnoreDataMember]
        public bool IsNew
        {
            get
            {
                return (this.Id == 0);
            }
        }
        [IgnoreDataMember]
        public ObservableCollection<Question> Questions
        {
            get { return _questions; }
            set { _questions = value; Notify("Questions"); }
        }
        [IgnoreDataMember]
        public bool IsValid
        {
            get
            {
                return (!string.IsNullOrEmpty(ChannelName) && !string.IsNullOrEmpty(SurveyTitle) && !string.IsNullOrEmpty(Description));
            }
        }
        public Survey()
        {
            Questions = new ObservableCollection<Question>();
        }

    }


}
