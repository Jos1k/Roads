using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Roads.Web.Mvc.Models
{
    /// <summary>
    /// The Translation View Model class.
    /// </summary>
    public class TranslationViewModel
    {
        /// <summary>
        /// Gets or sets the object identifier.
        /// </summary>
        /// <value>
        /// The object identifier.
        /// </value>
        public long ObjectId { get; set; }

        /// <summary>
        /// Gets or sets the languages.
        /// </summary>
        /// <value>
        /// The languages.
        /// </value>
        public SelectList Languages { get; set; }

        /// <summary>
        /// Gets or sets the language identifier.
        /// </summary>
        /// <value>
        /// The language identifier.
        /// </value>
        public int LanguageId { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [Required]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the language key.
        /// </summary>
        /// <value>
        /// The language key.
        /// </value>
        [Required]
        public string LanguageKey { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
    }
}