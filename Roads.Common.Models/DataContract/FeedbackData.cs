using System.Runtime.Serialization;

namespace Roads.Common.Models.DataContract
{
    /// <summary>
    /// This class created to declare metadata for Feedback.
    /// </summary>
    [DataContract]
    public class FeedbackData
    {
        [DataMember]
        public int FeedbackId { get; set; }

        [DataMember]
        public System.DateTime SubmitTime { get; set; }

        [DataMember]
        public int RouteNodeId { get; set; }

        [DataMember]
        public int UserId { get; set; }

    }
}
