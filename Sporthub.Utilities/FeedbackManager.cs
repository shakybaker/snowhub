using Sporthub.Model;
using Sporthub.Model.Enumerators;

namespace Sporthub.Utilities
{
    public class FeedbackManager
    {
        public static void AddFeedback(FeedbackType type, string message)
        {
            var feedback = new Feedback(type, message);

            if (SessionContext.CurrentSession == null)
                SessionContext.CurrentSession = new Session();

            SessionContext.CurrentSession.ShowFeedback = true;
            SessionContext.CurrentSession.Feedback = feedback;
        }
    }
}
