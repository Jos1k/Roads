using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Roads.Web.Mvc.Models
{
    public class FeedbackControl 
    {
        /// <summary>
        /// Gets or sets the feed back item identifier.
        /// </summary>
        /// <value>
        /// The feed back item identifier.
        /// </value>
        public int FeedBackItemId { get; set; }
        /// <summary>
        /// Gets or sets the CSS code.
        /// </summary>
        /// <value>
        /// The CSS code.
        /// </value>
        public string JSCode { get; set; }
        /// <summary>
        /// Gets or sets the HTML code.
        /// </summary>
        /// <value>
        /// The HTML code.
        /// </value>
        public string HTMLCode { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is mandatory.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is mandatory; otherwise, <c>false</c>.
        /// </value>
        public bool IsMandatory { get; set; }
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
    }
}