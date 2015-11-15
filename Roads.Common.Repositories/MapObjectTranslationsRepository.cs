using System;
using System.Linq;
using Roads.Common.Integrations;
using Roads.Common.Models.DataContract;
using Roads.DataBase.Model.Models;
using System.Collections.Generic;

namespace Roads.Common.Repositories
{
    /// <summary>
    /// The <see cref="MapObjectTranslationsRepository" /> class which provides MapObjectTranslations objects functionality.
    /// </summary>
    public class MapObjectTranslationsRepository : RepositoryBase, IMapObjectTranslationsRepository
    {
        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="LanguagesRepository" /> class.</summary>
        /// <param name="dataContext">The data context.</param>
        public MapObjectTranslationsRepository(IDataContext dataContext)
            : base(dataContext)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="LanguagesRepository" /> class.</summary>
        public MapObjectTranslationsRepository()
            : base(new DataContext())
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="languageKey">The key.</param>
        /// <param name="languageName">Name of the language.</param>
        /// <returns>The value in specific language.</returns>
        public string GetValue(string languageKey, string languageName)
        {
            return
                DataContext.MapObjectTranslations.Where(
                    k => k.LanguageKey == languageKey && k.Language.Name == languageName)
                    .Select(v => v.Value).FirstOrDefault();
        }

        /// <summary>
        /// Gets the region node by identifier.
        /// </summary>
        /// <param name="regionNodeId">The region node identifier.</param>
        /// <returns>The region node</returns>
        public RegionNode GetRegionNode(int regionNodeId)
        {
            RegionNode regionNode = DataContext.RegionNodes.FirstOrDefault(key => key.RegionNodeId == regionNodeId);
            return regionNode;
        }

        /// <summary>
        /// Gets the map object translations.
        /// </summary>
        /// <param name="languageKey">The language key.</param>
        /// <param name="languageId">The language identifier.</param>
        /// <returns>The map Object translations</returns>
        public MapObjectTranslation GetMapObjectTranslations(string languageKey, int languageId)
        {
            MapObjectTranslation mapObjectTranslation = DataContext.MapObjectTranslations.FirstOrDefault(
                key => key.LanguageKey == languageKey && key.LanguageId == languageId);
            return mapObjectTranslation;
        }

        /// <summary>
        /// Gets the map object translations.
        /// </summary>
        /// <param name="cityNodeId">The city node identifier.</param>
        /// <returns>The list of <see cref="MapObjectTranslation"/>.</returns>
        public List<MapObjectTranslationData> GetMapObjectTranslations(long cityNodeId)
        {
            var translationKey = DataContext.CityNodes.First(e => e.CityNodeId == cityNodeId).LanguageKey;

            return DataContext.MapObjectTranslations.Where(w => w.LanguageKey == translationKey).Select(s => new MapObjectTranslationData
            {
                LanguageId = s.LanguageId,
                LanguageKey = s.LanguageKey,
                Value = s.Value,
                MapObjectId = s.MapObjectId
            }).ToList();
        }

        /// <summary>
        /// Gets all city nodes.
        /// </summary>
        /// <returns>The all cities</returns>
        public ICollection<CityNode> GetAllCityNodes()
        {
            return DataContext.CityNodes.Select(x => x).ToList();
        }

        /// <summary>
        /// Gets the suggestion list.
        /// </summary>
        /// <param name="entries">The entries.</param>
        /// <param name="count">The count.</param>
        /// <returns>The list with suggestions</returns>
        public ICollection<MapObjectTranslation> GetSuggestionList(string entries, int count)
        {
            return DataContext.MapObjectTranslations.Where(e => e.Value.StartsWith(entries)).Take(count).ToList();
        }

