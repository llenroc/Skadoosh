
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
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

    }

    public class Survey : NotifyBase
    {
        private int id;
        private int accountUserId;
        private string channelName;
        private string surveyTitle;
        private string description;
        private DateTime? startTime;
        private DateTime? endTime;
        public int Id
        {
            get { return id; }
            set { id = value; Notify("Id"); }
        }
        public int AccountUserId
        {
            get { return accountUserId; }
            set { accountUserId = value; Notify("AccountUserId");}
        }
        public string ChannelName
        {
            get { return channelName; }
            set { channelName = value; Notify("ChannelName"); }
        }
        public string SurveyTitle
        {
            get { return surveyTitle; }
            set { surveyTitle = value; Notify("SurveyTitle");}
        }
        public string Description
        {
            get { return description; }
            set { description = value; Notify("Description");}
        }
        public DateTime? StartTime
        {
            get { return startTime; }
            set { startTime = value; Notify("StartTime");}
        }
        public DateTime? EndTime
        {
            get { return endTime; }
            set { endTime = value; Notify("EndTime");}
        }
        
    }

    public class Question : NotifyBase
    {
        private int id;
        private int surveyId;
        private string questionText;
        private int questionType;

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

        public int QuestionType
        {
            get { return questionType; }
            set { questionType = value; Notify("QuestionType");}
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
