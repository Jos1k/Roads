using System.Runtime.Serialization;

namespace Roads.Common.Models.DataContract
{
    /// <summary>
    /// This class created to declare metadata for RouteNode.
    /// </summary>
    [DataContract]
    public class RouteNodeData
    {
        [DataMember]
        public int RouteNodeId { get; set; }

        [DataMember]
        public int OriginCityNodeId { get; set; }

        [DataMember]
        public int DestinationCityNodeId { get; set; }
    }
}
