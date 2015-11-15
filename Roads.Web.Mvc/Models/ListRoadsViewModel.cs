using System.Collections.Generic;

namespace Roads.Web.Mvc.Models
{
    /// <summary>
    /// The List Roads View Model class.
    /// </summary>
    public class ListRoadsViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListRoadsViewModel"/> class.
        /// </summary>
        public ListRoadsViewModel()
        {
            RoadsList = new List<RoadSearchingResultViewModel>();
            PageNumber = 0;
        }

        /// <summary>
        /// Gets or sets the number of found.
        /// </summary>
        /// <value>
        /// The number of found.
        /// </value>
        public long NumberOfFound { get; set; }

        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        /// <value>
        /// The page number.
        /// </value>
        public int PageNumber { get; set; }

        /// <summary>
        /// Gets or sets the actual range.
        /// </summary>
        /// <value>
        /// The actual range.
        /// </value>
        public long ActualRange { get; set; }

        /// <summary>
        /// Gets or sets the records per page.
        /// </summary>
        /// <value>
        /// The records per page.
        /// </value>
        public int RecordsPerPage { get; set; }

        /// <summary>
        /// Gets or sets the roads list.
        /// </summary>
        /// <value>
        /// The List of <see cref="RoadSearchingResultViewModel"/>.
        /// </value>
        public List<RoadSearchingResultViewModel> RoadsList { get; set; }
    }
}