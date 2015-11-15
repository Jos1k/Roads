using System.ComponentModel.DataAnnotations;

namespace Roads.DataBase.Model.Models
{
    /// <summary>
    /// Partial class of StaticTranslation that allow using metadata.
    /// </summary>
    [MetadataType(typeof(StaticTranslationMetadata))]
    public partial class StaticTranslation
    {
    }

    /// <summary>
    /// This class created to declare metadata for StaticTranslation.
    /// </summary>
    public class StaticTranslationMetadata
    {
        [Key]
        public int StaticTranslationId { get; set; }

    }
}
