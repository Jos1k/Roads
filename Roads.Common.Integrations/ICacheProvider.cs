using Roads.Common.Models.Enums;

namespace Roads.Common.Integrations
{
    public interface ICacheProvider
    {
        /// <summary>
        /// Fetches the specified translation type.
        /// </summary>
        /// <param name="translationType">Type of the translation.</param>
        /// <param name="cacheKey">The cache key.</param>
        /// <returns>Translation literal.</returns>
        string Fetch(TranslationTypeEnum translationType, string cacheKey);

        /// <summary>
        /// Fetches the specified translation type.
        /// </summary>
        /// <param name="translationType">Type of the translation.</param>
        /// <param name="complexCachekey">The complex cachekey.</param>
        /// <returns>Translation literal.</returns>
        string Fetch(TranslationTypeEnum translationType, string[] complexCachekey);

        /// <summary>
        /// Resets the specific cache object.
        /// </summary>
        /// <param name="translationType">Type of the translation data.</param>
        void ResetCacheObject(TranslationTypeEnum translationType);
    }
}