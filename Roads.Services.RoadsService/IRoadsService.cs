using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using Roads.Common.Models;
using Roads.Common.Models.DataContract;

namespace Roads.Services.RoadsService
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IRoadsService
    {
        // TODO: Add your service operations here

        #region Suggestions Operation Contracts

        [OperationContract]
        List<Suggestion> GetSuggestions(string searchString, string language);

        #endregion

        #region Settings Operation Contracts

        [OperationContract]
        List<SettingData> GetSettings();

        [OperationContract]
        void SetSettings(List<SettingData> settings);

        #endregion

        #region Feedbacks Operation Contracts

        [OperationContract]
        List<HolisticFeedback> GetFeedbackControlsListForNewFeedback();

        [OperationContract]
        List<FeedbackItemData> GetFeedbackItemsData();

        [OperationContract]
        void SetFeedbackItemsData(List<FeedbackItemData> feedbackItemsDataList);

        [OperationContract]
        void DeleteFeedbackItem(FeedbackItemData feedbackItemForDelete);

        [OperationContract]
        void AddNewFeedbackItem(FeedbackItemData feedbackItemForDelete);

        [OperationContract]
        string AddFeedbacksToNewOrExistingRoutes(List<RouteNodeWithFeedbacksData> routeNodeWithFeedbacksData);

        [OperationContract]
        List<FeedbackModelData> GetFeedbackControlsList();

        #endregion

        [OperationContract]
        void CreateFeedback(RouteNodeWithFeedbacksData feedbacksData);

        #region StaticTranslations Operation Contracts

        [OperationContract]
        string GetLabelTranslation(string labelId, string userLanguage);

        [OperationContract]
        List<StaticTranslationData> GetStaticTranslationData(string translationKey);

        [OperationContract]
        void UpdateStaticTranslation(List<StaticTranslationData> translations);

        [OperationContract]
        List<StaticTranslationData> GetAllStaticTranslations();

        #endregion

        #region DynamicTranslations Operation Contracts

        [OperationContract]
        RouteDetailsData GetRouteDetailsByHash(string hash, string language);

        [OperationContract]
        string GetDynamicTranslation(string labelId, string userLanguage);

        [OperationContract]
        List<DynamicTranslationsData> GetDynamicTranslationData(string translationKey);

        [OperationContract]
        void AddDynamicTranslations(List<DynamicTranslationsData> dynamicTranslations);

        [OperationContract]
        void UpdateDynamicTranslations(List<DynamicTranslationsData> dynamicTranslations);

        [OperationContract]
        void DeleteDynamicTranslations(List<string> dynamicTranslationKeys);

        [OperationContract]
        List<DynamicTranslationsData> GetAllDynamicTranslationsData();

        #endregion

        #region Language Operation Contracts

        [OperationContract]
        List<LanguageData> GetAllLanguages();

        [OperationContract]
        string GetFullTranslationForCityById(int cityId, string language);

        #endregion

        #region Map Object Contracts

        /// <summary>
        /// Gets the map object translation data for.
        /// </summary>
        /// <param name="cityNodeId">The city node identifier.</param>
        /// <returns></returns>
        [OperationContract]
        List<MapObjectTranslationData> GetMapObjectTranslationDataFor(long cityNodeId);

        /// <summary>
        /// Updates the map object translation.
        /// </summary>
        /// <param name="objectTranslations">The list of <see cref="MapObjectTranslationData"/>.</param>
        [OperationContract]
        void UpdateMapObjectTranslation(List<MapObjectTranslationData> objectTranslations);

        /// <summary>
        /// Gets the regions list for language.
        /// </summary>
        /// <param name="languageName">Name of the language.</param>
        /// <returns>The list of <see cref="ObjectTranslationsData"/>.</returns>
        [OperationContract]
        List<ObjectTranslationsData> GetRegionsListForLanguage(string languageName);

        /// <summary>
        /// Creates the map object with translation.
        /// </summary>
        /// <param name="translations">The translations.</param>
        /// <param name="regionId">The region identifier.</param>
        /// <param name="languageKey">The language key.</param>
        [OperationContract]
        void CreateMapObjectWithTranslation(List<MapObjectTranslationData> translations, int regionId, string languageKey);

        /// <summary>
        /// Gets the exported map object translation data.
        /// </summary>
        /// <returns>The list of <see cref="MapObjectTranslationData"/>.</returns>
        [OperationContract]
        List<MapObjectTranslationData> GetExportedMapObjectTranslationData();

        /// <summary>
        /// Processes the import map object.
        /// </summary>
        /// <param name="data">The data.</param>
        [OperationContract]
        void ProcessImportMapObject(List<MapObjectTranslationData> data);

            #endregion

        [OperationContract]
		Task<RoutesSearchResultData> GetRoadsFor( int startedCityId, int destinationCityId, int page, string languageName, bool isRouteValidation );

        /// <summary>
        /// Gets the user identifier or create new if not exist.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="userPassword">The user password.</param>
        /// <param name="userType">Type of the user.</param>
        /// <returns>Return user id</returns>
        [OperationContract]
        int GetUserIdOrCreateNewIfNotExist(string userName, string userPassword, string userType);

        [OperationContract]
        int GetSearchingDepth();

    }
}
