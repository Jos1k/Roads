using System.Collections.Generic;
using Roads.Common.Models.DataContract;

namespace Roads.Common.Integrations
{
    public interface IStaticTranslationsRepository
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="languageName">Name of the language.</param>
        /// <returns>Specific value.</returns>
        string GetValue(string key, string languageName);

        /// <summary>
        /// Gets the static translation data.
        /// </summary>
        /// <param name="translationKey">The translation key.</param>
        /// <returns>The List of<see cref="StaticTranslationData"/>.</returns>
        List<StaticTranslationData> GetStaticTranslationData(string translationKey);

        /// <summary>
        /// Updates the specified list of static translations data.
        /// </summary>
        /// <param name="translations">The translations.</param>
        void Update(List<StaticTranslationData> translations);

        /// <summary>
        /// Gets all static translations.
        /// </summary>
        /// <returns>List of <see cref="StaticTranslationData"/></returns>
        List<StaticTranslationData> GetAllStaticTranslations();
    }
}
