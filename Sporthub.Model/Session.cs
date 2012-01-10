namespace Sporthub.Model
{
    public class Session
    {
        public bool IsRedirectedsFromActivation { get; set; }
        public bool IsRedirectedToLogin { get; set; }
        public bool IsLogout { get; set; }
        public bool ShowFeedback { get; set; }
        public bool IsNewMember { get; set; }
        public bool IsNotTracked { get; set; }
        public string ReturnUrl { get; set; }
        public string FacebookAccessToken { get; set; }
        public Feedback Feedback { get; set; }

        public Session()
        {
            IsRedirectedsFromActivation = false;
            IsRedirectedToLogin = false;
            ShowFeedback = false;
            IsNewMember = false;
            IsNotTracked = false;
            IsLogout = false;
        }
    }
}
