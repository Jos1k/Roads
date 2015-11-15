using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Roads.DataBase.Model.Models
{
    /// <summary>
    /// Partial class of FeedbackValue that allow using metadata.
    /// </summary>
    [MetadataType(typeof (FeedbackValueMetadata))]
    public partial class FeedbackValue
    {
    }

    /// <summary>
    /// This class created to declare DataContract for using FeedbackValue type in WCF data contract.
    /// </summary>
    [DataContract]
    public class FeedbackValueMetadata
    {
        [Key]
        public int FeedbackValueId { get; set; }
    }
}

