namespace Roads.Web.Mvc.Models
{
    /// <summary>
    /// Model for Find route details page.
    /// </summary>
    public class RDNodeFeedbackValue
    {
        /// <summary>
        /// Gets or sets the name translation key.
        /// </summary>
        /// <value>
        /// The name translation key.
        /// </value>
        public string NameTranslationKey { get; set; }

        /// <summary>
        /// Gets or sets the description translation key.
        /// </summary>
        /// <value>
        /// The description translation key.
        /// </value>
        public string DescriptionTranslationKey { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="RDNodeFeedbackValue"/> is mandatory.
        /// </summary>
        /// <value>
        ///   <c>true</c> if mandatory; otherwise, <c>false</c>.
        /// </value>
        public bool Mandatory { get; set; }

        /// <summary>
        /// Gets or sets the sort number.
        /// </summary>
        /// <value>
        /// The sort number.
        /// </value>
        public int SortNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is numeric.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is numeric; otherwise, <c>false</c>.
        /// </value>
        public bool IsNumeric { get; set; }
    }
}