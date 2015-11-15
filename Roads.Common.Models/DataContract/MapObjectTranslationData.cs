using System.Runtime.Serialization;

namespace Roads.Common.Models.DataContract
{
    /// <summary>
    /// This class created to declare metadata for MapObjectTranslation.
    /// </summary>
    [DataContract]
    public class MapObjectTranslationData
    {
        [DataMember]
        public long MapObjectId { get; set; }

        [DataMember]
        public string Value { get; set; }

        [DataMember]
        public int LanguageId { get; set; }

        [DataMember]
        public string LanguageKey { get; set; }

        [DataMember]
        public int RegionNodeId { get; set; }

        [DataMember]
        public int CityNodeId { get; set; }

        [DataMember]
        public virtual LanguageData Language { get; set; }
    }
}
