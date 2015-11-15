using System;
using System.Collections.Generic;
using Roads.Common.Integrations;
using Roads.Common.Models.DataContract;
using Roads.Common.Repositories;

namespace Roads.Common.Managers
{
    public class MapObjectManager : IDisposable
    {
        #region Private fields

        private readonly IMapObjectTranslationsRepository _mapObjectTranslationsRepository;

        #endregion

        #region Constructor and Destructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SmartSuggestionsManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public MapObjectManager(IMapObjectTranslationsRepository repository)
        {
            _mapObjectTranslationsRepository = repository;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SmartSuggestionsManager"/> class.
        /// </summary>
        public MapObjectManager()
        {
            _mapObjectTranslationsRepository = new MapObjectTranslationsRepository();
        }

        #endregion

        #region Pablic Methods

        /// <summary>
        /// Gets the translations for.
        /// </summary>
        /// <param name="cityNodeId">The city node identifier.</param>
        /// <returns>The list of <see cref="MapObjectTranslationData"/>.</returns>
        public List<MapObjectTranslationData> GetTranslationsFor(long cityNodeId)
        {
            return _mapObjectTranslationsRepository.GetMapObjectTranslations(cityNodeId);
        }

        /// <summary>
        /// Updates the map object translation.
        /// </summary>
        /// <param name="translations">The translations.</param>
        public void UpdateMapObjectTranslation(List<MapObjectTranslationData> translations)
        {
            foreach (var translation in translations)
            {
                _mapObjectTranslationsRepository.UpdateMapObject(translation);
            }
        }

        /// <summary>
        /// Gets the regions for select list.
        /// </summary>
        /// <param name="languageName">Name of the language.</param>
        /// <returns>The list of <see cref="ObjectTranslationsData"/></returns>
        public List<ObjectTranslationsData> GetRegionsForSelectList(string languageName)
        {
            return _mapObjectTranslationsRepository.GetRegionsByLanguageName(languageName);
        }

        /// <summary>
        /// Creates the map object with translation.
        /// </summary>
        /// <param name="translations">The translations.</param>
        /// <param name="regionId">The region identifier.</param>
        /// <param name="languageKey">The language key.</param>
        public void CreateMapObjectWithTranslation(List<MapObjectTranslationData> translations, int regionId, string languageKey)
        {
            if (!string.IsNullOrEmpty(languageKey))
            {
                if (_mapObjectTranslationsRepository.CreateMapObject(regionId, languageKey))
                {
                    _mapObjectTranslationsRepository.CreateMapObjectTranslations(translations, languageKey);
                }
            }
        }

        /// <summary>
        /// Gets the export data.
        /// </summary>
        /// <returns>The list of <see cref="MapObjectTranslationData"/>.</returns>
        public List<MapObjectTranslationData> GetExportData()
        {
            return _mapObjectTranslationsRepository.GetExportData();
        }

        /// <summary>
        /// Processes the import map object.
        /// </summary>
        /// <param name="data">The data.</param>
        public void ProcessImportMapObject(List<MapObjectTranslationData> data)
        {
            foreach (var translation in data)
            {
                if (translation.CityNodeId >= 0 && translation.RegionNodeId > 0 && translation.MapObjectId > 0)
                {
                    _mapObjectTranslationsRepository.UpdateMapObject(translation);
                }
                else if (translation.CityNodeId == 0 && translation.RegionNodeId < 0 && translation.MapObjectId == 0)
                {
                    _mapObjectTranslationsRepository.CreateTranslationWithRegionNode(translation);
                }
                else if (translation.CityNodeId < 0 && translation.RegionNodeId > 0 && translation.MapObjectId == 0)
                {
                    _mapObjectTranslationsRepository.CreateTranslationWithCityNode(translation);
                }                
            }
        }

        #endregion

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            
        }
    }
}
