using System.Data.Entity.Validation;
using System.Linq;
using Roads.DataBase.Model.Models;

namespace Roads.Common.Integrations
{
    public class DataContext : IDataContext
    {
        #region Private fields

        /// <summary>The _data context.</summary>
        private readonly DatabaseModelContainer _dataContext = new DatabaseModelContainer();
		private readonly TranslationModelContainer _translationContext = new TranslationModelContainer();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DataContext"/> class.
        /// </summary>
        public DataContext()
        {
            _dataContext.Database.CommandTimeout = 300;
            _dataContext.Configuration.LazyLoadingEnabled = false;

            _translationContext.Database.CommandTimeout = 300;
            _translationContext.Configuration.LazyLoadingEnabled = false;
        }

        #endregion

        #region Public Properties

        public IQueryable<RouteNode> RouteNodes
        {
            get { return _dataContext.RouteNodes; }
        }

        public IQueryable<CityNode> CityNodes
        {
            get { return _dataContext.CityNodes; }
        }

        public IQueryable<RegionNode> RegionNodes
        {
            get { return _dataContext.RegionNodes; }
        }

        public IQueryable<User> Users
        {
            get { return _dataContext.Users; }
        }

        public IQueryable<Feedback> Feedbacks
        {
            get { return _dataContext.Feedbacks; }
        }

        public IQueryable<FeedbackItem> FeedbackItems
        {
            get { return _dataContext.FeedbackItems; }
        }

        public IQueryable<FeedbackModel> FeedbackModels
        {
            get { return _dataContext.FeedbackModels; }
        }

        public IQueryable<Language> Languages
        {
            get { return _translationContext.Languages; }
        }

        public IQueryable<DynamicTranslations> DynamicTranslations
        {
            get { return _translationContext.DynamicTranslations; }
        }

        public IQueryable<StaticTranslation> StaticTranslations
        {
            get { return _translationContext.StaticTranslations; }
        }

        public IQueryable<MapObjectTranslation> MapObjectTranslations
        {
            get { return _translationContext.MapObjectTranslations; }
        }

        public IQueryable<FeedbackValue> FeedbackValues
        {
            get { return _dataContext.FeedbackValues; }
        }

        public IQueryable<Trek> Treks
        {
            get { return _dataContext.Treks; }
        }


		/// <summary>
		/// GetLanguageById the Site Settings
		/// </summary>
		/// <value>
		/// Site Settings
		/// </value>
		public IQueryable<Setting> Settings
        {
            get { return _dataContext.Settings; }
        }

        #endregion

        #region Public Methods

        public void AddDataObject<T>(T entity) where T : class
        {
            _dataContext.Set<T>().Add(entity);
        }

        public void AddTranslationObject<T>(T entity) where T : class
        {
            _translationContext.Set<T>().Add(entity);
        }

        public void DeleteDataObject<T>(T entity) where T : class
        {
            _dataContext.Set<T>().Remove(entity);
        }

        public void DeleteTranslationObject<T>(T entity) where T : class
        {
            _translationContext.Set<T>().Remove(entity);
        }

        public void SaveChanges()
        {
            try
            {
                _dataContext.SaveChanges();

                _translationContext.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var info = string.Join(
                    ";",
                    ex.EntityValidationErrors.SelectMany(ve => ve.ValidationErrors)
                        .Select(ve => string.Format("[{0}]: [{1}]", ve.PropertyName, ve.ErrorMessage)));

                //TODO: Add Logger!

                throw;
            }
        }

        #endregion

        #region IDisposable implementation

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _dataContext.Dispose();
        }

        #endregion
    }
}
