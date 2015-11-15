using System.Collections.Generic;
using Roads.DataBase.Model.Models;

namespace Roads.Common.Integrations
{
    public interface ISettingsRepository : IRepositoryBase
    {
        /// <summary>
        /// Gets all settings.
        /// </summary>
        /// <returns>The ICollection of <see cref="Setting"/>.</returns>
        ICollection<Setting> GetAllSettings();

        /// <summary>
        /// Updates the settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        void UpdateSettings(ICollection<Setting> settings);
    }
}
