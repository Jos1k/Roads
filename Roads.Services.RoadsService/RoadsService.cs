using System;
using System.Collections.Generic;
using System.ServiceModel;
using Roads.Common.Integrations;
using Roads.Common.Managers;
using Roads.Common.Models;
using Roads.Common.Models.DataContract;
using Roads.Common.Models.Enums;

namespace Roads.Services.RoadsService
{
    [ServiceBehavior(
    InstanceContextMode = InstanceContextMode.PerSession,
    IncludeExceptionDetailInFaults = true)]
    public class RoadsService : IRoadsService, IDisposable
    {
        #region Private fields

        private IResourceManager _resourceManager;
        private ILanguagesManager _languagesManager;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="RoadsService"/> class.
        /// </summary>
        /// <param name="resourceManager">The resource manager.</param>
        /// <param name="languagesManager">The languages manager.</param>
        public RoadsService(IResourceManager resourceManager, ILanguagesManager languagesManager)
        {
            _resourceManager = resourceManager;
            _languagesManager = languagesManager;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RoadsService"/> class.
        /// </summary>
        public RoadsService()
        {
        }

        #region Suggestions operations

        /// <summary>
        /// Return array of suggestions retrieved from database.
        /// </summary>
        /// <param name="searchString">The search string.</param>
        /// <param name="language">The language string.</param>
        /// <returns>Returns array of suggestions.</returns>
        public List<Suggestion> GetSuggestions(string searchString, string language)
        {
            try
            {
                var manager = new SmartSuggestionsManager();

                int languageId = new LanguagesManager().GetLanguageId(language);

                return manager.GetSuggestions(searchString, languageId, 20);
            }
            catch (Exception ex)
            {
                return new List<Suggestion>();
            }
        }

        /// <summary>
        /// Gets the feedback controls list for new feedback.
        /// </summary>
        /// <returns></returns>
        public List<HolisticFeedback> GetFeedbackControlsListForNewFeedback()
        {
            try
            {
                return new RoadsManager().GetFeedbackControlsForNewFeedback();
            }
            catch (Exception ex)
            {
                return new List<HolisticFeedback>();
            }
        }

        #region Settings operations

        /// <summary>
        /// Gets the feedback controls list for new feedback.
        /// </summary>
        /// <returns>Return RouteDetailsData objeect.</returns>
        public RouteDetailsData GetRouteDetailsByHash(string hash, string language)
        {
            try
            {
                var manager = new RoadsManager();
                return manager.GetRouteDetailsByHash(hash, language);
            }
            catch (Exception ex)
            {
                return new RouteDetailsData();
            }
        }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <returns>Return the list of settings</returns>
        public List<SettingData> GetSettings()
        {
            try
            {
                var settingsManager = new SettingsManager();
                return settingsManager.GetSettings();
            }
            catch (Exception ex)
            {
                return new List<SettingData>();
            }
        }

        /// <summary>
        /// Sets the settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public void SetSettings(List<SettingData> settings)
        {
            try
            {
                var settingsManager = new SettingsManager();
                settingsManager.SetSettings(settings);
            }
            catch (Exception ex)
            {
                return;
            }
        }

        #endregion

        #region Feedbacks operations

        /// <summary>
        /// Gets the feedback items data.
        /// </summary>
        /// <returns>List of <see cref="FeedbackItemData"/>.</returns>
        public List<FeedbackItemData> GetFeedbackItemsData()
        {
            try
            {
                var feedbackManager = new FeedbackItemDataManager();
                return feedbackManager.GetFeedbackItemsData();
            }
            catch (Exception ex)
            {
                //TODO ADD tracking
                return new List<FeedbackItemData>();
            }
        }

        /// <summary>
        /// Sets the feedback items data.
        /// </summary>
        /// <param name="feedbackItemsDataList">The feedback items data list.</param>
        public void SetFeedbackItemsData(List<FeedbackItemData> feedbackItemsDataList)
        {
            try
            {
                var feedbackManager = new FeedbackItemDataManager();
                feedbackManager.SetFeedbackItemsData(feedbackItemsDataList);
            }
            catch (Exception ex)
            {
                //TODO ADD tracking
            }
        }

        /// <summary>
        /// Deletes the feedback item.
        /// </summary>
        /// <param name="feedbackItemForDelete">The feedback item for delete.</param>
        public void DeleteFeedbackItem(FeedbackItemData feedbackItemForDelete)
        {
            try
            {
                var feedbackManager = new FeedbackItemDataManager();
                feedbackManager.DeleteFeedbackItem(feedbackItemForDelete);
            }
            catch (Exception ex)
            {
                //TODO ADD tracking
            }
        }

        /// <summary>
        /// Adds the new feedback item.
        /// </summary>
        /// <param name="feedbackItemForAdd">The feedback item for add.</param>
        public void AddNewFeedbackItem(FeedbackItemData feedbackItemForAdd)
        {
            try
            {
                var feedbackManager = new FeedbackItemDataManager();
                feedbackManager.AddNewFeedbackItem(feedbackItemForAdd);
            }
            catch (Exception ex)
            {
                //TODO ADD tracking
            }
        }

        /// <summary>
        /// Gets the feedback controls list.
        /// </summary>
        /// <returns>The list of <see cref="FeedbackModelData"/>.</returns>
        public List<FeedbackModelData> GetFeedbackControlsList()
        {
            try
            {
                var feedbackManager = new FeedbackItemDataManager();

                return feedbackManager.GetFeedbackModels();
            }
            catch (Exception ex)
            {
                //TODO ADD tracking
                return new List<FeedbackModelData>();
            }
        }

        #endregion

        #region StaticTranslations operations

        /// <summary>
        /// Gets the label translation by id and language.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <param name="language">The user language.</param>
        /// <returns>Return Translation of label.</returns>
        public string GetLabelTranslation(string labelId, string language)
        {
            try
            {
                return _resourceManager.GetResource(TranslationTypeEnum.StaticTranslation, labelId, language);
            }
            catch (Exception ex)
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// Gets the static translation data.
        /// </summary>
        /// <param name="translationKey">The translation key.</param>
        /// <returns>The list of <see cref="StaticTranslationData"/>.</returns>
        public List<StaticTranslationData> GetStaticTranslationData(string translationKey)
        {
            try
            {
                return _resourceManager.GetStaticTranslationData(translationKey);
            }
            catch (Exception ex)
            {
                //TODO ADD tracking
                return new List<StaticTranslationData>();
            }
        }

        /// <summary>
        /// Gets all static translations.
        /// </summary>
        /// <returns>The list of <see cref="StaticTranslationData"/></returns>
        public List<StaticTranslationData> GetAllStaticTranslations()
        {
            try
            {
                if (_resourceManager == null)
                {
                    _resourceManager = new ResourceManager();
                }
                return _resourceManager.GetAllStaticTranslationsData();
            }
            catch (Exception ex)
            {
                //TODO ADD tracking
                return new List<StaticTranslationData>();
            }
        }

        /// <summary>
        /// Updates the static translation.
        /// </summary>
        /// <param name="translations">The static translations data list.</param>
        public void UpdateStaticTranslation(List<StaticTranslationData> translations)
        {
            try
            {
                _resourceManager.UpdateResource(translations);
            }
            catch (Exception ex)
            {
                //TODO ADD tracking
            }
        }

        #endregion

        #region DynamicTranslations operations

        /// <summary>
        /// Gets all dynamic translations data.
        /// </summary>
        /// <returns>The list of <see cref="DynamicTranslationsData"/></returns>
        public List<DynamicTranslationsData> GetAllDynamicTranslationsData()
        {
            try
            {
                return _resourceManager.GetAllDynamicTranslationsData();
            }
            catch (Exception ex)
            {
                //TODO ADD tracking
                return new List<DynamicTranslationsData>();
            }
        }

        /// <summary>
        /// Gets the dynamic translation.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <param name="language">The language.</param>
        /// <returns>Dynamic translation value.</returns>
        public string GetDynamicTranslation(string labelId, string language)
        {
            try
            {
                return _resourceManager.GetResource(TranslationTypeEnum.DynamicTranslation, labelId, language);
            }
            catch (Exception ex)
            {
                //TODO ADD tracking
                return String.Empty;
            }
        }

        /// <summary>
        /// Gets the dynamic translation data.
        /// </summary>
        /// <param name="translationKey">The translation key.</param>
        /// <returns>The list of <see cref="DynamicTranslationsData"/>.</returns>
        public List<DynamicTranslationsData> GetDynamicTranslationData(string translationKey)
        {
            try
            {
                return _resourceManager.GetDynamicTranslationsData(translationKey);
            }
            catch (Exception ex)
            {
                //TODO ADD tracking
                return new List<DynamicTranslationsData>();
            }
        }

        /// <summary>
        /// Adds the dynamic translation.
        /// </summary>
        /// <param name="dynamicTranslationsData">The dynamic translations data.</param>
        public void AddDynamicTranslations(List<DynamicTranslationsData> dynamicTranslationsData)
        {
            try
            {
                _resourceManager.AddResource(dynamicTranslationsData);
            }
            catch (Exception ex)
            {
                //TODO ADD tracking
            }
        }

        /// <summary>
        /// Updates the dynamic translation.
        /// </summary>
        /// <param name="dynamicTranslationsData">The dynamic translations data.</param>
        public void UpdateDynamicTranslations(List<DynamicTranslationsData> dynamicTranslationsData)
        {
            try
            {
                _resourceManager.UpdateResource(dynamicTranslationsData);
            }
            catch (Exception ex)
            {
                //TODO ADD tracking
            }
        }

        /// <summary>
        /// Deletes the dynamic translation.
        /// </summary>
        /// <param name="dynamicTranslationsKeys">The dynamic translations data.</param>
        public void DeleteDynamicTranslations(List<string> dynamicTranslationsKeys)
        {
            try
            {
                _resourceManager.DeleteDynamicResource(dynamicTranslationsKeys);
            }
            catch (Exception ex)
            {
                //TODO ADD tracking
            }
        }

        #endregion

        #region Languages operations

        /// <summary>
        /// Gets all languages.
        /// </summary>
        /// <returns>The list of <see cref="LanguageData"/></returns>
        public List<LanguageData> GetAllLanguages()
        {
            try
            {
                return _languagesManager.GetAllLanguagesData();
            }
            catch (Exception ex)
            {
                //TODO ADD tracking
                return new List<LanguageData>();
            }
        }

        #endregion

        /// <summary>
        /// Gets the map object translation data for.
        /// </summary>
        /// <param name="cityNodeId">The city node identifier.</param>
        /// <returns>The list of <see cref="MapObjectTranslationData"/></returns>
        public List<MapObjectTranslationData> GetMapObjectTranslationDataFor(long cityNodeId)
        {
            try
            {
                using (var manager = new MapObjectManager())
                {
                    return manager.GetTranslationsFor(cityNodeId);
                }
            }
            catch (Exception ex)
            {
                //TODO ADD tracking
                return new List<MapObjectTranslationData>();
            }
        }

        /// <summary>
        /// Updates the map object translation.
        /// </summary>
        /// <param name="objectTranslations">The list of <see cref="MapObjectTranslationData" />.</param>
        public void UpdateMapObjectTranslation(List<MapObjectTranslationData> objectTranslations)
        {
            using (var manager = new MapObjectManager())
            {
                manager.UpdateMapObjectTranslation(objectTranslations);
            }
        }

        /// <summary>
        /// Gets the regions list for language.
        /// </summary>
        /// <param name="languageName">Name of the language.</param>
        /// <returns>The list of <see cref="ObjectTranslationsData"/>.</returns>
        public List<ObjectTranslationsData> GetRegionsListForLanguage(string languageName)
        {
            try
            {
                using (var manager = new MapObjectManager())
                {
                    return manager.GetRegionsForSelectList(languageName);
                }
            }
            catch (Exception ex)
            {
                //TODO ADD tracking
                return new List<ObjectTranslationsData>();
            }
        }

        /// <summary>
        /// Creates the map object with translation.
        /// </summary>
        /// <param name="translations">The translations.</param>
        /// <param name="regionId">The region identifier.</param>
        /// <param name="languageKey">The language key.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void CreateMapObjectWithTranslation(List<MapObjectTranslationData> translations, int regionId, string languageKey)
        {
            using (var manager = new MapObjectManager())
            {
                manager.CreateMapObjectWithTranslation(translations, regionId, languageKey);
            }
        }

        /// <summary>
        /// Gets the exported map object translation data.
        /// </summary>
        /// <returns>
        /// The list of <see cref="MapObjectTranslationData" />.
        /// </returns>
        public List<MapObjectTranslationData> GetExportedMapObjectTranslationData()
        {
            using (var manager = new MapObjectManager())
            {
                return manager.GetExportData();
            }
        }

        /// <summary>
        /// Processes the import map object.
        /// </summary>
        /// <param name="data">The data.</param>
        public void ProcessImportMapObject(List<MapObjectTranslationData> data)
        {
            using (var manager = new MapObjectManager())
            {
                manager.ProcessImportMapObject(data);
            }
        }

        /// <summary>
        /// Gets the roads for.
        /// </summary>
        /// <param name="startedCityId">The started city identifier.</param>
        /// <param name="destinationCityId">The destination city identifier.</param>
        /// <param name="page">The page.</param>
        /// <param name="languageName">Name of the language.</param>
        /// <returns>
        /// The list of <see cref="RoutesSearchResultData" />
        /// </returns>
		public RoutesSearchResultData GetRoadsFor( int startedCityId, int destinationCityId, int page, string languageName, bool isRouteValidation )
        {
            try
            {
                using (var manager = new RoadsManager())
                {
					return manager.GetRoutesSearchResult( startedCityId, destinationCityId, page, languageName, isRouteValidation );
                }
            }
            catch (Exception ex)
            {
                //TODO ADD tracking
                return new RoutesSearchResultData();
            }
        }

        /// <summary>
        /// Adds the feedbacks to new or existing routes.
        /// </summary>
        /// <param name="routeNodeWithFeedbacksData">The route node with feedbacks data.</param>
        public string AddFeedbacksToNewOrExistingRoutes(List<RouteNodeWithFeedbacksData> routeNodeWithFeedbacksData)
        {
            try
            {
                return new RoadsManager().AddNewFeedbackAndGetUrlToRoute(routeNodeWithFeedbacksData);
            }
            catch (Exception ex)
            {
                //TODO ADD tracking
                return String.Empty;
            }
        }

        /// <summary>
        /// Creates the feedback.
        /// </summary>
        /// <param name="feedbacksData">The feedbacks data.</param>
        public void CreateFeedback(RouteNodeWithFeedbacksData feedbacksData)
        {
            try
            {
                new RoadsManager().CreateFeedback(feedbacksData);
            }
            catch (Exception ex)
            {
                //TODO ADD tracking
            }
        }

        /// <summary>
        /// Gets the full translation for city by identifier.
        /// </summary>
        /// <param name="cityId">The city identifier.</param>
        /// <param name="language">The language.</param>
        /// <returns>Return full translation for city by Id in format: "City, Region"</returns>
        public string GetFullTranslationForCityById(int cityId, string language)
        {
            try
            {
                return new SmartSuggestionsManager().GetFullTranslationForCityById(cityId, language);
            }
            catch (Exception ex)
            {
                //TODO ADD tracking
                return String.Empty;
            }
        }

        /// <summary>
        /// Gets the user identifier or create new if not exist.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="userPassword">The user password.</param>
        /// <param name="userType">Type of the user.</param>
        /// <returns>
        /// Return user id
        /// </returns>
        public int GetUserIdOrCreateNewIfNotExist(string userName, string userPassword, string userType)
        {
            FeedbackItemDataManager feedbackItemManager = new FeedbackItemDataManager();
            int? existingUserId = feedbackItemManager.GetUserIdByName(userName);
            if (existingUserId.HasValue)
            {
                return existingUserId.Value;
            }

            return feedbackItemManager.CreateUser(userName, userPassword, userType);
        }

        #endregion

        void IDisposable.Dispose()
        {
        }

        public int GetSearchingDepth()
        {
            try
            {
                return new RoadsManager().GetSearchingDepth();
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
