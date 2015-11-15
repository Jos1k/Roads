using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using Roads.Web.Mvc.Models;
using Roads.Web.Mvc.RoadsServiceClient;
using WebGrease.Css.Extensions;
using DynamicTranslationsData = Roads.Web.Mvc.RoadsServiceClient.DynamicTranslationsData;
using FeedbackItemData = Roads.Web.Mvc.RoadsServiceClient.FeedbackItemData;
using FeedbackValueData = Roads.Web.Mvc.RoadsServiceClient.FeedbackValueData;
using HolisticFeedback = Roads.Web.Mvc.RoadsServiceClient.HolisticFeedback;
using LanguageData = Roads.Web.Mvc.RoadsServiceClient.LanguageData;
using RouteDetailsData = Roads.Web.Mvc.RoadsServiceClient.RouteDetailsData;
using RouteDetailsFeedback = Roads.Web.Mvc.RoadsServiceClient.RouteDetailsFeedback;
using RouteNodeWithFeedbacksData = Roads.Web.Mvc.RoadsServiceClient.RouteNodeWithFeedbacksData;
using SettingData = Roads.Web.Mvc.RoadsServiceClient.SettingData;
using StaticTranslationData = Roads.Web.Mvc.RoadsServiceClient.StaticTranslationData;
using Suggestion = Roads.Web.Mvc.RoadsServiceClient.Suggestion;

namespace Roads.Web.Mvc.Services
{
    public class RoadHelper
    {
        #region Private fields and constants

        private const string numberOfRecordPerPage = "NumberOfRecordPerPage";
        private const string searchDepth = "SearchDepth";

        #endregion

        #region Public fields and constants

        public const string JsUndefined = "undefined";

        #endregion

        #region AddRoad Step One Region

       /// <summary>
        /// Check if AddRoadModel is valid.
        /// </summary>
        /// <param name="model">The model AddRoadModel.</param>
        /// <returns>If model is valid return true.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static bool CheckIfAddRoadModelValid(ref AddRoadModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            if (model.CityPoints == null)
            {
                throw new ArgumentNullException("model.CityPoints");
            }

            if (!model.CityPoints.Exists(m => m.CityId == null || m.CityName == null))
            {
                return true;
            }

            return false;
        }

        #endregion

        #region SmartSuggestion Region

        /// <summary>
        /// Gets the suggestions by search string.
        /// </summary>
        /// <param name="client">IRoadsService instance. </param>
        /// <param name="searchString">The search string.</param>
        /// <param name="language">The language of user.</param>
        /// <returns><see cref="SuggestionsModel"/> which contains search results.</returns>
        public static SuggestionsModel GetSuggestions(IRoadsService client, string searchString, string language)
        {
            var suggestionsModel = new SuggestionsModel();
            Suggestion[] suggestions = client.GetSuggestions(searchString, language);

            if (suggestions != null && suggestions.Length != 0)
            {
                foreach (Suggestion suggestion in suggestions)
                {
                    suggestionsModel.suggestions.Add(new Models.Suggestion
                    {
                        data = suggestion.CityNodeId.ToString(CultureInfo.InvariantCulture),
                        value = String.Format("{0}{1}", suggestion.SuggestionCityName, suggestion.SuggestionRegionName)
                    });
                }
            }
            return suggestionsModel;
        }

        #endregion

        #region Fill Models Region

        /// <summary>
        /// Fills the add road model for step two.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="cities">The cities.</param>
        /// <returns>
        /// The <see cref="AddRoadModelForStepTwo"/> for Add Road Step 2.
        /// </returns>
        public static AddRoadModelForStepTwo FillAddRoadModelForStepTwo(IRoadsService client, List<KeyValuePair<City, City>> cities)
        {
            var addRoadModelForStepTwo = new AddRoadModelForStepTwo();

            client.GetFeedbackControlsListForNewFeedback()
                .ForEach(x => addRoadModelForStepTwo.FeedbackControls.Add(new FeedbackControl()
                {
                    JSCode = x.FeedbackModel.JavascriptCode,
                    HTMLCode = x.FeedbackModel.HtmlCode,
                    SortNumber = x.FeedbackItem.SortNumber,
                    IsMandatory = x.FeedbackItem.Mandatory,
                    DescriptionTranslationKey = x.FeedbackItem.DescriptionTranslationKey,
                    NameTranslationKey = x.FeedbackItem.NameTranslationKey,
                    FeedBackItemId = x.FeedbackItem.FeedbackItemId
                }));

            addRoadModelForStepTwo.FeedbackControls = addRoadModelForStepTwo
                .FeedbackControls
                .OrderBy(x => x.SortNumber).ToList();

            byte fakekId = 0;
            cities.ForEach(x =>
            {
                addRoadModelForStepTwo.Feedbacks.Add(new FeedbackToRouteNode()
            {
                OriginCityName = x.Key.CityName,
                OriginCityNodeId = x.Key.CityId,
                DestinationCityName = x.Value.CityName,
                DestinationCityNodeId = x.Value.CityId,
                FeedbackId = fakekId,
                FeedbackValues = new List<FeedbackValue>()
            });
                fakekId++;
            });

            fakekId = 0;
            addRoadModelForStepTwo.FeedbackControls.
                ForEach(
                    x => addRoadModelForStepTwo.Feedbacks.ForEach(
                        y =>
                        {
                            y.FeedbackValues.Add(new FeedbackValue()
                            {
                                FeedbackItemId = x.FeedBackItemId,
                                FeedbackId = y.FeedbackId,
                                FeedbackValueId = fakekId,
                                Value = string.Empty
                            });
                            fakekId++;
                        }));
            return addRoadModelForStepTwo;
        }

