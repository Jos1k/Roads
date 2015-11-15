using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Roads.Web.Mvc.Helpers
{
    public static class CultureHelper
    {
        //TODO Must retrieve the default language and available languages through WCF instead hardcode!

        // Include ONLY cultures you are implementing
        private static readonly List<string> _cultures = new List<string>
        {
            "en", // first culture is the DEFAULT
            "ru",
            "uk"
        };


        /// <summary>
        ///     Returns a valid culture name based on "name" parameter. If "name" is not valid, it returns the default culture.
        /// </summary>
        /// <param name="name">
        /// Culture's name (e.g. en-US)
        /// </param>
        public static string GetCulture(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return GetDefaultCulture();
            }

            // validation
            if (_cultures.Any(c => c.Equals(name, StringComparison.InvariantCultureIgnoreCase)))
                return name;

            string n = GetNeutralCulture(name);
            foreach (string c in _cultures.Where(c => c.StartsWith(n)))
            {
                return c;
            }

            return GetDefaultCulture(); // return Default culture as no match found
        }

        /// <summary>
        ///     Returns default culture name which is the first name decalared (e.g. en-US)
        /// </summary>
        /// <returns>
        ///     Default culture.
        /// </returns>
        public static string GetDefaultCulture()
        {
            return _cultures[0]; 
        }

        /// <summary>
        ///     Gets the current culture.
        /// </summary>
        /// <returns>
        ///     Current culture.
        /// </returns>
        public static string GetCurrentCulture()
        {
            return Thread.CurrentThread.CurrentCulture.Name;
        }

        /// <summary>
        ///     Gets the current neutral culture.
        /// </summary>
        /// <returns>
        ///     The current neutral culture.
        /// </returns>
        public static string GetCurrentNeutralCulture()
        {
            return GetNeutralCulture(Thread.CurrentThread.CurrentCulture.Name);
        }

        /// <summary>
        ///     Gets the neutral culture.
        /// </summary>
        /// <param name="name">The name of culture.</param>
        /// <returns>
        ///     The neutral culture.
        /// </returns>
        public static string GetNeutralCulture(string name)
        {
            return !name.Contains("-") ? name : name.Split('-')[0];
        }
    }
}