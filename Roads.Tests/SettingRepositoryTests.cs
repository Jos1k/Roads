using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Roads.Common.Repositories;
using Roads.DataBase.Model.Models;
using Roads.Tests.Helpers;
using Roads.Tests.Mocks;

namespace Roads.Tests
{
    [TestFixture]
    class SettingRepositoryTests
    {
        /// <summary>
        /// The _datacontext
        /// </summary>
        readonly DataContextMock _datacontext = new DataContextMock();

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestFixtureSetUp]
        public void Init()
        {
            DataBaseMockFiller.Instance.AddDataForSettingsTest(_datacontext.DataContext);            
        }

        [Test]
        public void SettingRepository_GetAllSettings_CheckIfDBContainTwoSetting()
        {
            //arrange
            var settingRepository = new SettingsRepository(_datacontext);
            
            //act
            var result = settingRepository.GetAllSettings().ToList().Count;

            //assert
            Assert.AreEqual(2, result);
        }

        [Test]
        [TestCase("SearchDepth")]
        [TestCase("NumberOfRecordPerPage")]
        public void SettingRepository_GetAllSettings_CheckIfDBContainTwoNotZerroSettings(string name)
        {
            //arrange
            var settingRepository = new SettingsRepository(_datacontext);

            //act
            var result = settingRepository.GetAllSettings().ToList().FirstOrDefault(s => s.SettingName == name);

            //assert
            Assert.Greater(Convert.ToInt32(result.SettingValue), 0);
        }

        [Test]
        [TestCase("SearchDepth","8")]
        [TestCase("NumberOfRecordPerPage", "12")]
        [TestCase("SearchDepth", "5")]
        [TestCase("NumberOfRecordPerPage", "100")]
        [TestCase("NumberOfRecordPerPage", "xxx")]
        public void SettingRepository_UpdateSettings_CheckIfUpdate(string name, string value)
        {
            //arrange
            var settingRepository = new SettingsRepository(_datacontext);

            //act
            settingRepository.UpdateSettings(new List<Setting>
            {
                new Setting {SettingName = name, SettingValue = value}
            });

            var result = _datacontext.Settings.Single(s => s.SettingName == name);

            //assert
            Assert.AreEqual(result.SettingValue, value);
        }

    }
}
