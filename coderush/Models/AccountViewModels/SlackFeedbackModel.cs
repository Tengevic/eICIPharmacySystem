using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Models.AccountViewModels
{
    public class SlackFeedbackModel
    {
        public string channel { get; set; }
        public string text { get; set; }
        public List<Attachments> attachments { get; set; }
    }
    public class Attachments
    {
        public string pretext { get; set; }
        public string text { get; set; }

    }
    public class UserFeedbackModel
    {
        public string Title { get; set; }
        public string Issue { get; set; }
        public string Comments { get; set; }
        public bool IsSubmited { get; set; } = false;

    }
}
