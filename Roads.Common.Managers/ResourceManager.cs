using System;
using System.Collections.Generic;
using Roads.Common.Helpers;
using Roads.Common.Integrations;
using Roads.Common.Models.DataContract;
using Roads.Common.Models.Enums;
using Roads.Common.Repositories;

namespace Roads.Common.Managers
{
    /// <summary>
    /// Translation Resource Manager functionality.
    /// </summary>
    public class ResourceManager : IResourceManager
    {
        #region Private fields

        private readonly ICacheProvider _cacheProvider;
        private IDynamicTranslationsRepository _dynamicRepository;
        private IStaticTranslationsRepository _staticRepository;
        private List<string> _availableLanguages = new List<string>();
        private string _defaultLanguage;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceManager" /> class.
        /// </summary>
        /// <param name="cacheProvider">The cache provider.</param>
        public ResourceManager(ICacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
            InitializeLanguages();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceManager"/> class.
        /// </summary>
        public ResourceManager() :this(new CacheProvider.CacheProvider())
        {         
        }

        #endregion

        #region IResourceManager implementation

        /// <summary>
        /// Gets the resource.
        /// </summary>
        /// <param name="translationType">Type of the translation.</param>
        /// <param name="key">The specific key of content.</param>
        /// <param name="culture">The specific culture.</param>
        /// <returns>Specific resource value.</returns>
        public string GetResource(TranslationTypeEnum translationType, string key, string culture)
        {
            if (culture == null)
            {
                throw new ArgumentNullException("culture");
            }
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            var resHelpers = new ResourceManagerHelpers();

            //Normalize culture
            resHelpers.NormalizeCultureParameter(ref culture);

            //Validate culture
            if (!_availableLanguages.Contains(culture))
            {
                culture = _defaultLanguage;
            }

            string resource = _cacheProvider.Fetch(translationType, new[] {culture, key});

            //Check availability of translation. Tries to fetch data by passing default language if the culture is absent.
            var result = resource == String.Empty
                ? _cacheProvider.Fetch(translationType, new[] {_defaultLanguage, key})
                : resource;

            if (string.IsNullOrEmpty(result))
            {
                result = _staticRepository.GetValue(key, culture) ?? _staticRepository.GetValue(key, _defaultLanguage);
            }

            return result;
        }

        /// <summary>
        /// Adds the resource.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        public void AddResource<T>(T entity)
        {
            if (entity is List<DynamicTranslationsData>)
            {
                if (_dynamicRepository == null)
                {
                    _dynamicRepository = new DynamicTranslationsRepository();
                }
                _dynamicRepository.Add(entity as List<DynamicTranslationsData>);
            }
        }

        /// <summary>
        /// Updates the resource.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        public void UpdateResource<T>(T entity)
        {
            if (entity is List<DynamicTranslationsData>)
            {
                if (_dynamicRepository == null)
                {
                    _dynamicRepository = new DynamicTranslationsRepository();
                }
                _dynamicRepository.Update(entity as List<DynamicTranslationsData>);
                _cacheProvider.ResetCacheObject(TranslationTypeEnum.DynamicTranslation);
            }

            if (entity is List<StaticTranslationData>)
            {
                if (_staticRepository == null)
                {
                    _staticRepository = new StaticTranslationsRepository();
                }
                _staticRepository.Update(entity as List<StaticTranslationData>);
                _cacheProvider.ResetCacheObject(TranslationTypeEnum.StaticTranslation);
            }
        }

        /// <summary>
        /// Deletes the dynamic resource.
        /// </summary>
        /// <param name="dynamicTranslationsKeys">The dynamic translations keys.</param>
        public void DeleteDynamicResource(List<string> dynamicTranslationsKeys)
        {
                if (_dynamicRepository == null)
                {
                    _dynamicRepository = new DynamicTranslationsRepository();
                }
                _dynamicRepository.Delete(dynamicTranslationsKeys);
                _cacheProvider.ResetCacheObject(TranslationTypeEnum.DynamicTranslation);
        }

        /// <summary>
        /// Gets all dynamic translations data.
        /// </summary>
        /// <returns>
        /// List of all dynamicTranslationsData items.
        /// </returns>
        public List<DynamicTranslationsData> GetAllDynamicTranslationsData()
        {
            if (_dynamicRepository == null)
            {
                _dynamicRepository = new DynamicTranslationsRepository();
            }
            return _dynamicRepository.GetAllDynamicTranslations();
        }

        /// <summary>
        /// Gets the dynamic translation data.
        /// </summary>
        /// <param name="translationKey">The translation key.</param>
        /// <returns></returns>
        public List<DynamicTranslationsData> GetDynamicTranslationsData(string translationKey)
        {
            if (_dynamicRepository == null)
            {
                _dynamicRepository = new DynamicTranslationsRepository();
            }
            return _dynamicRepository.GetDynamicTranslationsData(translationKey);
        }

        /// <summary>
        /// Gets all static translations data.
        /// </summary>
        /// <returns>List of <see cref="StaticTranslationData"/></returns>
        public List<StaticTranslationData> GetAllStaticTranslationsData()
        {
            if (_staticRepository == null)
            {
                _staticRepository = new StaticTranslationsRepository();
            }
            return _staticRepository.GetAllStaticTranslations();
        }

        /// <summary>
        /// Gets the static translation data list.
        /// </summary>
        /// <param name="translationKey">The translation key.</param>
        /// <returns>List of <see cref="StaticTranslationData"/></returns>
        public List<StaticTranslationData> GetStaticTranslationData(string translationKey)
        {
            if (_staticRepository == null)
            {
                _staticRepository = new StaticTranslationsRepository();
            }
            return _staticRepository.GetStaticTranslationData(translationKey);
        }

        /// <summary>
        /// Gets the available languages names.
        /// </summary>
        /// <returns>
        /// The list of available languages names.
        /// </returns>
        public List<string> GetLanguages()
        {
            return _availableLanguages;
        }

        /// <summary>
        /// Gets the default language.
        /// </summary>
        /// <returns>
        /// Value of the default language.
        /// </returns>
        public string GetDefaultLanguage()
        {
            return _defaultLanguage;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Initializes the languages.
        /// </summary>
        private void InitializeLanguages()
        {
            var languagesRepository = new LanguagesRepository();
            _availableLanguages = languagesRepository.GetAllLanguagesNames();
            _defaultLanguage = languagesRepository.GetDefaultLanguageName();
        }

        #endregion
    }
}