using System.Runtime.Serialization;

namespace Roads.Common.Models.DataContract
{
    /// <summary>
    /// This class created to declare DataContract for using Setting type in WCF data contract.
    /// </summary>
    [DataContract]
    public class SettingData
    {
        [DataMember]
        public int SettingId { get; set; }

        [DataMember]
        public string SettingName { get; set; }

        [DataMember]
        public string SettingValue { get; set; }
    }
}