        /// <summary>
        /// Fills the AddRoadModel.
        /// </summary>
        /// <param name="manager">The ITranslationManager manager.</param>
        /// <param name="userLanguages">The user languages string.</param>
        /// <returns>The <see cref="AddRoadModel"/> with default placeholders.</returns>
        public static AddRoadModel FillAddRoadModel(IRoadsService client, AddRoadModel model, ITranslationManager manager, string userLanguages)
        {
            int searchingDepth = client.GetSearchingDepth();
            model.SearchingDepth = searchingDepth;
            model.CityPoints = new List<City>
            {
                new City
                {
                    Placeholder =
                        manager.GetLabelTranslation("ARS1_Placeholder_From",
                            CultureHelper.GetCulture(userLanguages))
                },
                new City
                {
                    Placeholder =
                        manager.GetLabelTranslation("ARS1_Placeholder_To",
                            CultureHelper.GetCulture(userLanguages))
                }
            };
            return model;
        }

        /// <summary>
        /// Fills the create translation view model.
        /// </summary>
        /// <param name="model">The model.</param>
        public static void FillCreateTranslationViewModel(IRoadsService client, CreateTranslationViewModel model)
        {
            var leng = GetLanguages(client);

            var lenguages = new SelectList(leng.Select(s =>
                new SelectListItem
                {
                    Value = s.LanguageId.ToString(CultureInfo.InvariantCulture),
                    Text = s.Name,
                    Selected = s.IsDefault
                }), "Value", "Text", leng.First(e => e.IsDefault).LanguageId);

            foreach (var l in lenguages)
            {
                model.Translations.Add(new TranslationViewModel { Languages = lenguages });
            }
        }

        /// <summary>
        /// Fills the feedback item model.
        /// </summary>
        /// <returns>The <see cref="FeedbackItemModel"/>.</returns>
        public static FeedbackItemModel FillFeedbackItemModel(IRoadsService client)
        {
            var feedbacksItemsListForView = RoadHelper.GetFeedbackItems(client);
            feedbacksItemsListForView.modelsNames = GetFeedbackModels(client);
            feedbacksItemsListForView.availableLanguages = GetAllAvailableLanguage(client);
            feedbacksItemsListForView.dynamicTranslations = GetAllDynamicTranslations(client);
            return feedbacksItemsListForView;
        }

        #endregion

        #region Settings

        /// <summary>
        /// Gets the settings from WCF Service.
        /// </summary>
        /// <returns>
        /// The <see cref="SiteSettingsModel"/>.
        /// </returns>
        public static SiteSettingsModel GetSettings(IRoadsService client)
        {
            var siteSettings = new SiteSettingsModel();
            List<SettingData> settings = client.GetSettings().ToList();

            foreach (SettingData setting in settings)
            {
                if (String.CompareOrdinal(setting.SettingName, numberOfRecordPerPage) == 0)
                {
                    siteSettings.numberOfRecordsPerPage = Convert.ToInt32(setting.SettingValue);
                }
                else if (String.CompareOrdinal(setting.SettingName, searchDepth) == 0)
                {
                    siteSettings.searchDepth = Convert.ToInt32(setting.SettingValue);
                }
            }
            return siteSettings;
        }

        /// <summary>
        /// Sets the settings.
        /// </summary>
        /// <param name="siteSettings">The site settings.</param>
        public static void SetSettings(IRoadsService client, SiteSettingsModel siteSettings)
        {
            List<SettingData> settings = client.GetSettings().ToList();

            foreach (SettingData setting in settings)
            {
                if (String.Compare(setting.SettingName, numberOfRecordPerPage, false) == 0)
                {
                    setting.SettingValue = siteSettings.numberOfRecordsPerPage.ToString();
                }
                else if (String.Compare(setting.SettingName, searchDepth, false) == 0)
                {
                    setting.SettingValue = siteSettings.searchDepth.ToString();
                }
            }
            client.SetSettings(settings.ToArray());
        }

        /// <summary>
        /// Gets the languages.
        /// </summary>
        /// <returns>The list for <see cref="Mvc.RoadsServiceClient.LanguageData"/>.</returns>
        public static List<LanguageData> GetLanguages(IRoadsService client)
        {
            return client.GetAllLanguages().ToList();
        }

