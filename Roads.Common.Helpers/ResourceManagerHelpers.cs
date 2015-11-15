using System;
using System.Collections.Generic;

namespace Roads.Common.Helpers
{
    /// <summary>
    /// ResourceManagerHelpers functionality.
    /// </summary>
    public class ResourceManagerHelpers
    {
        #region Public methods

        /// <summary>
        /// Normalizes the culture parameter.
        /// </summary>
        /// <param name="culture">The culture.</param>
        public void NormalizeCultureParameter(ref string culture)
        {
            try
            {
                culture = culture.ToLowerInvariant();
                culture = culture.Substring(0, 2);
            }
            catch (ArgumentOutOfRangeException)
            {
                culture = String.Empty;
            }
        }

        /// <summary>
        /// Validates the culture.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <param name="availableLanguages">The abailable languages.</param>
        /// <returns>
        /// Validation result (true or false)
        /// </returns>
        public bool ValidateCulture(string culture, List<string> availableLanguages)
        {
            return availableLanguages.Contains(culture);
        }

        #endregion
    }
}