using System.IO;

namespace Roads.Common.Helpers
{
    /// <summary>The settings helper.</summary>
    /// <typeparam name="T">Settings type</typeparam>
    public static class SettingsHelper<T>
    {
        #region Public Methods and Operators

        /// <summary>The create instance.</summary>
        /// <param name="settingsFile">The settings file.</param>
        /// <returns>The <see cref="T"/>.</returns>
        /// <exception cref="FileNotFoundException"></exception>
        public static T CreateInstance(string settingsFile)
        {
            if (!string.IsNullOrEmpty(settingsFile))
            {
                var settingsXml = File.ReadAllText(settingsFile);
                return SerializationHelper<T>.DeserializeXml(settingsXml);
            }

            throw new FileNotFoundException(string.Format("Settings file {0} not found", typeof(T)), settingsFile);
        }

        #endregion
    }
}
