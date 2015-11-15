using System.ComponentModel.DataAnnotations;

namespace Roads.DataBase.Model.Models
{
    /// <summary>
    /// Partial class of Trek that allow using metadata.
    /// </summary>
    [MetadataType(typeof(TrekMetadata))]
    public partial class Trek
    {
    }

    /// <summary>
    /// This class created to declare metadata for Trek.
    /// </summary>
    public class TrekMetadata
    {
        [Key]
        public int TrekId { get; set; }

    }
}
