using System;

namespace Skadoosh.Common.DomainModels
{
    public class Responses : NotifyBase
    {
        private int _id;
        private int _surveyId;
        private int _questionId;
        private int _optionId;
        private string _userName;

        public int Id
        {
            get { return _id; }
            set { _id = value; Notify("Id"); }
        }
        public int SurveyId
        {
            get { return _surveyId; }
            set { _surveyId = value; Notify("SurveyId"); }
        }
        public int QuestionId
        {
            get { return _questionId; }
            set { _questionId = value; Notify("QuestionId"); }
        }
        public int OptionId
        {
            get { return _optionId; }
            set { _optionId = value; Notify("OptionId"); }
        }

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; Notify("UserName"); }
        }

        public DateTime DateEntered { get; set; }

    }
}