        /// <summary>
        /// Gets the static content by identifier and language.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="languageId">The language identifier.</param>
        /// <returns>Translation of static content</returns>
        /// <exception cref="System.NullReferenceException">StaticContent</exception>
        public string GetStaticContentByIdAndLang(string key, int languageId)
        {
            StaticTranslation staticContent = DataContext.StaticTranslations.FirstOrDefault(e => e.EnumKey.ToLower() == key.ToLower() && e.LanguageId == languageId);

            if (staticContent == null)
            {
                //TODO:Catch this exception
                throw new NullReferenceException("StaticContent");
            }
            return staticContent.Value;
        }

        /// <summary>
        /// Gets the city language key and region node.
        /// </summary>
        /// <param name="cityId">The city identifier.</param>
        /// <returns>Return language key for city node and get region node id </returns>
        public KeyValuePair<string, int> GetCityLangKeyAndRegionNode(int cityId)
        {
            CityNode resultCityNode = DataContext.CityNodes.FirstOrDefault(cityNode => cityNode.CityNodeId == cityId);
            return new KeyValuePair<string, int>(resultCityNode.LanguageKey, resultCityNode.RegionNodeId);
        }

        /// <summary>
        /// Gets the region language key by identifier.
        /// </summary>
        /// <param name="regionId">The region identifier.</param>
        /// <returns>Return language key for region</returns>
        public string GetRegionLangKeyById(int regionId)
        {
            return DataContext.RegionNodes
                .FirstOrDefault(regionNode => regionNode.RegionNodeId == regionId).LanguageKey;
        }

        /// <summary>
        /// Gets the name of the regions by language.
        /// </summary>
        /// <param name="languageName">Name of the language.</param>
        /// <returns>The list of <see cref="ObjectTranslationsData"/>.</returns>
        public List<ObjectTranslationsData> GetRegionsByLanguageName(string languageName)
        {
            var language = DataContext.Languages.FirstOrDefault(e => e.Name == languageName);

            if (language == null)
            {
                language = DataContext.Languages.FirstOrDefault(e => e.IsDefault);
            }

            var response = new List<ObjectTranslationsData>();

            foreach (var region in DataContext.RegionNodes.ToList())
            {
                var translation = DataContext.MapObjectTranslations.FirstOrDefault(e => e.LanguageId == language.LanguageId && e.LanguageKey == region.LanguageKey) ??
                    DataContext.MapObjectTranslations.FirstOrDefault(e => e.LanguageKey == region.LanguageKey);

                if (translation != null)
                {
                    response.Add(new ObjectTranslationsData
                    {
                        Id = region.RegionNodeId,
                        Name = translation.Value
                    });
                }
            }

            return response;
        }

        /// <summary>
        /// Updates the update map object.
        /// </summary>
        /// <param name="objectTranslationData">The object translation data.</param>
        public void UpdateMapObject(MapObjectTranslationData objectTranslationData)
        {
            var translation =
                DataContext.MapObjectTranslations.FirstOrDefault(f => f.MapObjectId == objectTranslationData.MapObjectId);

            if (translation != null)
            {
                translation.Value = objectTranslationData.Value;

                translation.LanguageId = objectTranslationData.LanguageId;

                DataContext.SaveChanges();
            }
        }

