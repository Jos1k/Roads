using System.ComponentModel.DataAnnotations;

namespace Roads.DataBase.Model.Models
{
    /// <summary>
    /// Partial class of RouteNode that allow using metadata.
    /// </summary>
    [MetadataType(typeof(RouteNodeMetadata))]
    public partial class RouteNode
    {
    }

    /// <summary>
    /// This class created to declare metadata for RouteNode.
    /// </summary>
    public class RouteNodeMetadata
    {
        [Key]
        public int RouteNodeId { get; set; }

    }
}
