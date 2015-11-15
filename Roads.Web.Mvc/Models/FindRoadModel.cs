using System.ComponentModel.DataAnnotations;

namespace Roads.Web.Mvc.Models
{
    /// <summary>
    /// The FindRoadModel class for FindRoad.
    /// </summary>
    public class FindRoadModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FindRoadModel"/> class.
        /// </summary>
        public FindRoadModel()
        {
            SearchResult = new ListRoadsViewModel();
            OriginCityNodeId = -1;
            DestinationCityNodeId = -1;
        }

        /// <summary>
        /// Gets or sets the origin city node identifier.
        /// </summary>
        /// <value>
        /// The origin city node identifier.
        /// </value>
        public int OriginCityNodeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the origin city node.
        /// </summary>
        /// <value>
        /// The name of the origin city node.
        /// </value>
        [RoadsRequired("FR_Validation_RoadRequired")]
        public string OriginCityNodeName { get; set; }

        /// <summary>
        /// Gets or sets the destination city node identifier.
        /// </summary>
        /// <value>
        /// The destination city node identifier.
        /// </value>
        public int DestinationCityNodeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the destination city node.
        /// </summary>
        /// <value>
        /// The name of the destination city node.
        /// </value>
        [RoadsRequired("FR_Validation_RoadRequired")]
        public string DestinationCityNodeName { get; set; }

        public ListRoadsViewModel SearchResult { get; set; }
    }
}