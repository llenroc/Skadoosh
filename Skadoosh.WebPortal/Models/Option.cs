using System.Runtime.Serialization;

namespace Skadoosh.Common.DomainModels
{
    public class Option : NotifyBase
    {
        private int _id;
        private int _questionId;
        private string _optionText;
        private bool _isDeleted;
        private bool _isSelected;


        
        public int Id
        {
            get { return _id; }
            set { _id = value; Notify("Id"); }
        }
        public int QuestionId
        {
            get { return _questionId; }
            set { _questionId = value; Notify("QuestionId"); }
        }
        public string OptionText
        {
            get { return _optionText; }
            set
            {
                _optionText = value;
                Notify("OptionText");
                if (string.IsNullOrEmpty(value))
                {
                    IsDeleted = true;
                }
            }
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
        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; Notify("IsSelected"); }
        }
        [IgnoreDataMember]
        public bool IsDeleted
        {
            get { return _isDeleted; }
            set { _isDeleted = value; Notify("IsDeleted"); }
        }
        [IgnoreDataMember]
        public bool IsValid
        {
            get
            {
                return (!string.IsNullOrEmpty(OptionText));
            }
        }
        
    }
}