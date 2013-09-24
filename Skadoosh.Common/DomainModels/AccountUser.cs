using System.Runtime.Serialization;

namespace Skadoosh.Common.DomainModels
{
    public class AccountUser : NotifyBase
    {
        private int _id;
        private string _userId;
        private string _firstName;
        private string _lastName;
        private string _email;

        public int Id
        {
            get { return _id; }
            set { _id = value; Notify("Id"); }
        }
        public string UserId
        {
            get { return _userId; }
            set { _userId = value; Notify("UserId"); }
        }
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; Notify("FirstName"); }
        }
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; Notify("LastName"); }
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; Notify("Email"); }
        }


        public AccountUser()
        {

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
        public bool IsValid
        {
            get
            {
                return (!string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName) && !string.IsNullOrEmpty(Email));
            }
        }

    }
}