using System;
using System.Runtime.Serialization;

namespace Roads.Common.Models.DataContract
{
    /// <summary>
    /// The HolisticFeedback class.
    /// </summary>
    [DataContract]
    public class HolisticFeedback
    {
        [DataMember]
        public long FeedbackId { get; set; }

        [DataMember]
        public long UserId { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public DateTime SubmitTime { get; set; }

        [DataMember]
        public string Value { get; set; }

        [DataMember]
        public string OriginCityName { get; set; }

        [DataMember]
        public string DestinationCityName { get; set; }

        [DataMember]
        public FeedbackItemData FeedbackItem { get; set; }

        [DataMember]
        public FeedbackModelData FeedbackModel { get; set; }
    }
}
