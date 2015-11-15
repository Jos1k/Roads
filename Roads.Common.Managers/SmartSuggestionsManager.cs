using System.Collections.Generic;
using System.Linq;
using System.Text;
using Roads.Common.Integrations;
using Roads.Common.Models;
using Roads.Common.Repositories;
using Roads.DataBase.Model.Models;

namespace Roads.Common.Managers
{
    /// <summary>
    /// The SmartSuggestionsManager for Smart Suggestions functionality.
    /// </summary>
    public class SmartSuggestionsManager
    {
        #region Private fields

        private readonly IMapObjectTranslationsRepository _mapObjectTranslationsRepository;

        #endregion

        #region Constructor and Destructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SmartSuggestionsManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public SmartSuggestionsManager(IMapObjectTranslationsRepository repository)
        {
            _mapObjectTranslationsRepository = repository;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SmartSuggestionsManager"/> class.
        /// </summary>
        public SmartSuggestionsManager()
        {
            _mapObjectTranslationsRepository = new MapObjectTranslationsRepository();
        }

        #endregion

        #region Public Methods

        /// <summary>  Gets the suggestions.  </summary>
        /// <param name="entries">The entry.</param>
        /// <param name="languageId">The language identifier.</param>
        /// <param name="count">The count of results.</param>
        /// <returns> The list of <see cref="Suggestion"/>.</returns>
        public List<Suggestion> GetSuggestions(string entries, int languageId, int count)
        {
            var result = new List<Suggestion>();

            if (string.IsNullOrEmpty(entries))
            {
                return new List<Suggestion>();
            }

            entries = entries.Trim();

            var sug = _mapObjectTranslationsRepository.GetSuggestionList(entries, count);

            var suggestions = sug
                .Join(_mapObjectTranslationsRepository.GetAllCityNodes(),
                    translations => translations.LanguageKey,
                    cityNode => cityNode.LanguageKey,
                    (translations, cityNode) => new { translations, cityNode }).ToList();


            var cityes = suggestions.GroupBy(g => g.cityNode).Select(s => s.Key).ToList();

            foreach (var city in cityes)
            {
                var suggestion =
                    suggestions.FirstOrDefault(f => f.cityNode == city && f.translations.LanguageId == languageId);

                if (suggestion != null)
                {
                    result.Add(GetPreparedSuggestion(suggestion.cityNode, suggestion.translations.Value, languageId));
                }
                else
                {
                    suggestion = suggestions.FirstOrDefault(f => f.cityNode == city);

                    if (suggestion != null)
                    {
                        result.Add(GetPreparedSuggestion(suggestion.cityNode, suggestion.translations.Value,
                            suggestion.translations.LanguageId));
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the full translation for city by identifier.
        /// </summary>
        /// <param name="cityId">The city identifier.</param>
        /// <param name="language">The language.</param>
        /// <returns>Return full translation of city in format: City, Region</returns>
        public string GetFullTranslationForCityById(int cityId, string language)
        {
            if (!new ResourceManager().GetLanguages().Exists(x => x == language))
            {
                language = new ResourceManager().GetDefaultLanguage();
            }
            

            KeyValuePair<string, int> cityLanguageKeyAndRegionId =
                _mapObjectTranslationsRepository.GetCityLangKeyAndRegionNode(cityId);

            string regionLangKey = _mapObjectTranslationsRepository.GetRegionLangKeyById(cityLanguageKeyAndRegionId.Value);

            return new StringBuilder()
                .Append(_mapObjectTranslationsRepository.GetValue(cityLanguageKeyAndRegionId.Key, language))
                .Append(", ")
                .Append(_mapObjectTranslationsRepository.GetValue(regionLangKey, language)).ToString();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the prepared suggestion.
        /// </summary>
        /// <param name="city">The city.</param>
        /// <param name="translation">The translation.</param>
        /// <param name="languageId">The language identifier.</param>
        /// <returns>The <see cref="Suggestion"/>.</returns>
        private Suggestion GetPreparedSuggestion(CityNode city, string translation, int languageId)
        {
            var regionName = string.Empty;

            var region = _mapObjectTranslationsRepository.GetRegionNode(city.RegionNodeId);

            if (region != null)
            {
                var regionTranslation = _mapObjectTranslationsRepository.GetMapObjectTranslations(region.LanguageKey, languageId);
                if (regionTranslation != null)
                {
                    regionName = string.Format(", {0}", regionTranslation.Value);
                }
            }

            return new Suggestion(translation, regionName, city.CityNodeId);
        }

        #endregion
    }
}
