using System.ComponentModel.DataAnnotations;

namespace Roads.DataBase.Model.Models
{
    /// <summary>
    /// Partial class of DynamicTranslations that allow using metadata.
    /// </summary>
    [MetadataType(typeof(DynamicTranslationsMetadata))]
    public partial class DynamicTranslations
    {
    }

    /// <summary>
    /// This class created to declare metadata for DynamicTranslations.
    /// </summary>
    public class DynamicTranslationsMetadata
    {
        [Key]
        public int DynamicObjectId { get; set; }

    }
}
