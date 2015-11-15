using System.Collections.Generic;
using System.Linq;
using Roads.Common.Integrations;
using Roads.Common.Models.DataContract;
using System.Data.Entity;

namespace Roads.Common.Repositories
{
    /// <summary>
    ///     The <see cref="Roads.Common.Repositories.StaticTranslationsRepository" /> class which provides StaticTranslations objects functionality..
    /// </summary>
    public class StaticTranslationsRepository : RepositoryBase, IStaticTranslationsRepository
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Roads.Common.Repositories.StaticTranslationsRepository" /> class.
        /// </summary>
        /// <param name="dataContext">DataContext object</param>
        public StaticTranslationsRepository(IDataContext dataContext) : base(dataContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Roads.Common.Repositories.StaticTranslationsRepository" /> class.
        /// </summary>
        public StaticTranslationsRepository() : base(new DataContext())
        {
        }

        #endregion

        #region Public methods

        /// <summary>
        ///     Gets the value.
        /// </summary>
        /// <param name="staticEnumKey">The static enum key.</param>
        /// <param name="languageName">Name of the language.</param>
        /// <returns>The value in specific language.</returns>
        public string GetValue(string staticEnumKey, string languageName)
        {
            return
                DataContext.StaticTranslations.Where(k => k.EnumKey == staticEnumKey && k.Language.Name == languageName)
                    .Select(v => v.Value).FirstOrDefault();
        }

        /// <summary>
        /// Gets the static translation data.
        /// </summary>
        /// <param name="translationKey">The translation key.</param>
        /// <returns>
        /// StaticTranslationData list.
        /// </returns>
        public List<StaticTranslationData> GetStaticTranslationData(string translationKey)
        {
            return DataContext.StaticTranslations
               .Where(e => e.EnumKey.Contains(translationKey) || e.Value.Contains(translationKey))
               .Select(s => new StaticTranslationData
               {
                   StaticTranslationId = s.StaticTranslationId,
                   LanguageId = s.LanguageId,
                   Value = s.Value,
                   EnumKey = s.EnumKey
               })
               .ToList();
        }

        /// <summary>
        /// Updates the specified list of static translations data.
        /// </summary>
        /// <param name="translations">The translations.</param>
        public void Update(List<StaticTranslationData> translations)
        {
            foreach (var t in translations)
            {
                var translation =
                    DataContext.StaticTranslations.FirstOrDefault(f => f.StaticTranslationId == t.StaticTranslationId);

                if (translation != null)
                {
                    translation.Value = t.Value;
                    translation.EnumKey = t.EnumKey;

                    DataContext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Gets all static translations.
        /// </summary>
        /// <returns>
        /// List of <see cref="StaticTranslationData" />
        /// </returns>
        public List<StaticTranslationData> GetAllStaticTranslations()
        {
            var allDynamicTranslations = DataContext.StaticTranslations.Include(x => x.Language).ToList();
            return allDynamicTranslations.Select(staticTranslation => new StaticTranslationData()
            {
                StaticTranslationId = staticTranslation.StaticTranslationId,
                EnumKey = staticTranslation.EnumKey,
                Value = staticTranslation.Value,
                LanguageId = staticTranslation.LanguageId,
                Language = new LanguageData()
                {
                    IsDefault = staticTranslation.Language.IsDefault,
                    LanguageId = staticTranslation.Language.LanguageId,
                    Name = staticTranslation.Language.Name
                }

            }).ToList();
        }
        #endregion
    }
}