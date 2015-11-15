using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Roads.Web.Mvc.Integrations;
using Roads.Web.Mvc.Models;
using Roads.Web.Mvc.Models.Enums;
using Roads.Web.Mvc.RoadsServiceClient;
using Roads.Web.Mvc.Services;
using Roads.Web.Mvc.Services.ActionResults;
using Roads.Web.Mvc.Services.ExportStringBuilders;
using Roads.Web.Mvc.Services.ImportFileProviders;

namespace Roads.Web.Mvc.Controllers
{
    [Authorize]
    public class AdminZoneController : BaseController
    {
        /// <summary>
        /// The client
        /// </summary>
        private IRoadsService client;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminZoneController"/> class.
        /// </summary>
        /// <param name="client">The client.</param>
        public AdminZoneController(IRoadsService client)
        {
            this.client = client;
        }

        //
        // GET: /AdminZone/
        /// <summary>
        /// Sites the settings.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SiteSettings()
        {
            var model = new SiteSettingsModel
            {
                numberOfRecordsPerPage = RoadHelper.GetSettings(client).numberOfRecordsPerPage,
                searchDepth = RoadHelper.GetSettings(client).searchDepth

            };

            return View(model);
        }

        /// <summary>
        /// Sites the settings.
        /// </summary>
        /// <param name="siteSettingsModel">The site settings model.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SiteSettings(SiteSettingsModel siteSettingsModel)
        {
            if (ModelState.IsValid)
            {
                RoadHelper.SetSettings(client, siteSettingsModel);
            }

            return View(siteSettingsModel);
        }

        /// <summary>
        /// Feedbacks the settings.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult FeedbackSettings()
        {
            var feedbacksItemsListForView = RoadHelper.FillFeedbackItemModel(client);
            return View(feedbacksItemsListForView);
        }

        /// <summary>
        /// Feedbacks the settings.
        /// </summary>
        /// <param name="itemForSave">The item for save.</param>
        /// <param name="dynamicTranslationsList">The dynamic translations list.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult FeedbackSettings(FeedbackItemSettings itemForSave, List<DynamicTranslation> dynamicTranslationsList)
        {
            var feedbacksItemsListFromRoadHelper = RoadHelper.GetFeedbackItems(client).feedbackItemSettings;
                foreach (var feedbackItem in feedbacksItemsListFromRoadHelper)
                {
                    if (feedbackItem.feedbackItemId == itemForSave.feedbackItemId)
                    {
                        feedbackItem.settingName = itemForSave.settingName;
                        feedbackItem.sortNumber = itemForSave.sortNumber;
                        feedbackItem.description = itemForSave.description;
                        feedbackItem.isNumeric = itemForSave.isNumeric;
                        feedbackItem.isMandatory = itemForSave.isMandatory;
                        feedbackItem.feedbackModelId = itemForSave.feedbackModelId;
                    }
                }

                List<DynamicTranslation> listForUpdate = new List<DynamicTranslation>();
                foreach (var translationInModel in RoadHelper.FillFeedbackItemModel(client).dynamicTranslations)
                {
                    foreach (var dynamicNameTranslation in dynamicTranslationsList)
                    {
                        if (translationInModel.DynamicKey == dynamicNameTranslation.DynamicKey &&
                            translationInModel.LanguageId == dynamicNameTranslation.LanguageId)
                        {
                            translationInModel.Value = dynamicNameTranslation.Value;
                            listForUpdate.Add(translationInModel);
                        }
                    }
                }

                RoadHelper.UpdateDynamicTranslations(client, listForUpdate);
                RoadHelper.SetFeedbackItems(client, feedbacksItemsListFromRoadHelper);

            return new JsonResult() { Data = true, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        /// <summary>
        /// Deletes the feedback.
        /// </summary>
        /// <param name="itemForDelete">The item for delete.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteFeedback(FeedbackItemSettings itemForDelete)
        {
            List<string> dynamicKeysForDelete = new List<string>();
            dynamicKeysForDelete.Add(itemForDelete.settingName);
            dynamicKeysForDelete.Add(itemForDelete.description);

            RoadHelper.DeleteFeedbackItem(client, itemForDelete);
            RoadHelper.DeleteDynamicTranslations(client, dynamicKeysForDelete);
            return new JsonResult();
        }

        /// <summary>
        /// Adds the feedback.
        /// </summary>
        /// <param name="newFeedbackItemSettings">The new feedback item settings.</param>
        /// <param name="newDynamicTranslations">The new dynamic translations.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddFeedback(FeedbackItemSettings newFeedbackItemSettings, List<DynamicTranslation> newDynamicTranslations)
        {
                var languages = RoadHelper.GetAllAvailableLanguage(client);
                foreach (var newDynamicTraslation in newDynamicTranslations)
                {
                    newDynamicTraslation.DescriptionValue = String.Format("{0} translation for {1} dynamic record.",
                        languages.FirstOrDefault(x => x.LanguageId == newDynamicTraslation.LanguageId).Name.ToUpper(),
                        newDynamicTraslation.Value);
                }

                RoadHelper.AddNewFeedbackItem(client, newFeedbackItemSettings);
                RoadHelper.AddDynamicTranslations(client, newDynamicTranslations);
            return new JsonResult();
        }

        /// <summary>
        /// The Languages Controller Action Method.
        /// </summary>
        /// <returns> The <see cref="ActionResult"/>.</returns>
        [HttpGet]
        public ActionResult Languages()
        {
            var model = new LanguagesViewModel
            {
                Languages = RoadHelper.GetLanguages(client)
            };

            return View(model);
        }

        /// <summary>
        /// Edits the translations.
        /// </summary>
        /// <returns>The partial view.</returns>
        [HttpGet]
        public ActionResult EditStaticTranslations()
        {
            return View(new StaticTranslationViewModel());
        }

        [HttpPost]
        public ActionResult EditStaticTranslations(StaticTranslationViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.SubmitType == "Submit")
                {
                    RoadHelper.UpdateStaticTranslations(client, model.StaticTranslations);

                    model.StaticTranslations = new List<StaticTranslationData>();
                }
                else if (model.SubmitType == "Search")
                {
                    model.StaticTranslations = RoadHelper.GetStaticTranslationFor(client, model.TranslationKey);
                }
            }

            return View(model);
        }

