using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Roads.Common.Models.DataContract
{
    /// <summary>
    /// This class created to declare metadata for RegionNode.
    /// </summary>
    [DataContract]
    public class RegionNodeData
    {
        [DataMember]
        public int RegionNodeId { get; set; }

        [DataMember]
        public string LanguageKey { get; set; }

        [DataMember]
        public virtual ICollection<CityNodeData> CityNodes { get; set; }
    }
}