        /// <summary>
        /// Creates the map object.
        /// </summary>
        /// <param name="regionId">The region identifier.</param>
        /// <param name="languageKey">The language key.</param>
        /// <returns>
        /// Boolean result true if new object was created.
        /// </returns>
        public bool CreateMapObject(int regionId, string languageKey)
        {
            if (string.IsNullOrEmpty(languageKey))
            {
                return false;
            }

            if (regionId != 0)
            {
                var cityNode = new CityNode
                {
                    LanguageKey = languageKey,
                    RegionNodeId = regionId
                };

                DataContext.AddDataObject(cityNode);
            }
            else
            {
                var regionNode = new RegionNode
                {
                    LanguageKey = languageKey
                };

                DataContext.AddDataObject(regionNode);
            }

            try
            {
                DataContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Creates the translation with city node.
        /// </summary>
        /// <param name="objectTranslationData">The object translation data.</param>
        public void CreateTranslationWithCityNode(MapObjectTranslationData objectTranslationData)
        {
            if (objectTranslationData.RegionNodeId <= 0)
            {
                return;
            }

            if (!DataContext.CityNodes.Any(e => e.LanguageKey == objectTranslationData.LanguageKey))
            {
                var newCity = new CityNode
                {
                    RegionNodeId = (int)objectTranslationData.RegionNodeId,
                    LanguageKey = objectTranslationData.LanguageKey,
                };

                DataContext.AddDataObject(newCity);

                DataContext.SaveChanges();
            }

            var newTranslation = new MapObjectTranslation
            {
                LanguageKey = objectTranslationData.LanguageKey,
                LanguageId = objectTranslationData.LanguageId,
                Value = objectTranslationData.Value
            };

            DataContext.AddTranslationObject(newTranslation);

            DataContext.SaveChanges();

        }

        /// <summary>
        /// Creates the translation with region node.
        /// </summary>
        /// <param name="objectTranslationData">The object translation data.</param>
        public void CreateTranslationWithRegionNode(MapObjectTranslationData objectTranslationData)
        {
            if (objectTranslationData.CityNodeId != 0)
            {
                return;
            }

            if (!DataContext.RegionNodes.Any(e => e.LanguageKey == objectTranslationData.LanguageKey))
            {
                var newCity = new RegionNode
                {
                    LanguageKey = objectTranslationData.LanguageKey
                };

                DataContext.AddDataObject(newCity);

                DataContext.SaveChanges();
            }

            var newTranslation = new MapObjectTranslation
            {
                LanguageKey = objectTranslationData.LanguageKey,
                LanguageId = objectTranslationData.LanguageId,
                Value = objectTranslationData.Value
            };

            DataContext.AddTranslationObject(newTranslation);

            DataContext.SaveChanges();

        }

        /// <summary>
        /// Creates the map object translations.
        /// </summary>
        /// <param name="translations">The translations.</param>
        /// <param name="languageKey">The language key.</param>
        public void CreateMapObjectTranslations(List<MapObjectTranslationData> translations, string languageKey)
        {
            foreach (var translarion in translations)
            {
                DataContext.AddTranslationObject(new MapObjectTranslation
                {
                    LanguageId = translarion.LanguageId,
                    LanguageKey = languageKey,
                    Value = translarion.Value
                });
            }

            DataContext.SaveChanges();
        }

        /// <summary>
        /// Gets the export data.
        /// </summary>
        /// <returns>
        /// The list of <see cref="MapObjectTranslationData" />.
        /// </returns>
        public List<MapObjectTranslationData> GetExportData()
        {
            var resultlist = new List<MapObjectTranslationData>();

            var translations = DataContext.MapObjectTranslations.ToList();

            foreach (var region in DataContext.RegionNodes.ToList())
            {
                resultlist.AddRange(translations.Where(e => e.LanguageKey == region.LanguageKey)
                    .Select(s => new MapObjectTranslationData
                    {
                        MapObjectId = s.MapObjectId,
                        LanguageKey = s.LanguageKey,
                        Value = s.Value,
                        LanguageId = s.LanguageId,
                        RegionNodeId = region.RegionNodeId,
                        CityNodeId = 0
                    }).ToList());

            }

            foreach (var city in DataContext.CityNodes.ToList())
            {
                resultlist.AddRange(translations.Where(e => e.LanguageKey == city.LanguageKey)
                    .Select(s => new MapObjectTranslationData
                    {
                        MapObjectId = s.MapObjectId,
                        LanguageKey = s.LanguageKey,
                        Value = s.Value,
                        LanguageId = s.LanguageId,
                        RegionNodeId = city.RegionNodeId,
                        CityNodeId = city.CityNodeId
                    }).ToList());
            }

            return resultlist;
        }

        #endregion
    }
}