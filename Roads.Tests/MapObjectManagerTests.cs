using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Roads.Common.Managers;
using Roads.Common.Models.DataContract;
using Roads.Common.Repositories;
using Roads.Tests.Helpers;
using Roads.Tests.Mocks;

namespace Roads.Tests
{
    [TestFixture]
    public class MapObjectManagerTests
    {
        readonly DataContextMock _datacontext = new DataContextMock();

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestFixtureSetUp]
        public void Init()
        {
            DataBaseMockFiller.Instance.Fill(_datacontext.DataContext);
        }

        /// <summary>
        /// Gets the translations for_ send_ city node id_ get list map object translation data.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="count">The count.</param>
        [Test]
        [TestCase(1,3)]
        [TestCase(2,3)]
        [TestCase(4,3)]
        [TestCase(5,3)]
        [TestCase(6,3)]
        [TestCase(7,3)]
        public void GetTranslationsFor_Send_CityNodeId_GetListMapObjectTranslationData(int id, int count)
        {
            //arrange
            var repository = new MapObjectTranslationsRepository(_datacontext);

            var manager = new MapObjectManager(repository);

            //act
            var result = manager.GetTranslationsFor(id);

            //assert
            Assert.AreEqual(result.Count, count);
        }

        /// <summary>
        /// Gets the regions for select list_ sent language name_ get_ list of translations.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="count">The count.</param>
        [Test]
        [TestCase("ru", 3)]
        [TestCase("ua", 3)]
        [TestCase("en", 3)]
        [TestCase(null, 3)]
        [TestCase("", 3)]
        [TestCase("       ", 3)]
        public void GetRegionsForSelectList_SentLanguageName_Get_ListOfTranslations(string name, int count)
        {
            //arrange
            var repository = new MapObjectTranslationsRepository(_datacontext);

            var manager = new MapObjectManager(repository);

            //act
            var result = manager.GetRegionsForSelectList(name);

            //assert
            Assert.AreEqual(result.Count, count); 
        }

        [Test]
        [TestCase("asdsa1-sadfasfsaf2-asfdasfasf3",1,"zdd1")]
        [TestCase("asdsa1-sadfasfsaf2-asfdasfasf3", 2, "zdd2")]
        public void CreateMapObjectWithTranslation_CreateNew_CheckDataSourth(string values, int regionId, string languageKey)
        {
            //arrange
            var repository = new MapObjectTranslationsRepository(_datacontext);

            var manager = new MapObjectManager(repository);

            var translations = values.Split('-').ToArray().Select(value => new MapObjectTranslationData {Value = value, LanguageKey = languageKey, LanguageId = 1}).ToList();

            //act
            manager.CreateMapObjectWithTranslation(translations, regionId, languageKey);

            var result = _datacontext.CityNodes.FirstOrDefault(e => e.LanguageKey == languageKey); ;

            //assert
            Assert.IsNotNull(result); 
        }
    }
}
