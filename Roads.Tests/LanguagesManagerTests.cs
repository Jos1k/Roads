using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Roads.Common.Integrations;
using Roads.Common.Managers;
using Roads.Common.Models.DataContract;
using Roads.DataBase.Model.Models;

namespace Roads.Tests
{
    [TestFixture]
    internal class LanguagesManagerTests
    {
        private readonly Mock<ILanguageRepository> _mock = new Mock<ILanguageRepository>();
        private ILanguageRepository _mockLanguageRepository;
        private List<LanguageData> _languages;

        [TestFixtureSetUp]
        public void Initialize()
        {
            _languages = new List<LanguageData>
            {
                new LanguageData {IsDefault = true, LanguageId = 1, Name = "ua"},
                new LanguageData {IsDefault = false, LanguageId = 2, Name = "ru"},
                new LanguageData {IsDefault = false, LanguageId = 3, Name = "en"}
            };

            _mock.Setup(m => m.GetDefaultLanguageName()).Returns("en");
            _mock.Setup(m => m.GetLanguageByKey("en"))
                .Returns(new Language {Name = "en", IsDefault = false, LanguageId = 3});
            _mock.Setup(m => m.GetLanguageByKey("ru"))
                .Returns(new Language {Name = "ru", IsDefault = false, LanguageId = 2});
            _mock.Setup(m => m.GetLanguageByKey("uk"))
                .Returns(new Language {Name = "uk", IsDefault = true, LanguageId = 1});

            _mock.Setup(m => m.GetLanguageByKey(""))
                .Returns(new Language {Name = "uk", IsDefault = true, LanguageId = 1});
            _mock.Setup(m => m.GetLanguageByKey(null))
                .Returns(new Language {Name = "uk", IsDefault = true, LanguageId = 1});
            _mock.Setup(m => m.GetLanguageByKey("Cz"))
                .Returns(new Language {Name = "uk", IsDefault = true, LanguageId = 1});
            _mock.Setup(m => m.GetAllLanguagesData()).Returns(_languages);

            _mockLanguageRepository = _mock.Object;
        }

        [Test]
        public void LanguagesManager_GetAllLanguagesData()
        {
            //arrange
            var languagesManager = new LanguagesManager(_mockLanguageRepository);

            //act
            List<LanguageData> result = languagesManager.GetAllLanguagesData();

            //assert
            Assert.AreSame(_languages, result);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("Cz")]
        public void LanguagesManager_GetLanguageId_SendNullAndEmptyAndNotValidName_ExpectDefaultLanguageId(
            string languageName)
        {
            //arrange
            const int expectedValue = 1;
            var languagesManager = new LanguagesManager(_mockLanguageRepository);

            //act
            int result = languagesManager.GetLanguageId(languageName);

            //assert
            Assert.AreEqual(expectedValue, result);
        }

        [Test]
        public void LanguagesManager_GetLanguageId_SendEnLanguageName_ExpectSpecificLanguageId()
        {
            //arrange
            const string languageName = "en";
            const int expectedValue = 3;
            var languagesManager = new LanguagesManager(_mockLanguageRepository);

            //act
            int result = languagesManager.GetLanguageId(languageName);

            //assert
            Assert.AreEqual(expectedValue, result);
        }

        [Test]
        public void LanguagesManager_GetLanguageId_SendRuLanguageName_ExpectSpecificLanguageId()
        {
            //arrange
            const string languageName = "ru";
            const int expectedValue = 2;
            var languagesManager = new LanguagesManager(_mockLanguageRepository);

            //act
            int result = languagesManager.GetLanguageId(languageName);

            //assert
            Assert.AreEqual(expectedValue, result);
        }

        [Test]
        public void LanguagesManager_GetLanguageId_SendUkLanguageName_ExpectSpecificLanguageId()
        {
            //arrange
            const string languageName = "uk";
            const int expectedValue = 1;
            var languagesManager = new LanguagesManager(_mockLanguageRepository);

            //act
            int result = languagesManager.GetLanguageId(languageName);

            //assert
            Assert.AreEqual(expectedValue, result);
        }
    }
}