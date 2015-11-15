using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Roads.Web.Mvc.Models
{
    public class FeedbackItemModel
    {
        public List<FeedbackItemSettings> feedbackItemSettings = new List<FeedbackItemSettings>();

        public Dictionary<Int64,string> modelsNames = new Dictionary<long, string>();

        public List<Language> availableLanguages = new List<Language>();

        public List<DynamicTranslation> dynamicTranslations = new List<DynamicTranslation>();
    }
}
