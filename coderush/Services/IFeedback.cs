using coderush.Models.AccountViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Services
{
    public interface IFeedback
    {
        void SendUserFeedback(SlackFeedbackModel slackFeedbackModel);
    }
}
