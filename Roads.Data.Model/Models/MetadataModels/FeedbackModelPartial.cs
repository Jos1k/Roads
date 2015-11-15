using System.ComponentModel.DataAnnotations;

namespace Roads.DataBase.Model.Models
{
    /// <summary>
    /// Partial class of FeedbackModel that allow using metadata.
    /// </summary>
    [MetadataType(typeof(FeedbackModelMetadata))]
    public partial class FeedbackModel
    {
    }

    /// <summary>
    /// This class created to declare metadata for FeedbackModel.
    /// </summary>
    public class FeedbackModelMetadata
    {
        [Key]
        public int FeedbackModelId { get; set; }

    }
}
