using System.ComponentModel.DataAnnotations;

namespace Roads.DataBase.Model.Models
{
    /// <summary>
    /// Partial class of MapObjectTranslation that allow using metadata.
    /// </summary>
    [MetadataType(typeof(MapObjectTranslationMetadata))]
    public partial class MapObjectTranslation
    {
    }

    /// <summary>
    /// This class created to declare metadata for MapObjectTranslation.
    /// </summary>
    public class MapObjectTranslationMetadata
    {
        [Key]
        public int MapObjectId { get; set; }

    }
}
