using System.Collections.Generic;
using System.Web.Mvc.Html;
using HtmlHelper = System.Web.Mvc.Html;

namespace Roads.Web.Mvc.Models
{
    /// <summary>
    /// Model for add route step one
    /// </summary>
    public class AddRoadModel
    {
        /// <summary>
        /// The city points
        /// </summary>
        public List<City> CityPoints;

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets the searching depth.
        /// </summary>
        /// <value>
        /// The searching depth.
        /// </value>
        public int SearchingDepth { get; set; }
    }
}