using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Roads.Web.Mvc.Models
{
    public class DynamicTranslation
    {
        public int DynamicObjectId { get; set; }

        [RoadsRequired("FBS_ErrorMessage_EmptyMandatoryFields")]
        public string Value { get; set; }

        [RoadsRequired("FBS_ErrorMessage_EmptyMandatoryFields")]
        public string DescriptionValue { get; set; }
        public int LanguageId { get; set; }
        public string DynamicKey { get; set; }
    }
}