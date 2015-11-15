using System;
using System.Collections.Generic;
using System.Linq;
using Roads.Common.Integrations;
using Roads.DataBase.Model.Models;

namespace Roads.Common.Repositories
{
    public class SettingsRepository : RepositoryBase, ISettingsRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsRepository"/> class.
        /// </summary>
        /// <param name="dataContext">DataContext object</param>
        public SettingsRepository(IDataContext dataContext)
            : base(dataContext)
        {          
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsRepository"/> class.
        /// </summary>
        public SettingsRepository()
            :base(new DataContext())
        {
            
        }

        /// <summary>
        /// Gets all settings.
        /// </summary>
        /// <returns>The ICollection of <see cref="Setting"/>.</returns>
        /// <exception cref="System.ArgumentNullException">settingsList</exception>
        public ICollection<Setting> GetAllSettings()
        {
            ICollection<Setting> settingsList;
            settingsList = DataContext.Settings.Select(x => x).ToList();

            if (settingsList == null)
            {
                throw new ArgumentNullException("settingsList");
            }
            return settingsList;
        }

        /// <summary>
        /// Updates the settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public void UpdateSettings(ICollection<Setting> settings)
        {
            foreach (var settingItem in settings)
            {
                DataContext.Settings.Single(m => m.SettingName == settingItem.SettingName).SettingValue = settingItem.SettingValue;
            }
            Save();
        }
    }
}
