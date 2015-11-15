using System.Collections.Generic;

namespace Roads.Common.Models
{
    /// <summary>
    /// The PointTree class.
    /// </summary>
    public class PointTree
    {
        /// <summary>
        /// Gets or sets the parent identifier.
        /// </summary>
        /// <value>
        /// The parent identifier.
        /// </value>
        public int ParentId { get; set; }

        /// <summary>
        /// Gets or sets the child point ids.
        /// </summary>
        /// <value>
        /// The child point ids.
        /// </value>
        public List<int> ChildPointIds { get; set; }
    }
}
