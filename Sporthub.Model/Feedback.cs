using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model.Enumerators;

namespace Sporthub.Model
{
    public class Feedback
    {
        public Feedback(FeedbackType feedbackType, string message, bool doFade, bool showClose) 
        {
            FeedbackType = feedbackType;
            Message = message;
            DoFade = doFade;
            ShowClose = showClose;
        }

        public Feedback()
        {
            Message = "";
            DoFade = true;
            ShowClose = false;
        }

        public Feedback(FeedbackType feedbackType, string message)
        {
            FeedbackType = feedbackType;
            Message = message;
            DoFade = true;
            ShowClose = false;
        }

        public FeedbackType FeedbackType { get; set; }
        public string Message { get; set; }
        public bool DoFade { get; set; }
        public bool ShowClose { get; set; }


    }
}
