using System.ComponentModel.DataAnnotations;

namespace Roads.DataBase.Model.Models
{
    /// <summary>
    /// Partial class of Feedback that allow using metadata.
    /// </summary>
    [MetadataType(typeof(FeedbackMetadata))]
    public partial class Feedback
    {
    }

    /// <summary>
    /// This class created to declare metadata for Feedback.
    /// </summary>
    public class FeedbackMetadata
    {
        [Key]
        public int FeedbackId { get; set; }

    }
}
