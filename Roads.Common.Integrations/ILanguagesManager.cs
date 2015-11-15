using System.Collections.Generic;
using Roads.Common.Models.DataContract;

namespace Roads.Common.Integrations
{
    public interface ILanguagesManager
    {
        /// <summary>
        /// Gets the language identifier.
        /// </summary>
        /// <param name="key">The language key.</param>
        /// <returns>Language Id</returns>
        int GetLanguageId(string key);

        /// <summary>
        /// Gets all languages data.
        /// </summary>
        /// <returns>List of all <see cref="LanguageData"/> items.</returns>
        List<LanguageData> GetAllLanguagesData();
    }
}