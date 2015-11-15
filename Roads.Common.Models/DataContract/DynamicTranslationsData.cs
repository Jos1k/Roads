using System.Runtime.Serialization;

namespace Roads.Common.Models.DataContract
{
    /// <summary>
    /// This class created to declare metadata for DynamicTranslations.
    /// </summary>
    [DataContract]
    public class DynamicTranslationsData
    {
        [DataMember]
        public int DynamicObjectId { get; set; }
        
        [DataMember]
        public string Value { get; set; }

        [DataMember]
        public string DescriptionValue { get; set; }

        [DataMember]
        public int LanguageId { get; set; }

        [DataMember]
        public string DynamicKey { get; set; }

        [DataMember]
        public LanguageData Lenguage { get; set; }
    }
}
