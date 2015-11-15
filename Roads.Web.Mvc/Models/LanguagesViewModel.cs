using System.Collections.Generic;
using Roads.Web.Mvc.RoadsServiceClient;

namespace Roads.Web.Mvc.Models
{
    /// <summary>
    /// The Languages View Model class.
    /// </summary>
    public class LanguagesViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LanguagesViewModel"/> class.
        /// </summary>
        public LanguagesViewModel()
        {
            Languages = new List<LanguageData>();
        }

        /// <summary>
        /// Gets or sets the languages.
        /// </summary>
        /// <value>
        /// The list of <see cref="LanguageData"/>.
        /// </value>
        public List<LanguageData> Languages { get; set; }
    }
}