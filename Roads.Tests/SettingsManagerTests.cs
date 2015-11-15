using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Roads.Common.Managers;
using Roads.Common.Models.DataContract;
using Roads.Common.Repositories;
using Roads.Tests.Helpers;
using Roads.Tests.Mocks;
using Roads.DataBase.Model.Models;

namespace Roads.Tests
{
    [TestFixture]
    class SettingsManagerTests
    {
        /// <summary>
        /// The _datacontext
        /// </summary>
        private readonly DataContextMock _datacontext = new DataContextMock();

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestFixtureSetUp]
        public void Init()
        {
            DataBaseMockFiller.Instance.AddDataForSettingsTest(_datacontext.DataContext);
        }

        [Test]
        public void SettingsManager_GetSettings_CheckIfDBContainTwoNotZerroSettings()
        {
            //arrange
            var settingRepository = new SettingsRepository(_datacontext);
            var settingManager = new SettingsManager(settingRepository);

            //act
            var result = settingManager.GetSettings().ToList().Count;

            //assert
            Assert.AreEqual(result, 2);
        }

        [Test]
        [TestCase("SearchDepth")]
        [TestCase("NumberOfRecordPerPage")]
        public void SettingRepository_GetAllSettings_CheckIfDBContainTwoNotZerroSettings(string name)
        {
            //arrange
            var settingRepository = new SettingsRepository(_datacontext);
            var settingManager = new SettingsManager(settingRepository);

            //act

            var result = settingManager.GetSettings().ToList().Single(s => s.SettingName == name);

            //assert
            Assert.Greater(Convert.ToInt32(result.SettingValue), 0);
        }

        [Test]
        [TestCase("SearchDepth", "8")]
        [TestCase("NumberOfRecordPerPage", "12")]
        [TestCase("SearchDepth", "1")]
        [TestCase("NumberOfRecordPerPage", "100")]
        [TestCase("NumberOfRecordPerPage", "xxx")]
        public void SettingRepository_UpdateSettings_CheckIfUpdate(string name, string value)
        {
            //arrange
            var settingRepository = new SettingsRepository(_datacontext);
            var settingManager = new SettingsManager(settingRepository);

            //act
            settingManager.SetSettings(new List<SettingData>
            {
                new SettingData{SettingName = name, SettingValue = value}
            });

            var result = settingRepository.GetAllSettings().Single(s => s.SettingName == name);

            //assert
            Assert.AreEqual(result.SettingValue, value);
        }
    }
}
