using System.ComponentModel.DataAnnotations;

namespace Roads.DataBase.Model.Models
{
    /// <summary>
    /// Partial class of RegionNode that allow using metadata.
    /// </summary>
    [MetadataType(typeof(RegionNodeMetadata))]
    public partial class RegionNode
    {
    }

    /// <summary>
    /// This class created to declare metadata for RegionNode.
    /// </summary>
    public class RegionNodeMetadata
    {
        [Key]
        public int RegionNodeId { get; set; }

    }
}
