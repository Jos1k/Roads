using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Roads.Web.Mvc.Models
{
    /// <summary>
    /// Model for Find route details page.
    /// </summary>
    public class RDLeaveFeedbackModel
    {
        /// <summary>
        /// Gets or sets the name of the origin city.
        /// </summary>
        /// <value>
        /// The name of the origin city.
        /// </value>
        public int OriginCityId { get; set; }
        /// <summary>
        /// Gets or sets the name of the destination city.
        /// </summary>
        /// <value>
        /// The name of the destination city.
        /// </value>
        public int DestinationCityId { get; set; }
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public int UserId { get; set; }
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets the submit time.
        /// </summary>
        /// <value>
        /// The submit time.
        /// </value>
        public DateTime SubmitTime { get; set; }
        /// <summary>
        /// Gets or sets the rd leave feedback values.
        /// </summary>
        /// <value>
        /// The rd leave feedback values.
        /// </value>
        public List<RDLeaveFeedbackValue> RDLeaveFeedbackValues { get; set; }
    }
}