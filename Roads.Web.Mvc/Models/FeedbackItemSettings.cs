using System.ComponentModel.DataAnnotations;

namespace Roads.Web.Mvc.Models
{
    public class FeedbackItemSettings
    {
        public int feedbackItemId { get; set; }

        [RoadsDisplayName("FBS_Label_FeedbackSettingName")]
        public string settingName { get; set; }

        [RoadsRequired("FBS_ErrorMessage_EmptyMandatoryFields")]
        [RoadsDisplayName("FBS_Label_FeedbackSortNumber")]
        [RoadsRegularExpression("([1-9][0-9]*)", "FBS_ErrorMessage_InvalidValueSortNumber")]
        public int sortNumber { get; set; }

        [RoadsDisplayName("FBS_Label_FeedbackSettinDescription")]
        public string description { get; set; }

        [RoadsDisplayName("FBS_Label_FeedbackIsNumeric")]
        public bool isNumeric { get; set; }

        [RoadsDisplayName("FBS_Label_FeedbackIsMandatory")]
        public bool isMandatory { get; set; }

        [RoadsDisplayName("FBS_Label_FeedbackModel")]
        public int feedbackModelId { get; set; }
    }
}