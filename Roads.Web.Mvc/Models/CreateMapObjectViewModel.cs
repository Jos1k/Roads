using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Roads.Web.Mvc.Models
{
    public class CreateMapObjectViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMapObjectViewModel"/> class.
        /// </summary>
        public CreateMapObjectViewModel()
        {
            Translations = new List<TranslationViewModel>();
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is region.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is region; otherwise, <c>false</c>.
        /// </value>
        public bool UseRegion { get; set; }

        /// <summary>
        /// Gets or sets the region identifier.
        /// </summary>
        /// <value>
        /// The region identifier.
        /// </value>
        public int RegionId { get; set; }

        /// <summary>
        /// Gets or sets the regions.
        /// </summary>
        /// <value>
        /// The regions.
        /// </value>
        public SelectList Regions { get; set; }

        /// <summary>
        /// Gets or sets the language key.
        /// </summary>
        /// <value>
        /// The language key.
        /// </value>
        [Required]
        public string LanguageKey { get; set; }

        /// <summary>
        /// Gets or sets the translations.
        /// </summary>
        /// <value>
        /// The translations.
        /// </value>
        public List<TranslationViewModel> Translations { get; set; } 
    }
}