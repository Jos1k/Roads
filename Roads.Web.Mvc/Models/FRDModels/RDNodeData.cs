using System.Collections.Generic;

namespace Roads.Web.Mvc.Models
{
    /// <summary>
    /// Model for Find route details page.
    /// </summary>
    public class RDNodeData
    {
        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        /// <value>
        /// The summary.
        /// </value>
        public RDNodeSummary Summary { get; set; }

        /// <summary>
        /// Gets or sets the leave feedback window.
        /// </summary>
        /// <value>
        /// The leave feedback window.
        /// </value>
        public RDLeaveFeedback LeaveFeedbackWindow { get; set; }

        /// <summary>
        /// Gets or sets the feedbacks.
        /// </summary>
        /// <value>
        /// The feedbacks.
        /// </value>
        public List<RDNodeFeedback> Feedbacks { get; set; }
    }
}