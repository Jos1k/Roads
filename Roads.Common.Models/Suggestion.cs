using System.Runtime.Serialization;

namespace Roads.Common.Models
{
    [DataContract]
    public class Suggestion
    {
        private string _suggestionCityName;
        private string _suggestionRegionName;
        private long _cityNodeId;

        public Suggestion(string suggestionCityName,string suggestionRegionName, long cityNodeId)
        {
            _suggestionCityName = suggestionCityName;
            _suggestionRegionName = suggestionRegionName;
            _cityNodeId = cityNodeId;
        }

        [DataMember]
        public string SuggestionCityName
        {
            get { return _suggestionCityName; }
            set { _suggestionCityName = value; }
        }

        [DataMember]
        public string SuggestionRegionName
        {
            get { return _suggestionRegionName; }
            set { _suggestionRegionName = value; }
        }

        [DataMember]
        public long CityNodeId
        {
            get { return _cityNodeId; }
            set { _cityNodeId = value; }
        }
    }
}
