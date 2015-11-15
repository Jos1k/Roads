using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Roads.Web.Mvc.Models
{
    /// <summary>
    /// Model for Find route details page.
    /// </summary>
    public class RDSummaryControl
    {
        /// <summary>
        /// Gets or sets the feedback model identifier.
        /// </summary>
        /// <value>
        /// The feedback model identifier.
        /// </value>
        public Int64 FeedbackModelId { get; set; }
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public List<long> Value { get; set; }
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
        /// Gets or sets the sort number.
        /// </summary>
        /// <value>
        /// The sort number.
        /// </value>
        public int SortNumber { get; set; }

        /// <summary>
        /// Gets the summury value.
        /// </summary>
        /// <returns>Return Average of Values</returns>
        public long GetSummuryValue()
        {
            var value = (long)Value.Average(m => m);
            return value;
        }
    }
}