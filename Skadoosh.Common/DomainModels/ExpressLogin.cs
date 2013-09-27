namespace Skadoosh.Common.DomainModels
{
    public class ExpressLogin : NotifyBase
    {
        private string _password;


        public string Password
        {
            get { return _password; }
            set { _password = value; Notify("Password"); }
        }

        public string AccountFileName { get; set; }
        public int Id { get; set; }
        public string UserId { get; set; }
        public string MobileServiceAuthenticationToken { get; set; }
    }
}