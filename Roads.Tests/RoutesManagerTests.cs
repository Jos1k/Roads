using System;
using System.Linq;
using NUnit.Framework;
using Roads.Common.Managers;
using Roads.Common.Repositories;
using Roads.Tests.Helpers;
using Roads.Tests.Mocks;

namespace Roads.Tests
{
    [TestFixture]
    public class RoutesManagerTests
    {
        readonly DataContextMock _datacontext = new DataContextMock();

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestFixtureSetUp]
        public void Init()
        {
            DataBaseMockFiller.Instance.AddDataForRouteManagerTest(_datacontext.DataContext);
        }

        [Test]
        [TestCase(1, 10, 21)]
        [TestCase(10, 1, 21)]
        [TestCase(3, 9, 22)]
        [TestCase(9, 3, 22)]
        public void RoadsManager_BuildRoutes_CheckNumbersOfResponse(int fromId, int toId, int count)
        {
            //arrange
            var repository = new RoutesRepository(_datacontext);

            var manager = new RoadsManager(repository);

            //act
            var result = manager.BuildRoutes(fromId, toId);

            //assert
            Assert.AreEqual(result.Count, count);
        }

        [Test]
        [TestCase("1-2-9-4-10", 5)]
        public void RoadsManager_GetRouteEstimation_SendStringRoad_CheckCount(string road, int pointsCout)
        {
            //arrange
            var repository = new RoutesRepository(_datacontext);

            var manager = new RoadsManager(repository);

            //act
            var result = manager.GetRouteEstimation(road);

            //asserts
            Assert.AreEqual(result.CityPointsIds.Count(), pointsCout);
        }

        [Test]
        [TestCase("1-2-9-4-10", 4)]
        public void RoadsManager_GetRouteEstimation_SendStringRoad_CheckTheNodesCount(string road, int pointsCout)
        {
            //arrange
            var repository = new RoutesRepository(_datacontext);

            var manager = new RoadsManager(repository);

            //act
            var result = manager.GetRouteEstimation(road);

            //asserts
            Assert.AreEqual(result.NodesEstimations.Count, pointsCout);
        }

        [Test]
        [TestCase("1-2-9-4-10", "D93F604B")]
        [TestCase("1-2-9-4-10-7-8-70", "6D885DDD")]
        [TestCase("9534-2323-19-4-10-7-8213-71240", "E7361842")]
        public void RoadsManager_GetHashForRoute(string trek, string hash)
        {
            //arrange
            var repository = new RoutesRepository(_datacontext);

            var manager = new RoadsManager(repository);

            //act
            var result = manager.GetHashForRoute(trek);

            //asserts
            Assert.AreEqual(result, hash);
        }

        [Test]
        [TestCase(25, "UA")]
        [ExpectedException(typeof(NullReferenceException))]
        public void RoadsManager_GetFullTranslationForCityById_NullReferenceException(int cityId, string lang)
        {
            //Arrange
            var repository = new MapObjectTranslationsRepository(_datacontext);
            var manager = new SmartSuggestionsManager(repository);

            //Act
            var result = manager.GetFullTranslationForCityById(cityId, lang);
        }
    }
}
