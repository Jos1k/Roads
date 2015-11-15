using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Security;
using Newtonsoft.Json;
using Roads.Web.Mvc.Models;
using Roads.Web.Mvc.Models;
using Roads.Web.Mvc.RoadsServiceClient;
using Roads.Web.Mvc.Services;

namespace Roads.Web.Mvc.Controllers
{
    public class RoadsController : BaseController
    {
        /// <summary>
        /// The client
        /// </summary>
        private IRoadsService client;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoadsController"/> class.
        /// </summary>
        /// <param name="client">The client.</param>
        public RoadsController(IRoadsService client)
        {
            this.client = client;
        }

        [HttpGet]
        public ActionResult FindRoad()
        {
            var model = new FindRoadModel();

            return View(model);
        }

        [HttpPost]
		public ActionResult FindRoadResult( string originalCityId, string destinationCityId, bool isRouteValidation )
        {
            var model = new FindRoadModel
            {
                OriginCityNodeId = 0,
                DestinationCityNodeId = 0
            };

            int orId;

            int dest;

            if (int.TryParse(originalCityId, out orId) && int.TryParse(destinationCityId, out dest))
            {
                model = new FindRoadModel
                {
                    OriginCityNodeId = orId,
                    DestinationCityNodeId = dest
                };
            }

			model.SearchResult = RoadHelper.GetSearchResultFor( client, model, isRouteValidation );
           
            return PartialView("Partial/FindRoadSearchResult", model);
        }

        [HttpPost]
		public ActionResult RoadGrigResult( string model, bool isRouteValidation )
        {
			FindRoadModel modelStrongTyped = JsonConvert.DeserializeObject<FindRoadModel>( model);
			modelStrongTyped.SearchResult = RoadHelper.GetSearchResultFor( client, modelStrongTyped, isRouteValidation );

			return PartialView( "Partial/FindRoadSearchResult", modelStrongTyped );
        }

        [HttpGet]
        public ActionResult Datails(string id)
        {
            if (id == null)
            {
                return RedirectToAction("FindRoad", "Roads");
            }

            var model = RoadHelper.GetFindRouteDetailsModel(client, id,
                CultureHelper.GetCurrentNeutralCulture());
            if (TempData["AddFeedbackError"] != null)
            {
                model.HasErrorAddFeedback = (bool)TempData["AddFeedbackError"];
            }
            else
            {
                model.HasErrorAddFeedback = false;
            }

            TempData["mandatoryFeedbackItemId"] = model.MandatoryControlsId;
            return View(model);
        }


        [HttpPost]
        public ActionResult AddFeedback(RDLeaveFeedbackModel id, string[] controlValue, string[] feedbackItemId, string returnUrl)
        {
            if (TempData["mandatoryFeedbackItemId"] != null)
            {

                var mandatoryFeedbackItemId = (int[])TempData["mandatoryFeedbackItemId"];

                if (controlValue.Length == feedbackItemId.Length )
                {
                    List<int> mandatoryFeedbackItemIdList = new List<int>(mandatoryFeedbackItemId);
                    id.RDLeaveFeedbackValues = new List<RDLeaveFeedbackValue>();
                    for (int i = 0; i < controlValue.Length; i++)
                    {
                        if (controlValue[i].Contains(RoadHelper.JsUndefined))
                        {
                            controlValue[i] = "";
                        }

                        int controlId;
                        if ((Int32.TryParse(feedbackItemId[i], out controlId) &&
                            mandatoryFeedbackItemIdList.Exists(item => item == controlId) &&
                            String.IsNullOrEmpty(controlValue[i])) || String.IsNullOrEmpty(id.UserName))
                        {
                            TempData["AddFeedbackError"] = true;
                            return Redirect(returnUrl);
                        }
                        
                        id.RDLeaveFeedbackValues.Add(new RDLeaveFeedbackValue
                        {
                            Value = controlValue[i],
                            FeedbackItemId = feedbackItemId[i]
                        });
                    }

                    id.SubmitTime = DateTime.Now;
                    id.UserId = client.GetUserIdOrCreateNewIfNotExist(id.UserName, id.UserName, "TestUserRole"); ;

                    RoadHelper.CreateNewFeedback(client, id);
                }
            }
            return Redirect(returnUrl);
        }

        [HttpGet]
        public ActionResult AddRoad()
        {
            AddRoadModel model = RoadHelper.FillAddRoadModel( client, new AddRoadModel(), new TranslationManager(), CultureHelper.GetCurrentNeutralCulture());

            if (TempData["SubmitFail"] != null && (bool)TempData["SubmitFail"])
            {
                model.ErrorMessage = RoadsExtensionMethods.GetLabel("ARS1_Error_SubmitFail");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult AddRoad(AddRoadModel model, List<City> сityPoints)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            if (сityPoints == null)
            {
                throw new ArgumentNullException("сityPoints");
            }

            model.CityPoints = сityPoints;

            if (!RoadHelper.CheckIfAddRoadModelValid(ref model))
            {
                TempData["SubmitFail"] = true;
                return RedirectToAction("AddRoad", "Roads");
            }
            if (Session != null)
            {
                Session["сityPoints"] = сityPoints;
            }

            return RedirectToAction("AddRoadStepTwo", "Roads");
        }

        public ActionResult AddRoadStepTwo()
        {
            var сityPoints = new List<City>();
            if (Session != null && Session["сityPoints"] != null)
            {
                сityPoints.AddRange((List<City>)Session["сityPoints"]);
            }

            var citiesPairs = new List<KeyValuePair<City, City>>();
            for (int index = 0; index < сityPoints.Count - 1; index++)
            {
                citiesPairs.Add(new KeyValuePair<City, City>(сityPoints[index], сityPoints[index + 1]));
            }
            RoadHelper.TranslateCities(client,
                CultureHelper.GetCurrentNeutralCulture(),
                ref citiesPairs);
            AddRoadModelForStepTwo feedbacksAddRoadModelForStepTwo = RoadHelper.FillAddRoadModelForStepTwo(client,
                citiesPairs);

            return View(feedbacksAddRoadModelForStepTwo);
        }

        [HttpPost]
        public ActionResult AddRoadStepTwo2(AddRoadModelForStepTwo model)
        {
            string url = RoadHelper.AddNewFeedbacksOrRouteIfNotExist(client, model);
            return RedirectToAction("Datails", new { id = url });
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            var model = new LogInModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult LogIn(LogInModel logInModel)
        {
            if (ModelState.IsValid)
            {
                if (FormsAuthentication.Authenticate(logInModel.userName, logInModel.password))
                {
                    FormsAuthentication.SetAuthCookie(logInModel.userName, false);
                    return RedirectToAction("SiteSettings", "AdminZone");
                }
                else
                {
                    ViewBag.LoginFailed = true;
                }
            }
            return View(logInModel);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("FindRoad");
        }
    }
}
