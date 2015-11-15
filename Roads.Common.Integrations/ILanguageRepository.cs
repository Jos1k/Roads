using System.Collections.Generic;
using Roads.Common.Models.DataContract;
using Roads.DataBase.Model.Models;

namespace Roads.Common.Integrations
{
    public interface ILanguageRepository:IRepositoryBase
    {
        /// <summary>
        /// Gets the Language object by id.
        /// </summary>
        /// <param name="id">The key.</param>
        /// <returns><see cref="Language"/> object by id.</returns>
        Language GetLanguageById(int id);

        /// <summary>
        /// Gets the language by key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns><see cref="Language"/> object by key.</returns>
        Language GetLanguageByKey(string key);

        /// <summary>
        /// Gets all languages names.
        /// </summary>
        /// <returns>Languages values list.</returns>
        List<string> GetAllLanguagesNames();

        /// <summary>
        /// Gets the default name of the language.
        /// </summary>
        /// <returns>Default language name.</returns>
        string GetDefaultLanguageName();

        /// <summary>
        /// Gets all languages data.
        /// </summary>
        /// <returns>List of all <see cref="LanguageData"/> items.</returns>
        List<LanguageData> GetAllLanguagesData();
    }
}
