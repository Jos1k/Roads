using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Roads.Common.Models.DataContract
{
    /// <summary>
    /// The RouteDetailsFeedback class.
    /// </summary>
    [DataContract]
    public class RouteDetailsFeedback
    {
        [DataMember]
        public long FeedbackId { get; set; }

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
        public int OriginCityId { get; set; }

        [DataMember]
        public int DestinationCityId { get; set; }

        [DataMember]
        public List<FeedbackValueData> FeedbackValues { get; set; }
    }
}