        /// <summary>
        /// Gets the static translation for.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The List of <see cref="Mvc.RoadsServiceClient.StaticTranslationData"/>.</returns>
        public static List<StaticTranslationData> GetStaticTranslationFor(IRoadsService client, string key)
        {
                var languages = client.GetAllLanguages();

                return client.GetStaticTranslationData(key).Select(s => new StaticTranslationData
                    {
                        EnumKey = s.EnumKey,
                        LanguageId = s.LanguageId,
                        Value = s.Value,
                        StaticTranslationId = s.StaticTranslationId,
                        Language = languages.FirstOrDefault(l => l.LanguageId == s.LanguageId)
                    })
                    .ToList();
        }

        /// <summary>
        /// Dynamics the translation for.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The List of <see cref="Mvc.RoadsServiceClient.DynamicTranslationsData"/>.</returns>
        public static List<DynamicTranslationsData> GetDynamicTranslationFor(IRoadsService client, string key)
        {
                var languages = client.GetAllLanguages();

                return client.GetDynamicTranslationData(key).Select(s => new DynamicTranslationsData
                    {
                        DynamicKey = s.DynamicKey,
                        LanguageId = s.LanguageId,
                        Value = s.Value,
                        DynamicObjectId = s.DynamicObjectId,
                        Lenguage = languages.FirstOrDefault(l => l.LanguageId == s.LanguageId)
                    }).ToList();
        }

        /// <summary>
        /// Updates the static translations.
        /// </summary>
        /// <param name="translations">The List of <see cref="StaticTranslationData"/>.</param>
        public static void UpdateStaticTranslations(IRoadsService client, List<StaticTranslationData> translations)
        {
            client.UpdateStaticTranslation(translations.ToArray());
        }

        /// <summary>
        /// Updates the dynamic translations.
        /// </summary>
        /// <param name="translations">The List of <see cref="DynamicTranslationsData"/>.</param>
        public static void UpdateDynamicTranslations(IRoadsService client, List<DynamicTranslationsData> translations)
        {
            client.UpdateDynamicTranslations(translations.ToArray());
        }

        #endregion

        #region Search Roads

        /// <summary>
        /// Gets the search result for.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>The <see cref="ListRoadsViewModel"/>.</returns>
        public static ListRoadsViewModel GetSearchResultFor(IRoadsService client, FindRoadModel model, bool isRouteValidation)
        {
            var responce = new ListRoadsViewModel();
            string currentLang = CultureHelper.GetNeutralCulture(CultureHelper.GetCurrentCulture());
                var data = client.GetRoadsFor(model.OriginCityNodeId, model.DestinationCityNodeId,
					model.SearchResult.PageNumber, currentLang, isRouteValidation );

                responce.ActualRange = data.ActualRange;

                responce.NumberOfFound = data.Count;

                responce.RecordsPerPage = data.RecordsPerPage;

                responce.PageNumber = model.SearchResult.PageNumber;

                responce.RoadsList =
                    data.Treks.Select(s => new RoadSearchingResultViewModel
                    {
                        OriginCityName = s.OriginCityNodeName,
                        DestinationCityName = s.DesinationCityNodeName,
                        RouteHash = s.Hash,
                        Trek = s.Track,
                        InterimCities = s.Track.Split('-').Count() - 2,
                        FeedbacksCount = s.FeedbackCount
                        
                    }).ToList();

            return responce;
        }

        #endregion

        #region FeedbackItems


        /// <summary>
        /// Gets the feedback models.
        /// </summary>
        /// <returns>The specific dictionary.</returns>
        public static Dictionary<Int64, string> GetFeedbackModels(IRoadsService client)
        {
            var feedbacksModels = client.GetFeedbackControlsList();

            var result = new Dictionary<long, string>();

            foreach (var model in feedbacksModels)
            {
                result.Add(model.FeedbackModelId, model.FeedBackModalName);
            }

            return result;
        }


        /// <summary>
        /// Gets the feedback items.
        /// </summary>
        /// <returns>The <see cref="FeedbackItemModel"/>.</returns>
        public static FeedbackItemModel GetFeedbackItems(IRoadsService client)
        {
                var feedbacksItemsListFromWcf = client.GetFeedbackItemsData().OrderBy(x => x.SortNumber).ToList();
            var feedbacksItemsListForView = new FeedbackItemModel();

            foreach (var feedbackItemData in feedbacksItemsListFromWcf)
            {
                feedbacksItemsListForView.feedbackItemSettings.Add(new FeedbackItemSettings
                {
                    feedbackItemId = feedbackItemData.FeedbackItemId,
                    settingName = feedbackItemData.NameTranslationKey,
                    sortNumber = feedbackItemData.SortNumber,
                    description = feedbackItemData.DescriptionTranslationKey,
                    isNumeric = feedbackItemData.IsNumeric,
                    isMandatory = feedbackItemData.Mandatory,
                    feedbackModelId = feedbackItemData.FeedbackModelId
                });
            }
            return feedbacksItemsListForView;
        }

