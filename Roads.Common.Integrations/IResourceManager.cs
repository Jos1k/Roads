using System.Collections.Generic;
using Roads.Common.Models.DataContract;
using Roads.Common.Models.Enums;

namespace Roads.Common.Integrations
{
    public interface IResourceManager
    {
        /// <summary>
        /// Gets the resource.
        /// </summary>
        /// <param name="translationType">Type of the translation.</param>
        /// <param name="key">The key of item.</param>
        /// <param name="culture">The culture of item.</param>
        /// <returns>The specific resource value.</returns>
        string GetResource(TranslationTypeEnum translationType, string key, string culture);

        /// <summary>
        /// Adds the resource.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The DataContract entity.</param>
        void AddResource<T>(T entity);

        /// <summary>
        /// Updates the resource.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The DataContract entity.</param>
        void UpdateResource<T>(T entity);

        /// <summary>
        /// Deletes the dynamic resource.
        /// </summary>
        /// <param name="dynamicTranslationsKeys">The dynamic translations keys.</param>
        void DeleteDynamicResource(List<string> dynamicTranslationsKeys);

        /// <summary>
        /// Gets the available languages.
        /// </summary>
        /// <returns>The List of available languages names.</returns>
        List<string> GetLanguages();

        /// <summary>
        /// Gets the default language.
        /// </summary>
        /// <returns>Value of the default language.</returns>
        string GetDefaultLanguage();

        /// <summary>
        /// Gets all dynamic translations data.
        /// </summary>
        /// <returns>
        /// List of all <see cref="DynamicTranslationsData"/> items.
        /// </returns>
        List<DynamicTranslationsData> GetAllDynamicTranslationsData();

        /// <summary>
        /// Gets the dynamic translations data list.
        /// </summary>
        /// <param name="translationKey">The translation key.</param>
        /// <returns>The List of <see cref="DynamicTranslationsData"/>.</returns>
        List<DynamicTranslationsData> GetDynamicTranslationsData(string translationKey);

        /// <summary>
        /// Gets the static translation data list.
        /// </summary>
        /// <param name="translationKey">The translation key.</param>
        /// <returns>The List of <see cref="StaticTranslationData"/></returns>
        List<StaticTranslationData> GetStaticTranslationData(string translationKey);

        /// <summary>
        /// Gets all static translations data.
        /// </summary>
        /// <returns>List of all <see cref="StaticTranslationData"/></returns>
        List<StaticTranslationData> GetAllStaticTranslationsData();
    }
}