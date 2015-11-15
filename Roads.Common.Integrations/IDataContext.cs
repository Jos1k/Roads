using System;
using System.Linq;
using Roads.DataBase.Model.Models;

namespace Roads.Common.Integrations
{
    /// <summary>
    /// The IDataContext interface.
    /// </summary>
    public interface IDataContext : IDisposable
    {
        #region Public Properties

        /// <summary>
        /// Gets the route nodes.
        /// </summary>
        /// <value>
        /// The route nodes.
        /// </value>
        IQueryable<RouteNode> RouteNodes { get; }

        /// <summary>
        /// Gets the city nodes.
        /// </summary>
        /// <value>
        /// The city nodes.
        /// </value>
        IQueryable<CityNode> CityNodes { get; }

        /// <summary>
        /// Gets the region nodes.
        /// </summary>
        /// <value>
        /// The region nodes.
        /// </value>
        IQueryable<RegionNode> RegionNodes { get; }

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        IQueryable<User> Users { get; }

        /// <summary>
        /// Gets the feedbacks.
        /// </summary>
        /// <value>
        /// The feedbacks.
        /// </value>
        IQueryable<Feedback> Feedbacks { get; }

        /// <summary>
        /// Gets the feedback items.
        /// </summary>
        /// <value>
        /// The feedback items.
        /// </value>
        IQueryable<FeedbackItem> FeedbackItems { get;}

        /// <summary>
        /// Gets the feedback models.
        /// </summary>
        /// <value>
        /// The feedback models.
        /// </value>
        IQueryable<FeedbackModel> FeedbackModels { get; }

        /// <summary>
        /// Gets the Feedback models.
        /// </summary>
        /// <value>
        /// Feedback models
        /// </value>
        IQueryable<Language> Languages { get; }

        /// <summary>
        /// Gets the languages
        /// </summary>
        /// <value>
        /// Languages
        /// </value>
        IQueryable<DynamicTranslations> DynamicTranslations { get; }

        /// <summary>
        /// Gets the dynamic translations.
        /// </summary>
        /// <value>
        /// The static translations.
        /// </value>
        IQueryable<StaticTranslation> StaticTranslations { get; }

        /// <summary>
        /// Gets the static translations.
        /// </summary>
        /// <value>
        /// The map object translations.
        /// </value>
        IQueryable<MapObjectTranslation> MapObjectTranslations { get; }

        /// <summary>
        /// GGets the Feedback Values
        /// </summary>
        /// <value>
        /// Feedback Values
        /// </value>
        IQueryable<FeedbackValue> FeedbackValues { get; }

        /// <summary>
        /// GetLanguageById the Treks
        /// </summary>
        /// <value>
        /// Treks
        /// </value>
        IQueryable<Trek> Treks { get; }

        /// <summary>
        /// GetLanguageById the Site Settings
        /// </summary>
        /// <value>
        /// Site Settings
        /// </value>
        IQueryable<Setting> Settings { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>The add data object.</summary>
        /// <param name="entity">The entity.</param>
        void AddDataObject<T>(T entity) where T : class;

        /// <summary>The add Translation object.</summary>
        /// <param name="entity">The entity.</param>
        void AddTranslationObject<T>(T entity) where T : class;

        /// <summary>The delete data object.</summary>
        /// <param name="entity">The entity.</param>
        void DeleteDataObject<T>(T entity) where T : class;

        /// <summary>The delete Translation object.</summary>
        /// <param name="entity">The entity.</param>
        void DeleteTranslationObject<T>(T entity) where T : class;

        /// <summary>
        /// The save changes.
        /// </summary>
        void SaveChanges();

        #endregion
    }
}