        /// <summary>
        /// Sets the feedback items.
        /// </summary>
        /// <param name="feedbacksItemsListFromView">The feedbacks items list from view.</param>
        public static void SetFeedbackItems(IRoadsService client, List<FeedbackItemSettings> feedbacksItemsListFromView)
        {
            var feedbacksItemsListForWCF = new List<FeedbackItemData>();

            foreach (var feedbackItemsFromView in feedbacksItemsListFromView)
            {
                feedbacksItemsListForWCF.Add(new FeedbackItemData
                {
                    FeedbackItemId = feedbackItemsFromView.feedbackItemId,
                    NameTranslationKey = feedbackItemsFromView.settingName,
                    SortNumber = feedbackItemsFromView.sortNumber,
                    DescriptionTranslationKey = feedbackItemsFromView.description,
                    IsNumeric = feedbackItemsFromView.isNumeric,
                    Mandatory = feedbackItemsFromView.isMandatory,
                    FeedbackModelId = feedbackItemsFromView.feedbackModelId
                });
            }
            client.SetFeedbackItemsData(feedbacksItemsListForWCF.ToArray());
        }

        /// <summary>
        /// Deletes the feedback item.
        /// </summary>
        /// <param name="feedbackItemFromView">The feedback item from view.</param>
        public static void DeleteFeedbackItem(IRoadsService client, FeedbackItemSettings feedbackItemFromView)
        {
            var feedbackItemForDelete = new FeedbackItemData
            {
                FeedbackItemId = feedbackItemFromView.feedbackItemId,
                NameTranslationKey = feedbackItemFromView.settingName,
                SortNumber = feedbackItemFromView.sortNumber,
                DescriptionTranslationKey = feedbackItemFromView.description,
                IsNumeric = feedbackItemFromView.isNumeric,
                Mandatory = feedbackItemFromView.isMandatory,
                FeedbackModelId = feedbackItemFromView.feedbackModelId
            };
            client.DeleteFeedbackItem(feedbackItemForDelete);
        }

        /// <summary>
        /// Adds the new feedback item.
        /// </summary>
        /// <param name="feedbackItemFromView">The feedback item from view.</param>
        public static void AddNewFeedbackItem(IRoadsService client, FeedbackItemSettings feedbackItemFromView)
        {
            var feedbackItemForDelete = new FeedbackItemData
            {
                FeedbackItemId = feedbackItemFromView.feedbackItemId,
                NameTranslationKey = feedbackItemFromView.settingName,
                SortNumber = feedbackItemFromView.sortNumber,
                DescriptionTranslationKey = feedbackItemFromView.description,
                IsNumeric = feedbackItemFromView.isNumeric,
                Mandatory = feedbackItemFromView.isMandatory,
                FeedbackModelId = feedbackItemFromView.feedbackModelId
            };
            client.AddNewFeedbackItem(feedbackItemForDelete);
        }

        /// <summary>
        /// Gets all available languages.
        /// </summary>
        /// <returns>The List of <see cref="Language"/>.</returns>
        public static List<Language> GetAllAvailableLanguage(IRoadsService client)
        {
            var langListFromWcf = client.GetAllLanguages();

            return langListFromWcf.Select(lang => new Language
                {
                    LanguageId = lang.LanguageId,
                    Name = lang.Name,
                    IsDefault = lang.IsDefault
                }).ToList();
        }

        /// <summary>
        /// Gets all dynamic translations.
        /// </summary>
        /// <returns>The List of <see cref="DynamicTranslation"/>.</returns>
        public static List<DynamicTranslation> GetAllDynamicTranslations(IRoadsService client)
        {
            var dynamicTranslationsListFromWcf = client.GetAllDynamicTranslationsData();
            var dynamicTranslationsForModel = new List<DynamicTranslation>();

            foreach (var dynamicTraslation in dynamicTranslationsListFromWcf)
            {
                dynamicTranslationsForModel.Add(new DynamicTranslation
                {
                    DynamicObjectId = dynamicTraslation.DynamicObjectId,
                    Value = dynamicTraslation.Value,
                    DescriptionValue = dynamicTraslation.DescriptionValue,
                    LanguageId = dynamicTraslation.LanguageId,
                    DynamicKey = dynamicTraslation.DynamicKey
                });

            }
            return dynamicTranslationsForModel;
        }

        /// <summary>
        /// Updates the dynamic translations.
        /// </summary>
        /// <param name="dynamicTranslationsList">The dynamic translations list.</param>
        public static void UpdateDynamicTranslations(IRoadsService client, List<DynamicTranslation> dynamicTranslationsList)
        {
            List<DynamicTranslationsData> dynamicTraslationListForWcf = new List<DynamicTranslationsData>();

            foreach (var dynamicTraslation in dynamicTranslationsList)
            {
                dynamicTraslationListForWcf.Add(new DynamicTranslationsData
                {
                    DynamicObjectId = dynamicTraslation.DynamicObjectId,
                    Value = dynamicTraslation.Value,
                    DescriptionValue = dynamicTraslation.DescriptionValue,
                    LanguageId = dynamicTraslation.LanguageId,
                    DynamicKey = dynamicTraslation.DynamicKey
                });                
            }
            client.UpdateDynamicTranslations(dynamicTraslationListForWcf.ToArray());           
        }

