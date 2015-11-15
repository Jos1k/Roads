using System.Runtime.Serialization;

namespace Roads.Common.Models.DataContract
{
    [DataContract]
    public class ObjectTranslationsData
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}
