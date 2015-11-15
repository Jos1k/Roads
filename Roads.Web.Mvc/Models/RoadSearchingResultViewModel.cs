namespace Roads.Web.Mvc.Models
{
    public class RoadSearchingResultViewModel
    {
        public string OriginCityName { get; set; }

        public string DestinationCityName { get; set; }

        public string RouteHash { get; set; }

        public int InterimCities { get; set; }

        public string Trek { get; set; }

        public long FeedbacksCount { get; set; }
    }
}