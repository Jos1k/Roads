using System;
using Moq;
using NUnit.Framework;
using Roads.Common.Integrations;
using Roads.Common.Managers;
using Roads.Common.Models.Enums;

namespace Roads.Tests
{
    [TestFixture]
    public class ResourceManagerTests
    {
        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void ResourceManager_GetResource_SendNullCultureArgument_ArgumentNullException()
        {
            //arrange
            string key = "cusomKey";
            var cacheProvider = Mock.Of<ICacheProvider>();
            var resourceManager = new ResourceManager(cacheProvider);

            //act
            resourceManager.GetResource(TranslationTypeEnum.StaticTranslation, key, null);
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void ResourceManager_GetResource_SendNullKeyArgument_ArgumentNullException()
        {
            //arrange
            string culture = "en";
            var cacheProvider = Mock.Of<ICacheProvider>();
            var resourceManager = new ResourceManager(cacheProvider);

            //act
            resourceManager.GetResource(TranslationTypeEnum.StaticTranslation, null, culture);
        }
    }
}