        /// <summary>
        /// Adds the dynamic translations.
        /// </summary>
        /// <param name="newDynamicTranslationsList">The new dynamic translations list.</param>
        public static void AddDynamicTranslations(IRoadsService client, List<DynamicTranslation> newDynamicTranslationsList)
        {
            List<DynamicTranslationsData> dynamicTraslationListForWcf = new List<DynamicTranslationsData>();

            foreach (var dynamicTraslation in newDynamicTranslationsList)
            {
                dynamicTraslationListForWcf.Add(new DynamicTranslationsData
                {
                    DynamicObjectId = dynamicTraslation.DynamicObjectId,
                    Value = dynamicTraslation.Value,
                    DescriptionValue = dynamicTraslation.DescriptionValue,
                    LanguageId = dynamicTraslation.LanguageId,
                    DynamicKey = dynamicTraslation.DynamicKey
                });
            }
            client.AddDynamicTranslations(dynamicTraslationListForWcf.ToArray());    
        }

        /// <summary>
        /// Deletes the dynamic translations.
        /// </summary>
        /// <param name="dynamicKeysForDelete">The dynamic keys for delete.</param>
        public static void DeleteDynamicTranslations(IRoadsService client, List<string> dynamicKeysForDelete)
        {
            client.DeleteDynamicTranslations(dynamicKeysForDelete.ToArray());
        }
        #endregion

        #region Find Road Details Region

        /// <summary>
        /// Gets the find route details model.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="urlId">The URL identifier.</param>
        /// <param name="language">The language.</param>
        /// <returns>Return view model for Find Route Details page.</returns>
        public static FindRoadDatailsModel GetFindRouteDetailsModel(IRoadsService client, string urlId, string language)
        {
            var model = new FindRoadDatailsModel();

            string hash = GetHashFromUrl(urlId);

            var rdControls = new List<FeedbackControl>();

            HolisticFeedback[] feedmackModel = client.GetFeedbackControlsListForNewFeedback();
            rdControls.AddRange(feedmackModel.Select(holisticFeedback => new FeedbackControl
            {
                DescriptionTranslationKey = holisticFeedback.FeedbackItem.DescriptionTranslationKey,
                SortNumber = holisticFeedback.FeedbackItem.SortNumber,
                NameTranslationKey = holisticFeedback.FeedbackItem.NameTranslationKey,
                HTMLCode = holisticFeedback.FeedbackModel.HtmlCode,
                JSCode = holisticFeedback.FeedbackModel.JavascriptCode,
                FeedBackItemId = holisticFeedback.FeedbackItem.FeedbackItemId,
                IsMandatory = holisticFeedback.FeedbackItem.Mandatory
            }));

            model.MandatoryControlsId =
                rdControls.Where(item => item.IsMandatory == true).Select(it => it.FeedBackItemId).ToArray();

            RouteDetailsData routeDetails = GetRouteDetails(client, hash, CultureHelper.GetCulture(language));

            model.RDNodesData = FillRDNodesData(routeDetails.RouteDetailsItems, rdControls);

            model.CityPointsNames = FillCityPList(model.RDNodesData);

            model.RouteSummary = FillRouteSummary(model.RDNodesData);

            return model;
        }

        /// <summary>
        /// Fills the route summary.
        /// </summary>
        /// <param name="rdNodesData">The rd nodes data.</param>
        /// <returns>Return RDNodeSummary model.</returns>
        /// <exception cref="System.ArgumentNullException">rdNodesData</exception>
        private static RDNodeSummary FillRouteSummary(List<RDNodeData> rdNodesData)
        {
            if (rdNodesData != null && rdNodesData.Count == 0)
            {
                throw new ArgumentNullException("rdNodesData");
            }

            var mainSummary = new RDNodeSummary
            {
                From = rdNodesData[0].Summary.From,
                To = rdNodesData[rdNodesData.Count - 1].Summary.To,
                Controls = new List<RDSummaryControl>()
            };

            foreach (var rdNodeData in rdNodesData)
            {
                if (rdNodeData == null || rdNodeData.Summary.Controls == null || rdNodeData.Summary.Controls.Count == 0)
                {
                    //TODO :: add tracking
                    break;
                }

                foreach (var summaryControl in rdNodeData.Summary.Controls)
                {
                    if (summaryControl == null)
                    {
                        //TODO :: add tracking
                        break;
                    }

                    int index = mainSummary.Controls.FindIndex(m => m.FeedbackModelId == summaryControl.FeedbackModelId &&
                                                                         m.NameTranslationKey == summaryControl.NameTranslationKey);

                    if (index >= 0)
                    {
                        mainSummary.Controls[index].Value.Add(summaryControl.GetSummuryValue());
                    }
                    else
                    {
                        mainSummary.Controls.Add(new RDSummaryControl
                        {
                            DescriptionTranslationKey = summaryControl.DescriptionTranslationKey,
                            FeedbackModelId = summaryControl.FeedbackModelId,
                            NameTranslationKey = summaryControl.NameTranslationKey,
                            SortNumber = summaryControl.SortNumber,
                            Value = new List<long>(new List<long> { summaryControl.GetSummuryValue() })
                        });
                    }
                }
            }
            //Sort controls by sort number (Sort Descending)
            mainSummary.Controls.Sort((m, n) => m.SortNumber.CompareTo(n.SortNumber));

            return mainSummary;
        }

