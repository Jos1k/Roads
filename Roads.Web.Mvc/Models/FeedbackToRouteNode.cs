using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Roads.Web.Mvc.Models
{
    public class FeedbackToRouteNode
    {
        /// <summary>
        /// Gets or sets the feedback identifier.
        /// </summary>
        /// <value>
        /// The feedback identifier.
        /// </value>
        public int? FeedbackId { get; set; }
        /// <summary>
        /// Gets or sets the origin city node identifier.
        /// </summary>
        /// <value>
        /// The origin city node identifier.
        /// </value>
        public string OriginCityNodeId { get; set; }
        /// <summary>
        /// Gets or sets the destination city node identifier.
        /// </summary>
        /// <value>
        /// The destination city node identifier.
        /// </value>
        public string DestinationCityNodeId { get; set; }
        /// <summary>
        /// Gets or sets the name of the origin city.
        /// </summary>
        /// <value>
        /// The name of the origin city.
        /// </value>
        public string OriginCityName { get; set; }
        /// <summary>
        /// Gets or sets the name of the destination city.
        /// </summary>
        /// <value>
        /// The name of the destination city.
        /// </value>
        public string DestinationCityName { get; set; }
        /// <summary>
        /// Gets or sets the route node identifier.
        /// </summary>
        /// <value>
        /// The route node identifier.
        /// </value>
        public int? RouteNodeId { get; set; }
        /// <summary>
        /// Gets or sets the feedback date time.
        /// </summary>
        /// <value>
        /// The feedback date time.
        /// </value>
        public DateTime FeedbackDateTime { get; set; }
        /// <summary>
        /// Gets or sets the feedback values.
        /// </summary>
        /// <value>
        /// The feedback values.
        /// </value>
        public List<FeedbackValue> FeedbackValues { get; set; }
    }
}