using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Roads.Common.Models.DataContract
{
    /// <summary>
    /// The Route Estimation class.
    /// </summary>
    [DataContract]
    public class RouteEstimation
    {
        public RouteEstimation()
        {
            NodesEstimations = new List<RouteNodeEstimation>();
        }

        /// <summary> Gets or sets the city points ids. </summary>
        /// <value> The city points ids. </value>
        [DataMember]
        public int[] CityPointsIds { get; set; }

        /// <summary> Gets or sets the route nodes. </summary>
        /// <value>  The route nodes.  </value>
        [DataMember]
        public List<RouteNodeEstimation> NodesEstimations { get; set; }
    }
}
