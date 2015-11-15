using System;
using System.Runtime.Serialization;

namespace Roads.Common.Models.DataContract
{
    /// <summary>
    /// This class created to declare metadata for Trek.
    /// </summary>
    [DataContract]
    public class TrekData
    {
        [DataMember]
        public string Hash { get; set; }

        [DataMember]
        public DateTime TreckDate { get; set; }

        [DataMember]
        public long FeedbackCount { get; set; }

        [DataMember]
        public string Track { get; set; }

        [DataMember]
        public long TrekId { get; set; }

        [DataMember]
        public string OriginCityNodeName { get; set; }

        [DataMember]
        public int OriginCityNodeId { get; set; }

        [DataMember]
        public string DesinationCityNodeName { get; set; }

        [DataMember]
        public int DesinationCityNodeId { get; set; }

        [DataMember]
        public virtual CityNodeData OriginCityNode { get; set; }

        [DataMember]
        public virtual CityNodeData DestinationCityNode { get; set; }
    }
}
