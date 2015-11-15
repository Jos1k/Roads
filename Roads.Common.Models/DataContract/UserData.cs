using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Roads.Common.Models.DataContract
{
    /// <summary>
    /// This class created to declare metadata for User.
    /// </summary>
    [DataContract]
    public class UserData
    {
        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string UserType { get; set; }

        [DataMember]
        public virtual ICollection<FeedbackData> Feedbacks { get; set; }
    }
}