        /// <summary>
        /// Fills the city p list.
        /// </summary>
        /// <param name="rdNodesData">The rd nodes data.</param>
        /// <returns>Return list of city names.</returns>
        /// <exception cref="System.ArgumentNullException">rdNodesData</exception>
        private static List<string> FillCityPList(List<RDNodeData> rdNodesData)
        {
            if (rdNodesData != null && rdNodesData.Count == 0)
            {
                throw new ArgumentNullException("rdNodesData");
            }

            var cityPList = new List<string>();

            for (int i = 0; i < rdNodesData.Count; i++)
            {
                if (rdNodesData[i] != null && rdNodesData[i].Summary != null)
                {
                    cityPList.Add(rdNodesData[i].Summary.From);

                    if (i == rdNodesData.Count - 1)
                    {
                        cityPList.Add(rdNodesData[i].Summary.To);
                    }
                }
            }

            return cityPList;
        }

        /// <summary>
        /// Fills the rd nodes data.
        /// </summary>
        /// <param name="rdFeedbacks">The rd feedbacks.</param>
        /// <returns>Retrun list of RDNodeData models.</returns>
        /// <exception cref="System.ArgumentNullException">rdFeedbacks</exception>
        private static List<RDNodeData> FillRDNodesData(IEnumerable<RouteDetailsFeedback[]> rdFeedbacks, List<FeedbackControl> rdControls)
        {
            if (rdFeedbacks == null)
            {
                throw new ArgumentNullException("rdFeedbacks");
            }

            var rdNodesData = new List<RDNodeData>();

            foreach (var routeDetailsArray in rdFeedbacks)
            {
                if (routeDetailsArray == null || routeDetailsArray.Length == 0)
                {
                    //TODO :: add tracking
                    break;
                }

                var rdNodesDataItem = new RDNodeData
                {
                    Feedbacks = new List<RDNodeFeedback>(),
                    LeaveFeedbackWindow = new RDLeaveFeedback
                    {
                        Controls = rdControls
                    },
                    Summary = new RDNodeSummary
                    {
                        Controls = new List<RDSummaryControl>(),
                        From = routeDetailsArray[0].OriginCityName,
                        To = routeDetailsArray[0].DestinationCityName,
                    }
                };

                foreach (var routeDetail in routeDetailsArray)
                {
                    if (routeDetail == null || routeDetail.FeedbackValues == null || routeDetail.FeedbackValues.Length == 0)
                    {
                        //TODO :: add tracking
                        break;
                    }

                    foreach (var feedbackValue in routeDetail.FeedbackValues)
                    {
                        if (feedbackValue == null)
                        {
                            //TODO :: add tracking
                            break;
                        }

                        if (feedbackValue.FeedbackItem.IsNumeric)
                        {
                            //Todo make condition more easy
                            int index =
                                rdNodesDataItem.Summary.Controls.FindIndex(
                                    m => m.FeedbackModelId == feedbackValue.FeedbackItem.FeedbackModelId &&
                                            m.NameTranslationKey == feedbackValue.FeedbackItem.NameTranslationKey);

                            if (index >= 0)
                            {
                                int value = 0;
                                if (Int32.TryParse(feedbackValue.Value, out value))
                                {
                                    rdNodesDataItem.Summary.Controls[index].Value.Add(value);
                                }
                            }
                            else
                            {
                                rdNodesDataItem.Summary.Controls.Add(FillFeedbackSummaryControl(feedbackValue));
                            }
                        }
                    }
                    rdNodesDataItem.Feedbacks.Add(FillFeedbackItemDateils(routeDetail));
                }
                //Sort feeedbacks by date (Sort Descending)
                rdNodesDataItem.Feedbacks.Sort((m, n) => n.SubmitTime.CompareTo(m.SubmitTime));
                //Sort controls by sort number (Sort Descending)
                rdNodesDataItem.Summary.Controls.Sort((m, n) => m.SortNumber.CompareTo(n.SortNumber));

                rdNodesDataItem.LeaveFeedbackWindow = new RDLeaveFeedback
                {
                    Controls = rdControls,
                    PostModel = new RDLeaveFeedbackModel
                    {
                        DestinationCityId = routeDetailsArray[0].DestinationCityId,
                        OriginCityId = routeDetailsArray[0].OriginCityId
                    }
                };
                rdNodesData.Add(rdNodesDataItem);
            }
            return rdNodesData;
        }

