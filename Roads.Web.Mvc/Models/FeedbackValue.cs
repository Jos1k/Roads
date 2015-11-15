namespace Roads.Web.Mvc.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class FeedbackValue
    {
        /// <summary>
        /// Gets or sets the feedback identifier.
        /// </summary>
        /// <value>
        /// The feedback identifier.
        /// </value>
        public int? FeedbackId { get; set; }
        /// <summary>
        /// Gets or sets the feedback item identifier.
        /// </summary>
        /// <value>
        /// The feedback item identifier.
        /// </value>
        public int? FeedbackItemId { get; set; }
        /// <summary>
        /// Gets or sets the feedback value identifier.
        /// </summary>
        /// <value>
        /// The feedback value identifier.
        /// </value>
        public int? FeedbackValueId { get; set; }
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        private string feedbackValue;
        public string Value 
        {
            get 
            { 
                if (feedbackValue == null)
                {
                        feedbackValue = string.Empty;
                }
                return feedbackValue;
            }
            set { feedbackValue = value; }
        }
    }
}