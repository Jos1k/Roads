using System.Runtime.Serialization;

namespace Roads.Common.Models.DataContract
{
    /// <summary>
    /// This class created to declare DataContract for using FeedbackValue type in WCF data contract.
    /// </summary>
    [DataContract]
    public class FeedbackValueData
    {
        [DataMember]
        public int FeedbackValueId { get; set; }

        [DataMember]
        public string Value { get; set; }

        [DataMember]
        public int FeedbackId { get; set; }

        [DataMember]
        public int FeedbackItemId { get; set; }

        [DataMember]
        public virtual FeedbackItemData FeedbackItem { get; set; }
    }
}

