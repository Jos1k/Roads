using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Roads.Common.Models.DataContract
{
    [DataContract]
    public class RouteNodeWithFeedbacksData
    {
        /// <summary>
        /// Gets or sets the route node identifier.
        /// </summary>
        /// <value>
        /// The route node identifier.
        /// </value>
        [DataMember]
        public int? RouteNodeId { get; set; }
        /// <summary>
        /// Gets or sets the origin city node identifier.
        /// </summary>
        /// <value>
        /// The origin city node identifier.
        /// </value>
        [DataMember]
        public int OriginCityNodeId { get; set; }
        /// <summary>
        /// Gets or sets the origin city node.
        /// </summary>
        /// <value>
        /// The origin city node.
        /// </value>
        [DataMember]
        public string OriginCityNode { get; set; }
        /// <summary>
        /// Gets or sets the destination city node identifier.
        /// </summary>
        /// <value>
        /// The destination city node identifier.
        /// </value>
        [DataMember]
        public int DestinationCityNodeId { get; set; }
        /// <summary>
        /// Gets or sets the destination city node.
        /// </summary>
        /// <value>
        /// The destination city node.
        /// </value>
        [DataMember]
        public string DestinationCityNode { get; set; }
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        [DataMember]
        public int UserId { get; set; }
        /// <summary>
        /// Gets or sets the submit time.
        /// </summary>
        /// <value>
        /// The submit time.
        /// </value>
        [DataMember]
        public DateTime SubmitTime { get; set; }

        /// <summary>
        /// Gets or sets the feedback identifier.
        /// </summary>
        /// <value>
        /// The feedback identifier.
        /// </value>
        [DataMember]
        public int FeedbackId { get; set; }


        /// <summary>
        /// The feedback values
        /// </summary>
        [DataMember]
        public List<FeedbackValueData> FeedbackValues;
    }
}
