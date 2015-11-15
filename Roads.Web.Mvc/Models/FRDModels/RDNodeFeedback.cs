using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Roads.Web.Mvc.Models
{
    /// <summary>
    /// Model for Find route details page.
    /// </summary>
    public class RDNodeFeedback
    {
        /// <summary>
        /// Gets or sets the feedback identifier.
        /// </summary>
        /// <value>
        /// The feedback identifier.
        /// </value>
        public long FeedbackId { get; set; }

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
        /// Gets or sets the feedback values.
        /// </summary>
        /// <value>
        /// The feedback values.
        /// </value>
        public List<RDNodeFeedbackValue> FeedbackValues { get; set; }

    }
}