using System;
using System.Linq;
using System.Threading;
using Roads.Web.Mvc.RoadsServiceClient;

namespace Roads.Web.Mvc.Services
{
    public static class CultureHelper
    {
        #region Private Fields

        private static LanguageData[] _languages;
        private static string _defaultLanguage;
        private static string[] _availableLanguages;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the array of <see cref="LanguageData"/>.
        /// </summary>
        /// <value>
        /// The languages.
        /// </value>
        public static LanguageData[] Languages
        {
            get
            {

                try
                {
                    if (_languages == null)
                    {
                        using (var client = new RoadsServiceClient.RoadsServiceClient())
                        {
                            _languages = client.GetAllLanguages().ToArray();
                        }
                    }
                    return _languages;
                }

                //Rethrow an exception caused by unavailability of wcf service.
                catch (System.InvalidOperationException ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Gets the default language.
        /// </summary>
        /// <value>
        /// The default language.
        /// </value>
        public static string DefaultLanguage
        {
            get
            {
                try
                {
                    _defaultLanguage = Languages.Where(l => l.IsDefault).Select(s => s.Name.ToLower()).FirstOrDefault();
                }

                //Catch an exception caused by unavailability of wcf service.
                catch (System.InvalidOperationException)
                {
                    return "en";
                }
                return _defaultLanguage;
            }
        }

        /// <summary>
        /// Gets the available languages.
        /// </summary>
        /// <value>
        /// The available languages.
        /// </value>
        public static string[] AvailableLanguages
        {
            get
            {
                try
                {
                    _availableLanguages = Languages.Select(l => l.Name.ToLower()).ToArray();
                }

                //Catch an exception caused by unavailability of wcf service.
                catch (System.InvalidOperationException)
                {
                    return new[] { "en", "ru", "uk" };
                }
                return _availableLanguages;
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Returns a valid culture name based on "name" parameter. If "name" is not valid, it returns the default culture.
        /// </summary>
        /// <param name="name">
        /// Culture's name
        /// </param>
        public static string GetCulture(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return GetDefaultCulture();
            }

            // validation
            if (AvailableLanguages.Any(c => c.Equals(name, StringComparison.CurrentCultureIgnoreCase)))
                return name.ToLowerInvariant();

            string n = GetNeutralCulture(name.ToLower());
            foreach (string c in AvailableLanguages.Where(c => c.StartsWith(n)))
            {
                return c;
            }

            return GetDefaultCulture(); // return Default culture as no match found
        }

        /// <summary>
        /// Gets the default culture name which is the first name decalared.
        /// </summary>
        /// <returns>
        /// Default culture.
        /// </returns>
        public static string GetDefaultCulture()
        {
            return DefaultLanguage;
        }

        /// <summary>
        /// Gets the current culture.
        /// </summary>
        /// <returns>
        /// Current culture.
        /// </returns>
        public static string GetCurrentCulture()
        {
            return Thread.CurrentThread.CurrentCulture.Name;
        }

        /// <summary>
        /// Gets the current neutral culture.
        /// </summary>
        /// <returns>
        /// The current neutral culture.
        /// </returns>
        public static string GetCurrentNeutralCulture()
        {
            return GetNeutralCulture(Thread.CurrentThread.CurrentCulture.Name);
        }

        /// <summary>
        ///  Gets the neutral culture.
        /// </summary>
        /// <param name="name">The name of culture.</param>
        /// <returns>
        /// The neutral culture.
        /// </returns>
        public static string GetNeutralCulture(string name)
        {
            return !name.Contains("-") ? name : name.Split('-')[0];
        }

        /// <summary>
        /// Make the first letter to uppercase.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>Text with first letter in uppercase.</returns>
        public static string FirstLetterUp(string text)
        {
            if (String.IsNullOrEmpty(text))
            {
                return String.Empty;
            }
            return char.ToUpper(text[0]) + text.Substring(1);
        }

        #endregion
    }
}