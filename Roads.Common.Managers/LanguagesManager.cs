using System.Collections.Generic;
using Roads.Common.Integrations;
using Roads.Common.Models.DataContract;
using Roads.Common.Repositories;
using Roads.DataBase.Model.Models;

namespace Roads.Common.Managers
{
    public class LanguagesManager: ILanguagesManager
    {
        #region Private Properties

        /// <summary>
        /// The languages repository
        /// </summary>
        private readonly ILanguageRepository _languagesRepository;

        #endregion

        #region Constructor and Destructor

        /// <summary>
        /// Initializes a new instance of the <see cref="LanguagesManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public LanguagesManager(ILanguageRepository repository)
        {
            _languagesRepository = repository;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LanguagesManager"/> class.
        /// </summary>
        public LanguagesManager()
        {
            _languagesRepository = new LanguagesRepository();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the language identifier.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>Language Id</returns>
        public int GetLanguageId(string key)
        {
            Language language = _languagesRepository.GetLanguageByKey(key);
            return language.LanguageId;
        }

        /// <summary>
        /// Gets all languages data.
        /// </summary>
        /// <returns>List of all <see cref="LanguageData"/> items.</returns>
        public List<LanguageData> GetAllLanguagesData()
        {
            return _languagesRepository.GetAllLanguagesData();
        }

        #endregion
    }
}
