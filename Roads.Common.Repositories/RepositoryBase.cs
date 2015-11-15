using Roads.Common.Integrations;

namespace Roads.Common.Repositories
{
    /// <summary>The repository base.</summary>
    public abstract class RepositoryBase : IRepositoryBase
    {
        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the class. Initialize new instance of Repository class</summary>
        /// <param name="dataContext">DataContext object</param>
        protected RepositoryBase(IDataContext dataContext)
        {
            DataContext = dataContext;
        }

        /// <summary>Initializes a new instance of the  class. 
        /// Initialize new instance of Repository class</summary>
        protected RepositoryBase()
        {
            DataContext = new DataContext();
        }

        #endregion

        #region Public and Protected Properties

        /// <summary>
        /// Gets or sets DataContext object to access to database
        /// </summary>
        protected IDataContext DataContext { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Saves all changes to DataContext
        /// </summary>
        public void Save()
        {
            DataContext.SaveChanges();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            DataContext.Dispose();
        } 

        #endregion
    }
}
