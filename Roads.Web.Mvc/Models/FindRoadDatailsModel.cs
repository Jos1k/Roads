using System.Collections.Generic;
using Roads.Web.Mvc.RoadsServiceClient;

namespace Roads.Web.Mvc.Models
{
    /// <summary>
    /// Model for Find route details page.
    /// </summary>
    public class FindRoadDatailsModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance has error in add feedback process.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has error add feedback; otherwise, <c>false</c>.
        /// </value>
        public bool HasErrorAddFeedback { get; set; }

        /// <summary>
        /// Gets or sets the mandatory controls identifier.
        /// </summary>
        /// <value>
        /// The mandatory controls identifier.
        /// </value>
        public int[] MandatoryControlsId { get; set; }

        /// <summary>
        /// Gets or sets the route summary.
        /// </summary>
        /// <value>
        /// The route summary.
        /// </value>
        public RDNodeSummary RouteSummary { get; set; }
        /// <summary>
        /// Gets or sets the city points names.
        /// </summary>
        /// <value>
        /// The city points names.
        /// </value>
        public List<string> CityPointsNames { get; set; }

        /// <summary>
        /// Gets or sets the rd nodes data.
        /// </summary>
        /// <value>
        /// The route detail node data.
        /// </value>
        public List<RDNodeData> RDNodesData { get; set; }
    }
}