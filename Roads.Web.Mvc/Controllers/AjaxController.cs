using System.Threading;
using System.Web.Http;
using System.Web.Mvc;
using Roads.Web.Mvc.Models;
using Roads.Web.Mvc.RoadsServiceClient;
using Roads.Web.Mvc.Services;

namespace Roads.Web.Mvc.Controllers
{
    public class AjaxController : ApiController
    {
        /// <summary>
        /// The client
        /// </summary>
        private IRoadsService client;

        /// <summary>
        /// Initializes a new instance of the <see cref="AjaxController"/> class.
        /// </summary>
        public AjaxController()
        {
            this.client = new RoadsServiceClient.RoadsServiceClient();
        }

        public SuggestionsModel GetSuggestion(string searchString)
        {
                string lang = CultureHelper.GetCulture(Thread.CurrentThread.CurrentUICulture.Name);
                return RoadHelper.GetSuggestions(client, searchString, lang);
        }
    }
}
