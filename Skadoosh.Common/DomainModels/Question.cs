using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Linq;

namespace Skadoosh.Common.DomainModels
{
    public class Question : NotifyBase
    {
        private int _id;
        private int _surveyId;
        private string _questionText;
        private bool _isMultiSelect;
        private bool _isActive;
        private ObservableCollection<Option> _options;
        private string _questionTypeState;

        public int Id
        {
            get { return _id; }
            set { _id = value; Notify("Id"); }
        }
        public int SurveyId
        {
            get { return _surveyId; }
            set { _surveyId = value; Notify("SurveyId");}
        }
        public string QuestionText
        {
            get { return _questionText; }
            set { _questionText = value;  Notify("QuestionText");  }
        }

        public bool IsMultiSelect
        {
            get { return _isMultiSelect; }
            set { _isMultiSelect = value; Notify("IsMultiSelect"); SetQuestionTypeState(); }
        }
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; Notify("IsActive"); SetQuestionTypeState(); }
        }

        [IgnoreDataMember]
        public ObservableCollection<Option> Options
        {
            get { return _options; }
            set { _options = value; Notify("Options"); }
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
        public string QuestionTypeState
        {
            get { return _questionTypeState; }
            set { _questionTypeState = value; Notify("QuestionTypeState"); }
        }
        [IgnoreDataMember]
        public bool IsValid
        {
            get
            {
                return (!string.IsNullOrEmpty(QuestionText));
            }
        }
        [IgnoreDataMember]
        public string QuestionHintText
        {
            get { return IsMultiSelect ? "Select all that apply" : "Single Selection"; }
        }
      
        public Question()
        {
            Options = new ObservableCollection<Option>();
        }
        private void SetQuestionTypeState()
        {
            var type = IsMultiSelect ? "Multiple Select" : "Single Select";
            var state = IsActive ? "Active" : "Not Active";
            QuestionTypeState = string.Format("{0} : {1}", type, state);
        }
    }
}