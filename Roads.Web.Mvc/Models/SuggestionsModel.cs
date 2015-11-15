using System.Collections.Generic;

namespace Roads.Web.Mvc.Models
{
    public class SuggestionsModel
    {
        //Do not rename :: it's important for autocomplete plugin name convension
        public List<Suggestion> suggestions = new List<Suggestion>();
    }
}