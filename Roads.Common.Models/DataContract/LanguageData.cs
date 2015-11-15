using System.Runtime.Serialization;

namespace Roads.Common.Models.DataContract
{
    /// <summary>
    /// This class created to declare metadata for Language.
    /// </summary>
    [DataContract]
    public class LanguageData
    {
        [DataMember]
        public int LanguageId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public bool IsDefault { get; set; }
    }
}
