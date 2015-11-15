using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Roads.Web.Mvc.RoadsServiceClient;

namespace Roads.Web.Mvc.Models
{
    /// <summary>
    /// The StaticTranslationViewModel class.
    /// </summary>
    public class DynamicTranslationViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicTranslationViewModel"/> class.
        /// </summary>
        public DynamicTranslationViewModel()
        {
            DynamicTranslations = new List<DynamicTranslationsData>();
        }

        /// <summary>
        /// Gets or sets the StaticTranslations.
        /// </summary>
        /// <value>
        /// The list of <see cref="StaticTranslationData"/>.
        /// </value>
        public List<DynamicTranslationsData> DynamicTranslations { get; set; }

        /// <summary>
        /// Gets or sets the TranslationKey.
        /// </summary>
        /// <value>
        /// The TranslationKey.
        /// </value>
        [RoadsRequired("FR_Validation_RoadRequired")]
        public string TranslationKey { get; set; }

        /// <summary>
        /// Gets or sets the type of the submit.
        /// </summary>
        /// <value>
        /// The type of the submit.
        /// </value>
        public string SubmitType { get; set; }
    }
}