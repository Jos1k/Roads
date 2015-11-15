using System;
using System.Collections.Generic;

namespace Roads.Web.Mvc.Models
{
    /// <summary>
    /// The RDLeaveFeedback class for find route details page.
    /// </summary>
    public class RDLeaveFeedback
    {
       /// <summary>
        /// Gets or sets the controls.
        /// </summary>
        /// <value>
        /// The controls.
        /// </value>
        public List<FeedbackControl> Controls { get; set; }

        /// <summary>
        /// Gets or sets the post model.
        /// </summary>
        /// <value>
        /// The post model.
        /// </value>
        public RDLeaveFeedbackModel PostModel { get; set; }
    }
}