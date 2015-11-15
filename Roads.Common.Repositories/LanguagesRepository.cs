using System.Collections.Generic;
using System.Linq;
using Roads.Common.Integrations;
using Roads.Common.Models.DataContract;
using Roads.DataBase.Model.Models;

namespace Roads.Common.Repositories
{
    /// <summary>
    /// The LanguagesRepository class.
    /// </summary>
    public class LanguagesRepository : RepositoryBase, ILanguageRepository
    {
        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="LanguagesRepository" /> class.</summary>
        /// <param name="dataContext">The data context.</param>
        public LanguagesRepository(IDataContext dataContext)
            : base(dataContext)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="LanguagesRepository" /> class.</summary>
        public LanguagesRepository()
            : base(new DataContext())
        {
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets the Language object by id.
        /// </summary>
        /// The <param name="id">.</param>
        /// <returns>
        /// <see cref="Language" /> object by id.
        /// </returns>
        public Language GetLanguageById(int id)
        {
            return DataContext.Languages.FirstOrDefault(k => k.LanguageId == id);
        }

        /// <summary>
        /// Gets the language by key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// <see cref="Language" /> object by key.
        /// </returns>
        public Language GetLanguageByKey(string key)
        {
            Language language = DataContext.Languages.FirstOrDefault(e => e.Name.ToLower() == key.ToLower());

            if (language == null)
            {
                return DataContext.Languages.FirstOrDefault(e => e.IsDefault);
            }

            return language;
        }

        /// <summary>
        /// Gets all languages names.
        /// </summary>
        /// <returns>
        /// Languages values list.
        /// </returns>
        public List<string> GetAllLanguagesNames()
        {
            return DataContext.Languages.Select(l => l.Name).ToList();
        }

        /// <summary>
        /// Gets the default name of the language.
        /// </summary>
        /// <returns>
        /// Default language name.
        /// </returns>
        public string GetDefaultLanguageName()
        {
            return DataContext.Languages.Where(l => l.IsDefault).Select(x => x.Name).FirstOrDefault();
        }

        /// <summary>
        /// Gets all languages data.
        /// </summary>
        /// <returns>
        /// List of all <see cref="LanguageData"/> items.
        /// </returns>
        public List<LanguageData> GetAllLanguagesData()
        {
            var languages = DataContext.Languages.ToList();
            return languages.Select(language => new LanguageData
            {
                LanguageId = language.LanguageId,
                IsDefault = language.IsDefault,
                Name = language.Name
            }).ToList();
        }

        #endregion
    }
}