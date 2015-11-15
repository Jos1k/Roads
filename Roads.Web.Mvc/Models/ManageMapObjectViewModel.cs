using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Roads.Web.Mvc.Models
{
    public class ManageMapObjectViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManageMapObjectViewModel"/> class.
        /// </summary>
        public ManageMapObjectViewModel()
        {
            Translations = new List<TranslationViewModel>();
        }

        /// <summary>
        /// Gets or sets the type of the submit.
        /// </summary>
        /// <value>
        /// The type of the submit.
        /// </value>
        public string SubmitType { get; set; }

        /// <summary>
        /// Gets or sets the name of the origin city node.
        /// </summary>
        /// <value>
        /// The name of the origin city node.
        /// </value>
        [RoadsRequired("FR_Validation_RoadRequired")]
        public string MapObjectName { get; set; }

        /// <summary>
        /// Gets or sets the map object identifier.
        /// </summary>
        /// <value>
        /// The map object identifier.
        /// </value>
        public long MapObjectId { get; set; }

        /// <summary>
        /// Gets or sets the translations.
        /// </summary>
        /// <value>
        /// The translations.
        /// </value>
        public List<TranslationViewModel> Translations { get; set; } 

    }
}