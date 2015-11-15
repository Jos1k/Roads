using System.ComponentModel.DataAnnotations;

namespace Roads.DataBase.Model.Models
{
    /// <summary>
    /// Partial class of User that allow using metadata.
    /// </summary>
    [MetadataType(typeof(UserMetadata))]
    public partial class User
    {
    }

    /// <summary>
    /// This class created to declare metadata for User.
    /// </summary>
    public class UserMetadata
    {
        [Key]
        public int UserId { get; set; }

    }
}
