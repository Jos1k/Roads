using System.ComponentModel.DataAnnotations;

namespace Roads.DataBase.Model.Models
{
    /// <summary>
    /// Partial class of CityNode that allow using metadata.
    /// </summary>
    [MetadataType(typeof(CityNodeMetadata))]
    public partial class CityNode
    {
    }

    /// <summary>
    /// This class created to declare metadata for CityNode.
    /// </summary>
    public class CityNodeMetadata
    {
        [Key]
        public int CityNodeId { get; set; }

    }
}
