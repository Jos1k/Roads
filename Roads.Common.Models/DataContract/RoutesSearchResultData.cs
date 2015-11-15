using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Roads.Common.Models.DataContract
{
    [DataContract]
    public class RoutesSearchResultData
    {
        public RoutesSearchResultData()
        {
            Treks = new List<TrekData>();
        }

        [DataMember]
        public long FeedbacksCount { get; set; }

        [DataMember]
        public int PageNumber { get; set; }

        [DataMember]
        public long Count { get; set; }

        [DataMember]
        public long ActualRange { get; set; }

        [DataMember]
        public int RecordsPerPage { get; set; }

        [DataMember]
        public List<TrekData> Treks { get; set; }
    }
}
