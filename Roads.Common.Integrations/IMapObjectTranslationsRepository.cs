using Roads.Common.Models.DataContract;
using Roads.DataBase.Model.Models;
using System.Collections.Generic;

namespace Roads.Common.Integrations
{
    public interface IMapObjectTranslationsRepository : IRepositoryBase
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="languageName">Name of the language.</param>
        /// <returns>The value.</returns>
        string GetValue(string key, string languageName);

        /// <summary>
        /// Gets the region node.
        /// </summary>
        /// <param name="regionNodeId">The region node identifier.</param>
        /// <returns>The <see cref="RegionNode"/>.</returns>
        RegionNode GetRegionNode(int regionNodeId);

        /// <summary>
        /// Gets the map object translations.
        /// </summary>
        /// <param name="languageKey">The language key.</param>
        /// <param name="languageId">The language identifier.</param>
        /// <returns>The <see cref="MapObjectTranslation"/>.</returns>
        MapObjectTranslation GetMapObjectTranslations(string languageKey, int languageId);

        /// <summary>
        /// Gets the map object translations.
        /// </summary>
        /// <param name="cityNodeId">The city node identifier.</param>
        /// <returns>The list of <see cref="MapObjectTranslation"/>.</returns>
        List<MapObjectTranslationData> GetMapObjectTranslations(long cityNodeId);

        /// <summary>
        /// Gets all city nodes.
        /// </summary>
        /// <returns>The ICollection of <see cref="CityNode"/>.</returns>
        ICollection<CityNode> GetAllCityNodes();

        /// <summary>
        /// Gets the suggestion list.
        /// </summary>
        /// <param name="entries">The entries.</param>
        /// <param name="count">The count.</param>
        /// <returns>The ICollection of <see cref="MapObjectTranslation"/>.</returns>
        ICollection<MapObjectTranslation> GetSuggestionList(string entries, int count);

        /// <summary>
        /// Gets the static content by identifier and language.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="languageId">The language identifier.</param>
        /// <returns>The string value.</returns>
        string GetStaticContentByIdAndLang(string key, int languageId);

        /// <summary>
        /// Gets the city language key and region node.
        /// </summary>
        /// <param name="cityId">The city identifier.</param>
        /// <returns>Return key value pair with region id and lang key for city</returns>
        KeyValuePair<string, int> GetCityLangKeyAndRegionNode(int cityId);

        /// <summary>
        /// Gets the region language key by identifier.
        /// </summary>
        /// <param name="regionId">The region identifier.</param>
        /// <returns>Returen region lang key</returns>
        string GetRegionLangKeyById(int regionId);

        /// <summary>
        /// Gets the name of the regions by language.
        /// </summary>
        /// <param name="languageName">Name of the language.</param>
        /// <returns>The list of <see cref="ObjectTranslationsData"/>.</returns>
        List<ObjectTranslationsData> GetRegionsByLanguageName(string languageName);

        /// <summary>
        /// Updates the update map object.
        /// </summary>
        /// <param name="objectTranslationData">The object translation data.</param>
        void UpdateMapObject(MapObjectTranslationData objectTranslationData);

        /// <summary>
        /// Creates the map object.
        /// </summary>
        /// <param name="regionId">The region identifier.</param>
        /// <param name="languageKey">The language key.</param>
        /// <returns>Boolean result true if new object was created.</returns>
        bool CreateMapObject(int regionId, string languageKey);

        /// <summary>
        /// Creates the translation with city node.
        /// </summary>
        /// <param name="objectTranslationData">The object translation data.</param>
        void CreateTranslationWithCityNode(MapObjectTranslationData objectTranslationData);

        /// <summary>
        /// Creates the translation with region node.
        /// </summary>
        /// <param name="objectTranslationData">The object translation data.</param>
        void CreateTranslationWithRegionNode(MapObjectTranslationData objectTranslationData);

        /// <summary>
        /// Creates the map object translations.
        /// </summary>
        /// <param name="translations">The translations.</param>
        /// <param name="languageKey">The language key.</param>
        void CreateMapObjectTranslations(List<MapObjectTranslationData> translations, string languageKey);

        /// <summary>
        /// Gets the export data.
        /// </summary>
        /// <returns>The list of <see cref="MapObjectTranslationData"/>.</returns>
        List<MapObjectTranslationData> GetExportData();
    }
}