        /// <summary>
        /// Fills the feedback summary control.
        /// </summary>
        /// <param name="routeDetail">The route detail.</param>
        /// <returns>Return RDSummaryControl model.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// routeDetail
        /// or
        /// routeDetail.Value
        /// or
        /// routeDetail.FeedbackItem
        /// </exception>
        /// <exception cref="System.ArgumentException">Control value is invalid.</exception>
        private static RDSummaryControl FillFeedbackSummaryControl(FeedbackValueData routeDetail)
        {

            if (routeDetail == null)
            {
                throw new ArgumentNullException("routeDetail");
            }
            if (String.IsNullOrEmpty(routeDetail.Value))
            {
                throw new ArgumentNullException("routeDetail.Value");
            }
            if (routeDetail.FeedbackItem == null)
            {
                throw new ArgumentNullException("routeDetail.FeedbackItem");
            }

            int value;
            if (Int32.TryParse(routeDetail.Value, out value) == false)
            {
                throw new ArgumentException("Control value is invalid.");
            }

            return new RDSummaryControl
            {
                Value = new List<long> { value },
                NameTranslationKey = routeDetail.FeedbackItem.NameTranslationKey,
                SortNumber = routeDetail.FeedbackItem.SortNumber,
                FeedbackModelId = routeDetail.FeedbackItem.FeedbackModelId,
                DescriptionTranslationKey = routeDetail.FeedbackItem.DescriptionTranslationKey
            };
        }

        /// <summary>
        /// Fills the feedback item dateils.
        /// </summary>
        /// <param name="rdFeedback">The rd feedback.</param>
        /// <returns>Return RDNodeFeedback model.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// rdFeedback
        /// or
        /// rdFeedback.FeedbackValues
        /// </exception>
        private static RDNodeFeedback FillFeedbackItemDateils(RouteDetailsFeedback rdFeedback)
        {
            if (rdFeedback == null)
            {
                throw new ArgumentNullException("rdFeedback");
            }
            if (rdFeedback.FeedbackValues == null)
            {
                throw new ArgumentNullException("rdFeedback.FeedbackValues");
            }

            var valuesList = rdFeedback.FeedbackValues.Select(feedbackValue => new RDNodeFeedbackValue
            {
                DescriptionTranslationKey = feedbackValue.FeedbackItem.DescriptionTranslationKey,
                Value = feedbackValue.Value,
                NameTranslationKey = feedbackValue.FeedbackItem.NameTranslationKey,
                SortNumber = feedbackValue.FeedbackItem.SortNumber,
                IsNumeric = feedbackValue.FeedbackItem.IsNumeric,
                Mandatory = feedbackValue.FeedbackItem.Mandatory
            }).ToList();

            var dateilsItem = new RDNodeFeedback
            {
                FeedbackValues = valuesList,
                FeedbackId = rdFeedback.FeedbackId,
                SubmitTime = rdFeedback.SubmitTime,
                UserName = rdFeedback.UserName,
            };
            return dateilsItem;
        }

        /// <summary>
        /// Gets the hash from URL.
        /// </summary>
        /// <param name="urlId">The URL identifier.</param>
        /// <returns>Return hash from url.</returns>
        public static string GetHashFromUrl(string urlId)
        {
            string[] urlArray = urlId.Split('-');
            if (urlArray.Length > 0)
            {
                return urlArray[0];
            }
            return null;
        }

        /// <summary>
        /// Gets the route details from Wcf.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="hash">The hash.</param>
        /// <param name="language">The language.</param>
        /// <returns>Returns RouteDetailsData model.</returns>
        private static RouteDetailsData GetRouteDetails(IRoadsService client, string hash, string language)
        {
            return client.GetRouteDetailsByHash(hash, language);
        }

        #endregion

        #region Add road step two

        /// <summary>
        /// Adds the new feedbacks or route if not exist.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="routeFeedbacksModel">The route feedbacks model.</param>
        public static string AddNewFeedbacksOrRouteIfNotExist(IRoadsService client, AddRoadModelForStepTwo routeFeedbacksModel)
        {
            List<RouteNodeWithFeedbacksData> routeNodeWithFeedbacks = new List<RouteNodeWithFeedbacksData>();

            int userId = client.GetUserIdOrCreateNewIfNotExist(routeFeedbacksModel.UserName, routeFeedbacksModel.UserName, "TestUserRole");

            routeFeedbacksModel.Feedbacks.ForEach(feedback =>
            {
                routeNodeWithFeedbacks.Add(new RouteNodeWithFeedbacksData()
                {
                    DestinationCityNodeId = int.Parse(feedback.DestinationCityNodeId),
                    OriginCityNodeId = int.Parse(feedback.OriginCityNodeId),
                    OriginCityNode = feedback.OriginCityName,
                    DestinationCityNode = feedback.DestinationCityName,
                    SubmitTime = DateTime.Now,
                    UserId = userId,
                    FeedbackValues = feedback.FeedbackValues.Select(feedbackValue => new FeedbackValueData()
                    {
                        FeedbackItemId = feedbackValue.FeedbackItemId.Value,
                        Value = feedbackValue.Value == "undefined" ? "" : feedbackValue.Value
                    }).ToArray()
                });
            });

            return client.AddFeedbacksToNewOrExistingRoutes(routeNodeWithFeedbacks.ToArray());
        }

