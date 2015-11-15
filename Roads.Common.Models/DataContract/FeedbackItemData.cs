using System.Runtime.Serialization;

namespace Roads.Common.Models.DataContract
{
    /// <summary>
    /// This class created to declare DataContract for using FeedbackItem type in WCF data contract.
    /// </summary>
    [DataContract]
    public class FeedbackItemData
    {
        [DataMember]
        public int FeedbackItemId { get; set; }

        [DataMember]
        public bool Mandatory { get; set; }

        [DataMember]
        public int SortNumber { get; set; }

        [DataMember]
        public bool IsNumeric { get; set; }

        [DataMember]
        public int FeedbackModelId { get; set; }

        [DataMember]
        public string NameTranslationKey { get; set; }

        [DataMember]
        public string DescriptionTranslationKey { get; set; }

    }
}
