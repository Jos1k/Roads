using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Roads.Web.Mvc.RoadsServiceClient;

namespace Roads.Web.Mvc.Models
{
    /// <summary>
    /// The StaticTranslationViewModel class.
    /// </summary>
    public class StaticTranslationViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StaticTranslationViewModel"/> class.
        /// </summary>
        public StaticTranslationViewModel()
        {
            StaticTranslations = new List<StaticTranslationData>();
        }

        /// <summary>
        /// Gets or sets the StaticTranslations.
        /// </summary>
        /// <value>
        /// The list of <see cref="StaticTranslationData"/>.
        /// </value>
        public List<StaticTranslationData> StaticTranslations { get; set; }

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