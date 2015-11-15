using System.Collections.Generic;
using Roads.Common.Models.DataContract;

namespace Roads.Common.Integrations
{
    public interface IDynamicTranslationsRepository
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="languageName">Name of the language.</param>
        /// <returns>Specific value.</returns>
        string GetValue(string key, string languageName);

        /// <summary>
        /// Adds the specified dynamic entity.
        /// </summary>
        /// <param name="dynamicEntity">The dynamic DataContract entity.</param>
        void Add(DynamicTranslationsData dynamicEntity);

        /// <summary>
        /// Adds the list of dynamic entities.
        /// </summary>
        /// <param name="dynamicEntities">The dynamic entities.</param>
        void Add(List<DynamicTranslationsData> dynamicEntities);

        /// <summary>
        /// Updates the specified dynamic entity.
        /// </summary>
        /// <param name="dynamicEntity">The dynamic DataContract entity.</param>
        void Update(DynamicTranslationsData dynamicEntity);

        /// <summary>
        /// Updates the list of dynamic entities.
        /// </summary>
        /// <param name="dynamicEntities">The dynamic entities.</param>
        void Update(List<DynamicTranslationsData> dynamicEntities);

        /// <summary>
        /// Deletes the specified dynamic entity.
        /// </summary>
        /// <param name="dynamicEntity">The dynamic DataContract entity.</param>
        void Delete(DynamicTranslationsData dynamicEntity);

        /// <summary>
        /// Deletes the list of dynamic entities by keys.
        /// </summary>
        /// <param name="dynamicKeys">The dynamic keys list.</param>
        void Delete(List<string> dynamicKeys);

        /// <summary>
        /// Gets all dynamic translations.
        /// </summary>
        /// <returns>List of all <see cref="DynamicTranslationsData"/> items.</returns>
        List<DynamicTranslationsData> GetAllDynamicTranslations();

        /// <summary>
        /// Gets the dynamic translations data.
        /// </summary>
        /// <param name="translationKey">The translation key.</param>
        /// <returns>List of <see cref="DynamicTranslationsData"/> items.</returns>
        List<DynamicTranslationsData> GetDynamicTranslationsData(string translationKey);
    }
}