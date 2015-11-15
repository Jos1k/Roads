namespace Roads.Common.Integrations
{
    /// <summary>
    /// The base class for singleton realization.
    /// </summary>
    /// <typeparam name="T"> The class. </typeparam>
    public class SingletonBase<T>
        where T : class, new()
    {
        #region Static Fields

        private static T _instance;

        #endregion

        #region Public Properties

        /// <summary>Gets the instance.</summary>
        public static T Instance
        {
            get
            {
                return _instance ?? (_instance = new T());
            }
        }

        #endregion
    }
}
