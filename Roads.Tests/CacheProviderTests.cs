using System;
using Moq;
using NUnit.Framework;
using Roads.Common.CacheProvider;
using Roads.Common.Integrations;
using Roads.Common.Models.Enums;

namespace Roads.Tests
{
    [TestFixture]
    public class CacheProviderTests
    {
        #region Private fields and constants

        private const TranslationTypeEnum StaticTranslationType = TranslationTypeEnum.StaticTranslation;
        private const TranslationTypeEnum DynamicTranslationType = TranslationTypeEnum.DynamicTranslation;
        private const TranslationTypeEnum MapObjectTranslationType = TranslationTypeEnum.MapObjectTranslation;

        private IStaticTranslationsRepository _staticTranslationsRepository;
        private IDynamicTranslationsRepository _dynamicTranslationsRepository;
        private IMapObjectTranslationsRepository _mapObjRepositoryTranslations;
        private readonly string[] _expectedValues = {"Dropdown list", "Add button"};
        private readonly string[] _complexCacheKey = {"en", "drop_down"};
        private const string ButtonCacheKey = "en_addButton";

        #endregion

        #region Tests

        [Test]
        [TestCase(StaticTranslationType)]
        [TestCase(DynamicTranslationType)]
        [TestCase(MapObjectTranslationType)]
        public void CacheProvider_Fetch_PassComplexKey_GetResource_AreEqualExpectedValue(
            TranslationTypeEnum translationType)
        {
            //arrange
            InitTranslationRepo(_expectedValues[0]);

            var cacheProvider = new CacheProvider(_staticTranslationsRepository, _dynamicTranslationsRepository,
                _mapObjRepositoryTranslations,
                null);

            //act
            string result = cacheProvider.Fetch(translationType, _complexCacheKey);

            //assert
            Assert.AreEqual(_expectedValues[0], result);
        }

        [Test]
        [TestCase(StaticTranslationType)]
        [TestCase(DynamicTranslationType)]
        [TestCase(MapObjectTranslationType)]
        public void CacheProvider_Fetch_PassEmptyKey_GetEmptyString_AreEqualExpectedValue(
            TranslationTypeEnum translationType)
        {
            //arrange

            InitTranslationRepo(String.Empty);
            var cacheProvider = new CacheProvider(_staticTranslationsRepository, _dynamicTranslationsRepository,
                _mapObjRepositoryTranslations,
                null);

            //act
            string result = cacheProvider.Fetch(translationType, String.Empty);

            //assert
            Assert.AreEqual(String.Empty, result);
        }

        [Test]
        public void CacheProvider_Fetch_PassSingleKey_GetResourceFromCache_AreEqualExpectedValue()
        {
            //arrange

            InitTranslationRepo(_expectedValues[1]);
            var cacheProvider = new CacheProvider(_staticTranslationsRepository, _dynamicTranslationsRepository,
                _mapObjRepositoryTranslations,
                null);

            //act
            string result = cacheProvider.Fetch(StaticTranslationType, ButtonCacheKey);
            //get the value from cache without accessing the DB

            //assert
            Assert.AreEqual(_expectedValues[1], result);
        }

        #endregion

        #region Private methods

        private void InitTranslationRepo(string expectedValue)
        {
            _staticTranslationsRepository = Mock.Of<IStaticTranslationsRepository>(
                r => r.GetValue(It.IsAny<string>(), It.IsAny<string>()) == expectedValue);

            _dynamicTranslationsRepository = Mock.Of<IDynamicTranslationsRepository>(
                r => r.GetValue(It.IsAny<string>(), It.IsAny<string>()) == expectedValue);

            _mapObjRepositoryTranslations = Mock.Of<IMapObjectTranslationsRepository>(
                r => r.GetValue(It.IsAny<string>(), It.IsAny<string>()) == expectedValue);
        }

        #endregion
    }
}