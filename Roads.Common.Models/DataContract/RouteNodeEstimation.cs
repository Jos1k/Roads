using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Roads.Common.Models.DataContract
{
    /// <summary>
    /// The RouteNodeEstimation class.
    /// </summary>
    [DataContract]
    public class RouteNodeEstimation
    {
        public RouteNodeEstimation()
        {
            HolisticFeedbacks = new List<HolisticFeedback>();
        }

        /// <summary>  Gets or sets the route node.  </summary>
        /// <value>  The route node.  </value>
        [DataMember]
        public RouteNodeData RouteNode { get; set; }

        /// <summary>  Gets or sets the holistic feedbacks. </summary>
        /// <value>  The holistic feedbacks. </value>
        [DataMember]
        public List<HolisticFeedback> HolisticFeedbacks { get; set; }

    }
}
