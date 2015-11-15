using System.ComponentModel.DataAnnotations;

namespace Roads.DataBase.Model.Models
{
    /// <summary>
    /// Partial class of Language that allow using metadata.
    /// </summary>
    [MetadataType(typeof(LanguageMetadata))]
    public partial class Language
    {
    }

    /// <summary>
    /// This class created to declare metadata for Language.
    /// </summary>
    public class LanguageMetadata
    {
        [Key]
        public int LanguageId { get; set; }

    }
}
