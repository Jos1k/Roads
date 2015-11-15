using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Roads.Common.Integrations;
using Roads.Common.Models.DataContract;
using Roads.DataBase.Model.Models;

namespace Roads.Common.Repositories
{
    /// <summary>
    ///  The <see cref="DynamicTranslationsRepository" /> class which provides DynamicTranslations objects functionality.
    /// </summary>
    public class DynamicTranslationsRepository : RepositoryBase, IDynamicTranslationsRepository
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicTranslationsRepository" /> class.
        /// </summary>
        /// <param name="dataContext">DataContext object</param>
        public DynamicTranslationsRepository(IDataContext dataContext) : base(dataContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicTranslationsRepository" /> class.
        /// </summary>
        public DynamicTranslationsRepository() : base(new DataContext())
        {
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets the value of DynamicTranslation.
        /// </summary>
        /// <param name="dynamicKey">The dynamic key.</param>
        /// <param name="languageName">Name of the language.</param>
        /// <returns>The value in specific language.</returns>
        public string GetValue(string dynamicKey, string languageName)
        {
            return
                DataContext.DynamicTranslations.Where(k => k.DynamicKey == dynamicKey && k.Language.Name == languageName)
                    .Select(v => v.Value).FirstOrDefault();
        }

        /// <summary>
        /// Adds the specified dynamic entity.
        /// </summary>
        /// <param name="dynamicEntity">The dynamic DataContract entity.</param>
        public void Add(DynamicTranslationsData dynamicEntity)
        {
            var dynamicTranslation = new DynamicTranslations
            {
                DescriptionValue = dynamicEntity.DescriptionValue,
                DynamicKey = dynamicEntity.DynamicKey,
                LanguageId = dynamicEntity.LanguageId,
                Value = dynamicEntity.Value
            };

            DataContext.AddTranslationObject(dynamicTranslation);
            DataContext.SaveChanges();
        }

        /// <summary>
        /// Adds the list of dynamic entities.
        /// </summary>
        /// <param name="dynamicEntities">The List of dynamic entities.</param>
        public void Add(List<DynamicTranslationsData> dynamicEntities)
        {
            foreach (var dynamicEntity in dynamicEntities)
            {
                var dynamicTranslation = new DynamicTranslations
                {
                    DescriptionValue = dynamicEntity.DescriptionValue,
                    DynamicKey = dynamicEntity.DynamicKey,
                    LanguageId = dynamicEntity.LanguageId,
                    Value = dynamicEntity.Value
                };

                DataContext.AddTranslationObject(dynamicTranslation);
                DataContext.SaveChanges();
            }
        }

        /// <summary>
        /// Updates the specified dynamic entity.
        /// </summary>
        /// <param name="dynamicEntity">The dynamic DataContract entity.</param>
        public void Update(DynamicTranslationsData dynamicEntity)
        {
            DynamicTranslations entity =
                DataContext.DynamicTranslations.FirstOrDefault(
                    e => e.DynamicKey == dynamicEntity.DynamicKey && e.LanguageId == dynamicEntity.LanguageId);

            //check for null
            if (entity == null)
            {
                return;
            }

            entity.Value = dynamicEntity.Value;
            entity.DescriptionValue = dynamicEntity.DescriptionValue;
            entity.DynamicKey = dynamicEntity.DynamicKey;

            DataContext.SaveChanges();
        }

        /// <summary>
        /// Updates the list of dynamic entities.
        /// </summary>
        /// <param name="dynamicEntities">The List of <see cref="DynamicTranslationsData"/>.</param>
        public void Update(List<DynamicTranslationsData> dynamicEntities)
        {
            foreach (var dynamicEntity in dynamicEntities)
            {
                DynamicTranslations entity =
                DataContext.DynamicTranslations.FirstOrDefault(
                    e => e.DynamicKey == dynamicEntity.DynamicKey && e.LanguageId == dynamicEntity.LanguageId);

                //adds dynamicEntity if it doesn't exist for update
                if (entity == null)
                {
                    DataContext.AddTranslationObject(dynamicEntity);
                }

                //update exist entity
                else
                {
                    entity.Value = dynamicEntity.Value;
                    entity.DescriptionValue = dynamicEntity.DescriptionValue;
                    entity.DynamicKey = dynamicEntity.DynamicKey;
                }

                DataContext.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the specified dynamic entity.
        /// </summary>
        /// <param name="dynamicEntity">The <see cref="DynamicTranslationsData"/>.</param>
        public void Delete(DynamicTranslationsData dynamicEntity)
        {
            DynamicTranslations dynamicTranslation =
                DataContext.DynamicTranslations.FirstOrDefault(
                    d => d.DynamicKey == dynamicEntity.DynamicKey && d.LanguageId == dynamicEntity.LanguageId);

            //check for null
            if (dynamicTranslation == null)
            {
                return;
            }

            DataContext.DeleteTranslationObject(dynamicTranslation);
            DataContext.SaveChanges();
        }

        /// <summary>
        /// Deletes DynamicTranslations by list of dynamic keys.
        /// </summary>
        /// <param name="dynamicKeys">The dynamic keys.</param>
        public void Delete(List<string> dynamicKeys)
        {
            var dynamicTranslations = DataContext.DynamicTranslations.Where(e => dynamicKeys.Contains(e.DynamicKey)).ToList();
            foreach (var dynamicTranslation in dynamicTranslations)
            {
                try
                {
                    DataContext.DeleteTranslationObject(dynamicTranslation);
                    DataContext.SaveChanges();
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }

        /// <summary>
        /// Gets all dynamic translations.
        /// </summary>
        /// <returns>
        /// The List of all <see cref="DynamicTranslationsData"/> items.
        /// </returns>
        public List<DynamicTranslationsData> GetAllDynamicTranslations()
        {
            var allDynamicTranslations = DataContext.DynamicTranslations.Include(x=>x.Language).ToList();
            return allDynamicTranslations.Select(dynamicTranslation => new DynamicTranslationsData()
            {
                DynamicObjectId = dynamicTranslation.DynamicObjectId,
                DescriptionValue = dynamicTranslation.DescriptionValue,
                DynamicKey = dynamicTranslation.DynamicKey,
                LanguageId = dynamicTranslation.LanguageId,
                Value = dynamicTranslation.Value,
                Lenguage = new LanguageData()
                {
                    IsDefault = dynamicTranslation.Language.IsDefault,
                    LanguageId = dynamicTranslation.Language.LanguageId,
                    Name = dynamicTranslation.Language.Name
                }

            }).ToList();
        }

        /// <summary>
        /// Gets the dynamic translations data.
        /// </summary>
        /// <param name="translationKey">The translation key.</param>
        /// <returns>
        /// The list of <see cref="DynamicTranslationsData"/>.
        /// </returns>
        public List<DynamicTranslationsData> GetDynamicTranslationsData(string translationKey)
        {
            return DataContext.DynamicTranslations
               .Where(e => e.DynamicKey.Contains(translationKey) || e.Value.Contains(translationKey))
               .Select(s => new DynamicTranslationsData
               {
                   DynamicObjectId = s.DynamicObjectId,
                   LanguageId = s.LanguageId,
                   Value = s.Value,
                   DynamicKey = s.DynamicKey,
                   DescriptionValue = s.DescriptionValue
               })
               .ToList();
        }

        #endregion
    }
}