        /// <summary>
        /// Edits the translations.
        /// </summary>
        /// <returns>The partial view.</returns>
        [HttpGet]
        public ActionResult EditDynamicTranslations()
        {
            return View(new DynamicTranslationViewModel());
        }

        /// <summary>
        /// Edits the dynamic translations.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditDynamicTranslations(DynamicTranslationViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.SubmitType == "Submit")
                {
                    RoadHelper.UpdateDynamicTranslations(client, model.DynamicTranslations);

                    model.DynamicTranslations = new List<DynamicTranslationsData>();
                }
                else if (model.SubmitType == "Search")
                {
                    model.DynamicTranslations = RoadHelper.GetDynamicTranslationFor(client, model.TranslationKey);
                }
            }

            return View(model);
        }

        /// <summary>
        /// Creates the dynamic translation.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateDynamicTranslation()
        {
            var model = new CreateTranslationViewModel();

            RoadHelper.FillCreateTranslationViewModel(client, model);

            return View(model);
        }

        /// <summary>
        /// Creates the dynamic translation.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>The ActionResult.</returns>
        [HttpPost]
        public ActionResult CreateDynamicTranslation(CreateTranslationViewModel model)
        {
            return View(model);
        }

        /// <summary>
        /// Manages the map object.
        /// </summary>
        /// <returns>The ActionResult.</returns>
        [HttpGet]
        public ActionResult EditMapObjectTranslations()
        {
            return View(new ManageMapObjectViewModel());
        }

        /// <summary>
        /// Manages the map object.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>The ActionResult.</returns>
        [HttpPost]
        public ActionResult EditMapObjectTranslations(ManageMapObjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.SubmitType == "Search")
                {
                    model.Translations = RoadHelper.GetTranslationDatasFor(client, model.MapObjectId);

                    return View(model);
                }

                if (model.SubmitType == "Submit")
                {
                    RoadHelper.UpdateMapObjectsTranslations(client, model.Translations);

                    return RedirectToAction("EditMapObjectTranslations", "AdminZone");
                }
            }

            model.Translations = new List<TranslationViewModel>();

            return View(model);
        }

        /// <summary>
        /// Creates the map object.
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateMapObject()
        {
            var model = new CreateMapObjectViewModel();

            RoadHelper.FillCreateMapObjectModel(client, model);

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateMapObject(CreateMapObjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                RoadHelper.SaveMapObject(model);

                return RedirectToAction("CreateMapObject", "AdminZone");
            }

            RoadHelper.FillCreateMapObjectModel(client, model);

            return View(model);
        }

        #region Import/Export

        public ActionResult ExportXml(ExportDataType dataType)
        {
            var fileName = String.Format(dataType + "Translations");
            var exportProvider = new ExportDataProvider(new XmlStringBuilder());
            var report = exportProvider.CreateExportData(client, dataType);
            return new XmlActionResult(report, fileName);
        }

        public ActionResult ExportCsv(ExportDataType dataType)
        {
            var fileName = String.Format(dataType + "Translations");
            var exportProvider = new ExportDataProvider(new CsvStringBuilder());
            var report = exportProvider.CreateExportData(client, dataType);
            return new CsvActionResult(report, fileName);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase file, ExportDataType dataType)
        {
            IImportFileProvider provider = ImportProviderResolver.ResolveImportProvider(file);

            provider.Update(client, file, dataType);
            return new JsonResult {Data = provider.Report};
        }

        #endregion
    }
}
