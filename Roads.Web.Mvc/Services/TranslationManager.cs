using Roads.Web.Mvc.RoadsServiceClient;

namespace Roads.Web.Mvc.Services
{
    /// <summary>
    /// Class TranslationManager using to get translation for each label by key.
    /// </summary>
    public class TranslationManager : ITranslationManager
    {
        #region Private fields

        //private readonly IRoadsService _service;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslationManager" /> class.
        /// </summary>
        public TranslationManager()
        {
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets the label translation.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <param name="userLanguage">The user language.</param>
        /// <returns>Returns translated text.</returns>
        public string GetLabelTranslation(string labelId, string userLanguage)
        {
            using (var client = new RoadsServiceClient.RoadsServiceClient())
            {
                return client.GetLabelTranslation(labelId, userLanguage);
            }
        }

        /// <summary>
        /// Gets the dynamic translation.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <param name="userLanguage">The user language.</param>
        /// <returns>Returns dynamic resource./</returns>
        public string GetDynamicTranslation(string labelId, string userLanguage)
        {
            using (var client = new RoadsServiceClient.RoadsServiceClient())
            {
                return client.GetDynamicTranslation(labelId, userLanguage);
            }
        }

        #endregion
    }
}