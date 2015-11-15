using System.Runtime.Serialization;

namespace Roads.Common.Models.DataContract
{
    /// <summary>
    /// This class created to declare metadata for FeedbackModel.
    /// </summary>
    [DataContract]
    public class FeedbackModelData
    {
        [DataMember]
        public int FeedbackModelId { get; set; }

        [DataMember]
        public string HtmlCode { get; set; }

        [DataMember]
        public string JavascriptCode { get; set; }

        [DataMember]
        public string FeedBackModalName { get; set; }
    }
}
