
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skadoosh.Common.DomainModels
{

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
            set { SetProperty<int>(ref id, value); }
        }
        public string UserId
        {
            get { return userId; }
            set { SetProperty<string>(ref userId, value); }
        }
        public string FirstName
        {
            get { return firstName; }
            set { SetProperty<string>(ref firstName, value); }
        }
        public string LastName
        {
            get { return lastName; }
            set { SetProperty<string>(ref lastName, value); }
        }
        public string Email
        {
            get { return email; }
            set { SetProperty<string>(ref email, value); }
        }

    }

    public class Survey
    {
        public int Id { get; set; }
        public int AccountUserId { get; set; }
        public string ChannelName { get; set; }
        public string SurveyTitle { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }

    public class Question
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public string QuestionText { get; set; }
        public int QuestionType { get; set; }
        
    }

    public class Responses
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public int QuestionId { get; set; }
        public int OptionId { get; set; }
        public int? AccountId { get; set; }
    }

    public class Option
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string OptionText { get; set; }
    }

    public class SurveyNotificationChannel
    {
        public int Id { get; set; }
        public string ChannelName { get; set; }
        public string UrlNotification { get; set; }
    }
}
