using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Roads.DataBase.Model.Models
{
    /// <summary>
    /// Partial class of FeedbackItem that allow using metadata.
    /// </summary>
    [MetadataType(typeof (FeedbackItemMetadata))]
    public partial class FeedbackItem
    {
    }

    /// <summary>
    /// This class created to declare DataContract for using FeedbackItem type in WCF data contract.
    /// </summary>
    [DataContract]
    public class FeedbackItemMetadata
    {
        [Key]
        public int FeedbackItemId { get; set; }
    }
}
