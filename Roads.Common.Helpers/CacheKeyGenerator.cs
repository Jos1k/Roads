using System;

namespace Roads.Common.Helpers
{
    public class CacheKeyGenerator
    {
        #region Private fields and constants

        private const string Separator = "_";

        #endregion

        #region Public Methods

        /// <summary>
        /// Composes the key.
        /// </summary>
        /// <param name="keys">Array of strings keys.</param>
        /// <returns>Compose key.</returns>
        public static string ComposeKey(string[] keys)
        {
            if (keys == null)
            {
                return String.Empty;
            }
            return String.Join(Separator, keys);
        }

        /// <summary>
        /// Decomposes the key.
        /// </summary>
        /// <param name="cacheKey">The cache key.</param>
        /// <returns>Decompose key as array of strings.</returns>
        public static string[] DecomposeKey(string cacheKey)
        {
            return cacheKey.Split(new[] {Separator}, StringSplitOptions.RemoveEmptyEntries);
        }

        #endregion
    }
}