        /// <summary>
        /// Creates the new feedback.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="leaveFeedback">The leave feedback.</param>
        public static void CreateNewFeedback(IRoadsService client, RDLeaveFeedbackModel leaveFeedback)
        {

            var valueList = new List<FeedbackValueData>();

            leaveFeedback.RDLeaveFeedbackValues.ForEach(m =>
            {
                int feedbackItemId;
                if (int.TryParse(m.FeedbackItemId, out feedbackItemId))
                {
                    valueList.Add(new FeedbackValueData
                    {
                        FeedbackItemId = feedbackItemId,
                        Value = m.Value
                    });
                }
            });

            var newFeedback = new RouteNodeWithFeedbacksData
            {
                DestinationCityNodeId = leaveFeedback.DestinationCityId,
                OriginCityNodeId = leaveFeedback.OriginCityId,
                SubmitTime = leaveFeedback.SubmitTime,
                UserId = leaveFeedback.UserId,
                FeedbackValues = valueList.ToArray()
            };

            client.CreateFeedback(newFeedback);
        }

        /// <summary>
        /// Translates the cities.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="lang">The language.</param>
        /// <param name="cities">The cities.</param>
        public static void TranslateCities(IRoadsService client, string lang, ref List<KeyValuePair<City, City>> cities)
        {
            cities.ForEach(cityPair =>
            {
                cityPair.Key.CityName = client.GetFullTranslationForCityById(int.Parse(cityPair.Key.CityId), lang);
                cityPair.Value.CityName = client.GetFullTranslationForCityById(int.Parse(cityPair.Value.CityId), lang);
            });
        }
        #endregion

        #region Map Objects

        /// <summary>
        /// Gets the translation data for.
        /// </summary>
        /// <param name="cityNodeId">The city node identifier.</param>
        /// <returns>The list of <see cref="TranslationViewModel"/>.</returns>
        public static List<TranslationViewModel> GetTranslationDatasFor(IRoadsService client, long cityNodeId)
        {
            var leng = GetLanguages(client);

                var lenguages = new SelectList(leng.Select(s =>
                new SelectListItem
                {
                    Value = s.LanguageId.ToString(CultureInfo.InvariantCulture),
                    Text = s.Name,
                    Selected = s.IsDefault
                }), "Value", "Text", leng.First(e => e.IsDefault).LanguageId);

                return client.GetMapObjectTranslationDataFor(cityNodeId).Select(s => new TranslationViewModel
                {
                    ObjectId = s.MapObjectId,
                    LanguageKey = s.LanguageKey,
                    Value = s.Value,
                    LanguageId = s.LanguageId,
                    Languages = lenguages

                }).ToList();
        }

        /// <summary>
        /// Updates the map objects translations.
        /// </summary>
        /// <param name="translations">The translations.</param>
        public static void UpdateMapObjectsTranslations(IRoadsService client, List<TranslationViewModel> translations)
        {
                client.UpdateMapObjectTranslation(translations.Select(s => new MapObjectTranslationData
                {
                    LanguageId = s.LanguageId,
                    LanguageKey = s.LanguageKey,
                    Value = s.Value,
                    MapObjectId = s.ObjectId
                }).ToArray());
        }

        /// <summary>
        /// Fills the create map object model.
        /// </summary>
        /// <param name="model">The model.</param>
        public static void FillCreateMapObjectModel(IRoadsService client, CreateMapObjectViewModel model)
        {
            var lang = GetLanguages(client);

                var languages = new SelectList(lang.Select(s =>
                    new SelectListItem
                    {
                        Value = s.LanguageId.ToString(CultureInfo.InvariantCulture),
                        Text = s.Name,
                        Selected = s.IsDefault
                    }), "Value", "Text", lang.First(e => e.IsDefault).LanguageId);

                if (model.Translations.Any())
                {
                    foreach (var t in model.Translations)
                    {
                        t.LanguageKey = "null";
                        t.Languages = languages;
                    }
                }
                else
                {
                    foreach (var l in languages)
                    {
                        model.Translations.Add(new TranslationViewModel
                        {
                            Languages = languages, 
                            LanguageKey = "null", 
                            LanguageId = int.Parse(l.Value)
                        });
                    }
                }

                string selectedLanguage = CultureHelper.GetCulture(Thread.CurrentThread.CurrentUICulture.Name);

                var regions = client.GetRegionsListForLanguage(selectedLanguage);

                model.Regions = new SelectList(regions.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(CultureInfo.InvariantCulture),
                    Text = s.Name,
                    Selected = false
                }), "Value", "Text", 1);
            }
        /// <summary>
        /// Saves the map object.
        /// </summary>
        /// <param name="model">The model.</param>
        public static void SaveMapObject(CreateMapObjectViewModel model)
        {
            using (var client = new RoadsServiceClient.RoadsServiceClient())
            {
                var regionId = model.UseRegion ? model.RegionId : 0;

                client.CreateMapObjectWithTranslation(
                    model.Translations.Select(s => new MapObjectTranslationData { Value = s.Value, LanguageId = s.LanguageId }).ToArray(),
                    regionId,
                    model.LanguageKey);
            }
        }

        #endregion
    }
}