using System.Runtime.Serialization;

namespace Roads.Common.Models.DataContract
{
    /// <summary>
    /// This class created to declare metadata for StaticTranslation.
    /// </summary>
    [DataContract]
    public class StaticTranslationData
    {
        [DataMember]
        public string EnumKey { get; set; }

        [DataMember]
        public string Value { get; set; }

        [DataMember]
        public int LanguageId { get; set; }

        [DataMember]
        public long StaticTranslationId { get; set; }

        [DataMember]
        public virtual LanguageData Language { get; set; }
    }
}
