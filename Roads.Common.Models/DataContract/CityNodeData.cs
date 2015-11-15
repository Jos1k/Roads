using System.Runtime.Serialization;

namespace Roads.Common.Models.DataContract
{
    [DataContract]
    public class CityNodeData
    {
        [DataMember]
        public int CityNodeId { get; set; }

        [DataMember]
        public int RegionNodeId { get; set; }

        [DataMember]
        public string LanguageKey { get; set; }
    }
}
