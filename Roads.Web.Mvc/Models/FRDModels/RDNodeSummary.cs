using System.Collections.Generic;

namespace Roads.Web.Mvc.Models
{
    /// <summary>
    /// Model for Find route details page.
    /// </summary>
    public class RDNodeSummary
    {
        /// <summary>
        /// Gets or sets origin city name.
        /// </summary>
        /// <value>
        /// From.
        /// </value>
        public string From { get; set; }

        /// <summary>
        /// Gets or sets destination city name.
        /// </summary>
        /// <value>
        /// To.
        /// </value>
        public string To { get; set; }

        /// <summary>
        /// Gets or sets the controls.
        /// </summary>
        /// <value>
        /// The controls.
        /// </value>
        public List<RDSummaryControl> Controls { get; set; }
    }
}