using System.Collections.Generic;
using System.Linq;
using Roads.Common.Integrations;
using Roads.Common.Repositories;
using Roads.Common.Models.DataContract;
using Roads.DataBase.Model.Models;

namespace Roads.Common.Managers
{
	public class SettingsManager
	{
		#region Private fields

		/// <summary>
		/// The settings repository
		/// </summary>
        private readonly ISettingsRepository _settingsRepository;

		#endregion

		#region Constructor and Destructor

		/// <summary>
		/// Initializes a new instance of the <see cref="SettingsManager"/> class.
		/// </summary>
		/// <param name="repository">The repository.</param>
        public SettingsManager(ISettingsRepository repository)
		{
			_settingsRepository = repository;
		}


		/// <summary>
		/// Initializes a new instance of the <see cref="SettingsManager"/> class.
		/// </summary>
		public SettingsManager()
		{
			_settingsRepository = new SettingsRepository();
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Gets the settings.
		/// </summary>
		/// <returns></returns>
		/// <exception cref="System.Exception">
		/// Settings list is null
		/// or
		/// Settings list is empty
		/// </exception>
		public List<SettingData> GetSettings()
		{
			return _settingsRepository.GetAllSettings().Select(
                s => new SettingData
                {
                    SettingId = s.SettingId, 
                    SettingName = s.SettingName, 
                    SettingValue = s.SettingValue
                }).ToList();
		}

		/// <summary>
		/// Sets the settings.
		/// </summary>
		/// <param name="settings">The settings.</param>
		public void SetSettings(List<SettingData> settings)
		{
            _settingsRepository.UpdateSettings(
                settings.Select(
                x=>new Setting
                {
                    SettingId = x.SettingId, 
                    SettingName = x.SettingName, 
                    SettingValue = x.SettingValue
                }).ToList());
		}

		#endregion
	}
}


