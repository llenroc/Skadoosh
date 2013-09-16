
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Skadoosh.Common.DomainModels
{

    public enum QuestionType
    {
        Single,
        Multiple
    }

    public static class SkadooshExtensions
    {
        public static AccountUser CreateWith(this MobileServiceUser mobileUser)
        {
            return new AccountUser() { UserId = mobileUser.UserId };
     
        }
    }

    public class AccountUser : NotifyBase
    {
        private int id;
        private string userId;
        private string firstName;
        private string lastName;
        private string email;

        public int Id
        {
            get { return id; }
            set { id = value; Notify("Id"); }
        }
        public string UserId
        {
            get { return userId; }
            set { userId = value; Notify("UserId"); }
        }
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; Notify("FirstName"); }
        }
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; Notify("LastName"); }
        }
        public string Email
        {
            get { return email; }
            set { email = value; Notify("Email"); }
        }

        public AccountUser()
        {

        }

    }

    public class Survey : NotifyBase
    {
        private int id;
        private int accountUserId;
        private string channelName;
        private string surveyTitle;
        private string description;
        private bool isActive;
        private bool isLiveSurvey;
        private ObservableCollection<Question> questions;

        [IgnoreDataMember]
        public ObservableCollection<Question> Questions
        {
            get { return questions; }
            set { questions = value; Notify("Questions"); }
        }

        public int Id
        {
            get { return id; }
            set { id = value; Notify("Id"); }
        }
        public int AccountUserId
        {
            get { return accountUserId; }
            set { accountUserId = value; Notify("AccountUserId"); }
        }
        public string ChannelName
        {
            get { return channelName; }
            set { channelName = value; Notify("ChannelName"); }
        }
        public string SurveyTitle
        {
            get { return surveyTitle; }
            set { surveyTitle = value; Notify("SurveyTitle"); }
        }
        public string Description
        {
            get { return description; }
            set { description = value; Notify("Description"); }
        }
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; Notify("IsActive"); }
        }
        public bool IsLiveSurvey
        {
            get { return isLiveSurvey; }
            set { isLiveSurvey = value; Notify("IsLiveSurvey"); }
        }

        public bool IsNew
        {
            get
            {
                return (this.Id == 0);
            }
        }

        public Survey()
        {
            Questions = new ObservableCollection<Question>();
        }
    }

    public class Question : NotifyBase
    {
        private int id;
        private int surveyId;
        private string questionText;
        private bool isMultiSelect;
        private bool isActive;
        private ObservableCollection<Option> options;

        [IgnoreDataMember]
	    public ObservableCollection<Option> Options
	    {
		    get { return options;}
		    set { options = value; Notify("Options");}
	    }
	
        public int Id
        {
            get { return id; }
            set { id = value; Notify("Id"); }
        }
        public int SurveyId
        {
            get { return surveyId; }
            set { surveyId = value; Notify("SurveyId");}
        }
        public string QuestionText
        {
            get { return questionText; }
            set { questionText = value;  Notify("QuestionText");}
        }

        public bool IsMultiSelect
        {
            get { return isMultiSelect; }
            set { isMultiSelect = value; Notify("IsMultiSelect"); }
        }
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; Notify("IsActive"); }
        }

        public bool IsNew
        {
            get
            {
                return (this.Id == 0);
            }
        }

        public Question()
        {
            Options = new ObservableCollection<Option>();
        }
    }

    public class Responses : NotifyBase
    {
        private int id;
        private int surveyId;
        private int questionId;
        private int optionId;
        private int? accountId;

        public int Id
        {
            get { return id; }
            set { id = value; Notify("Id"); }
        }
        public int SurveyId
        {
            get { return surveyId; }
            set { surveyId = value; Notify("SurveyId"); }
        }
        public int QuestionId
        {
            get { return questionId; }
            set { questionId = value; Notify("QuestionId");}
        }
        public int OptionId
        {
            get { return optionId; }
            set { optionId = value; Notify("OptionId");}
        }
        public int? AccountId
        {
            get { return accountId; }
            set { accountId = value; Notify("AccountId");}
        }
        
    }

    public class Option : NotifyBase
    {
        private int id;
        private int questionId;
        private string optionText;

        public int Id
        {
            get { return id; }
            set { id = value; Notify("Id"); }
        }
        public int QuestionId
        {
            get { return questionId; }
            set { questionId = value; Notify("QuestionId"); }
        }
        public string OptionText
        {
            get { return optionText; }
            set { optionText = value; Notify("OptionText"); }
        }

        public bool IsNew
        {
            get
            {
                return (this.Id == 0);
            }
        }
        
    }

    public class SurveyNotificationChannel : NotifyBase
    {
        private int id;
        private string channelName;
        private string urlNotification;
        public int Id
        {
            get { return id; }
            set { id = value; Notify("Id"); }
        }
        public string ChannelName
        {
            get { return channelName; }
            set { channelName = value; Notify("ChannelName"); }
        }
        public string UrlNotification
        {
            get { return urlNotification; }
            set { urlNotification = value; Notify("UrlNotification"); }
        }
        
    }
}
