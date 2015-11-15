using System;
using System.Runtime.Caching;
using Roads.Common.Helpers;
using Roads.Common.Integrations;
using Roads.Common.Models.Enums;
using Roads.Common.Repositories;

namespace Roads.Common.CacheProvider
{
    /// <summary>
    /// CacheProvider functionality.
    /// </summary>
    public class CacheProvider : ICacheProvider
    {
        #region Private fields

        private static readonly DateTimeOffset _absoluteExpiration = DateTimeOffset.MaxValue;
        private static readonly TimeSpan _slidingExpiration = TimeSpan.FromDays(5);

        private ObjectCache _staticObjectCache;
        private ObjectCache _dynamicObjectCache;
        private ObjectCache _mapObjectCache;

        private readonly IDynamicTranslationsRepository _dynamicTranslationsRepository;
        private readonly IMapObjectTranslationsRepository _mapObjectTranslationsRepository;
        private readonly CacheItemPolicy _policy;
        private readonly IStaticTranslationsRepository _staticTranslationsRepository;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheProvider" /> class.
        /// </summary>
        public CacheProvider()
            : this(
                new StaticTranslationsRepository(), new DynamicTranslationsRepository(),
                new MapObjectTranslationsRepository(), null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheProvider" /> class.
        /// </summary>
        /// <param name="policy">The policy.</param>
        public CacheProvider(CacheItemPolicy policy)
            : this(
                new StaticTranslationsRepository(), new DynamicTranslationsRepository(),
                new MapObjectTranslationsRepository(), policy)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheProvider" /> class.
        /// </summary>
        /// <param name="staticTranslationsRepo">The static translations repo.</param>
        /// <param name="dynamicTranslationsRepo">The dynamic translations repo.</param>
        /// <param name="mapObjectTranslationsRepo">The map object translations repo.</param>
        /// <param name="policy">The policy.</param>
        /// <exception cref="System.ArgumentNullException">policy</exception>
        public CacheProvider(IStaticTranslationsRepository staticTranslationsRepo,
            IDynamicTranslationsRepository dynamicTranslationsRepo,
            IMapObjectTranslationsRepository mapObjectTranslationsRepo, CacheItemPolicy policy)
        {
            if (policy == null)
            {
                policy = new CacheItemPolicy
                {
                    AbsoluteExpiration = _absoluteExpiration,
                    SlidingExpiration = _slidingExpiration
                };
            }

            _staticObjectCache = new MemoryCache("static");
            _dynamicObjectCache = new MemoryCache("dynamic");
            _mapObjectCache = new MemoryCache("map");

            _policy = policy;
            _staticTranslationsRepository = staticTranslationsRepo;
            _dynamicTranslationsRepository = dynamicTranslationsRepo;
            _mapObjectTranslationsRepository = mapObjectTranslationsRepo;
        }

        #endregion

        #region ICacheProvider Implementation

        /// <summary>
        /// Fetches the specified translation type.
        /// </summary>
        /// <param name="translationType">Type of the translation.</param>
        /// <param name="cacheKey">The cache key.</param>
        /// <returns>
        /// The data through the cache.
        /// </returns>
        public string Fetch(TranslationTypeEnum translationType, string cacheKey)
        {
            return FetchAndCache(translationType, cacheKey);
        }

        /// <summary>
        /// Fetches the specified translation type.
        /// </summary>
        /// <param name="translationType">Type of the translation.</param>
        /// <param name="complexCacheKey">The complex cache key.</param>
        /// <returns>
        /// The data through the cache.
        /// </returns>
        public string Fetch(TranslationTypeEnum translationType, string[] complexCacheKey)
        {
            return FetchAndCache(translationType, CacheKeyGenerator.ComposeKey(complexCacheKey));
        }

        /// <summary>
        /// Resets the cache object.
        /// </summary>
        /// <param name="translationType">Type of the translation data.</param>
        public void ResetCacheObject(TranslationTypeEnum translationType)
        {
            switch (translationType)
            {
                case TranslationTypeEnum.StaticTranslation:
                    _staticObjectCache = new MemoryCache("static");
                    break;
                case TranslationTypeEnum.DynamicTranslation:
                    _dynamicObjectCache = new MemoryCache("dynamic");
                    break;
                case TranslationTypeEnum.MapObjectTranslation:
                    _mapObjectCache = new MemoryCache("map");
                    break;
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Fetches and cache data.
        /// </summary>
        /// <param name="translationType">Type of the translation.</param>
        /// <param name="cacheKey">The cache key.</param>
        /// <returns>
        /// The data through the cache.
        /// </returns>
        private string FetchAndCache(TranslationTypeEnum translationType, string cacheKey)
        {
            if (cacheKey.Length < 3)
            {
                return String.Empty;
            }
            string value;

            if (!TryGetValue(cacheKey, out value, translationType))
            {
                string culture = cacheKey.Substring(0, 2);
                string dbKey = cacheKey.Substring(3);

                switch (translationType)
                {
                    case TranslationTypeEnum.StaticTranslation:
                        value = _staticTranslationsRepository.GetValue(dbKey, culture);
                        break;
                    case TranslationTypeEnum.DynamicTranslation:
                        value = _dynamicTranslationsRepository.GetValue(dbKey, culture);
                        break;
                    case TranslationTypeEnum.MapObjectTranslation:
                        value = _mapObjectTranslationsRepository.GetValue(dbKey, culture);
                        
                        break;
                }

                if (value == null)
                {
                    value = String.Empty;
                }

                //Add data to the cache
                switch (translationType)
                {
                    case TranslationTypeEnum.StaticTranslation:
                        _staticObjectCache.Add(cacheKey, value, _policy);
                        break;
                    case TranslationTypeEnum.DynamicTranslation:
                        _dynamicObjectCache.Add(cacheKey, value, _policy);
                        break;
                    case TranslationTypeEnum.MapObjectTranslation:
                        _mapObjectCache.Add(cacheKey, value, _policy);
                        break;
                }
            }

            return value;
        }

        /// <summary>
        /// Tries to get value from cache.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="translationType"></param>
        /// <returns>
        /// The result of trying to get value from cache.
        /// </returns>
        private bool TryGetValue(string key, out string value, TranslationTypeEnum translationType)
        {
            var cachedValue = new object();
            switch (translationType)
            {
                case TranslationTypeEnum.StaticTranslation:
                    cachedValue = _staticObjectCache.Get(key);
                    break;
                case TranslationTypeEnum.DynamicTranslation:
                    cachedValue = _dynamicObjectCache.Get(key);
                    break;
                case TranslationTypeEnum.MapObjectTranslation:
                    cachedValue = _mapObjectCache.Get(key);
                    break;
            }

            value = cachedValue as String;
            if (value != null)
            {
                return true;
            }
            value = String.Empty;
            return false;
        }

        #endregion
    }
}