namespace Roads.Web.Mvc.Services
{
    public interface ITranslationManager
    {
        /// <summary>
        /// Gets the label translation.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <param name="userLangusge">The user langusge.</param>
        /// <returns>Returns translated text.</returns>
        string GetLabelTranslation(string labelId, string userLangusge);

        /// <summary>
        /// Gets the dynamic translation.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <param name="userLanguage">The user language.</param>
        /// <returns>Returns translated text.</returns>
        string GetDynamicTranslation(string labelId, string userLanguage);
    }
}