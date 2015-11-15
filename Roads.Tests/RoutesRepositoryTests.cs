using System;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using Roads.Common.Repositories;
using Roads.DataBase.Model.Models;
using Roads.Tests.Helpers;
using Roads.Tests.Mocks;

namespace Roads.Tests
{
    [TestFixture]
    public class RoutesRepositoryTests
    {
        readonly DataContextMock _datacontext = new DataContextMock();

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestFixtureSetUp]
        public void Init()
        {
            DataBaseMockFiller.Instance.Fill(_datacontext.DataContext);
            DataBaseMockFiller.Instance.AddDataForRouteRepositoryTest(_datacontext.DataContext);
            DataBaseMockFiller.Instance.AddDataForFeedbackSettingsTest(_datacontext.DataContext);
            DataBaseMockFiller.Instance.AddDataForFindRouteDetailsTest(_datacontext.DataContext);
        }

        /// <summary>
        /// The test for routes repository GetSearchingDepth method.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="expectedResult">The expected result.</param>
        [Test]
        [TestCase("", 5)]
        [TestCase("sfsdfdfds", 5)]
        [TestCase("    ", 5)]
        [TestCase("1  1   1", 5)]
        [TestCase("1", 1)]
        [TestCase("2", 2)]
        [TestCase("3", 3)]
        [TestCase("100", 100)]
        [TestCase("111", 111)]
        public void RoutesRepository_GetSearchingDepth(string value, int expectedResult)
        {
            //arrange
            var setting = _datacontext.Settings.FirstOrDefault(e => e.SettingName == "SearchDepth");

            if (setting != null)
            {
                setting.SettingValue = value;
                _datacontext.SaveChanges();
            }

            var repository = new RoutesRepository(_datacontext);

            //act
            var result = repository.GetSearchingDepth();

            //assert
            Assert.AreEqual(result, expectedResult);
        }

        /// <summary>
        /// Routes the repository get hash by track correct data.
        /// </summary>
        /// <param name="track">The track.</param>
        /// <param name="expectedResult">The expected result.</param>
        [Test]
        [TestCase("1075-147-2249-1986-1439", "1A9A4B89")]
        [TestCase("1439-147-2249", "F06342A6")]
        [TestCase("1075-147-1439", "39C5B77C")]
        public void RoutesRepository_GetHashByTrack_CorrectData(string track, string expectedResult)
        {
            // Arrange
            var repository = new RoutesRepository(_datacontext);
            //Act
            var result = repository.GetHashByTrack(track);
            //Assert
            Assert.AreEqual(result, expectedResult);
        }

        /// <summary>
        /// Routes the repository get hash by track null reference incorrect data.
        /// </summary>
        /// <param name="track">The track.</param>
        [Test]
        [TestCase("1439-147-2241")]
        [TestCase("")]
        [TestCase("Incorrect track string")]
        [ExpectedException(typeof(NullReferenceException))]
        public void RoutesRepository_GetHashByTrack_NullReference_IncorrectData(string track)
        {
            // Arrange
            var repository = new RoutesRepository(_datacontext);
            //Act
            var result = repository.GetHashByTrack(track);
        }


        [Test]
        [TestCase(11, 5, "en", new[] { "Jmerinka", "Vinnitsa" })]
        [TestCase(11, 5, "uk", new[] { "Жмеренка", "Вінниця" })]
        [TestCase(11, 5, "ru", new[] { "Жмеринка", "Винница" })]
        [TestCase(11, 10, "en", new[] { "Kiev", "Vinnitsa" })]
        [TestCase(11, 10, "uk", new[] { "Київ", "Вінниця" })]
        [TestCase(11, 10, "ru", new[] { "Киев", "Винница" })]
        public void RoutesRepository_GetRoutDetailsFeedbackFor_PosistiveTest(int originCityNodeID, int destinationCityNodeID, string lang, string[] expected)
        {
            // Arrange
            var repository = new RoutesRepository(_datacontext);
            var originCityNode = _datacontext.CityNodes.SingleOrDefault(m => m.CityNodeId == originCityNodeID);
            var destinationCityNode = _datacontext.CityNodes.SingleOrDefault(m => m.CityNodeId == destinationCityNodeID);
            var routeNode = new RouteNode
            {
                RouteNodeId = 1, 
                OriginCityNodeId = 11, 
                DestinationCityNodeId = 5, 
                OriginCityNode = originCityNode, 
                DestinationCityNode = destinationCityNode
            };

            //Act
            var result = repository.GetRoutDetailsFeedbackFor(routeNode, lang);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, expected.Length);
            Assert.AreEqual(result[0].DestinationCityName, expected[0]);
            Assert.AreEqual(result[0].OriginCityName, expected[1]);
            for (int index = 0; index < result.Count; index++)
            {
                var resultItem = result[index];
                Assert.IsNotNull(resultItem);
            }
        }

        
        [Test]
        [TestCase(100, 5, "en")]
        [TestCase(0, 5, "en")]
        [TestCase(11, 100, "en")]
        [TestCase(11, 0, "en")]
        [ExpectedException(typeof(TargetException))]
        public void RoutesRepository_GetRoutDetailsFeedbackFor_WronrgOriginCity(int оriginCityNodeID, int destinationCityNodeID, string lang)
        {
            // Arrange
            var repository = new RoutesRepository(_datacontext);
            var originCityNode = _datacontext.CityNodes.SingleOrDefault(m => m.CityNodeId == оriginCityNodeID);
            var destinationCityNode = _datacontext.CityNodes.SingleOrDefault(m => m.CityNodeId == destinationCityNodeID);
            var routeNode = new RouteNode { RouteNodeId = 1, OriginCityNodeId = 11, DestinationCityNodeId = 5, OriginCityNode = originCityNode, DestinationCityNode = destinationCityNode };
            //string lang = "en";

            //Act
            var result = repository.GetRoutDetailsFeedbackFor(routeNode, lang);
        }
       
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RoutesRepository_GetRoutDetailsFeedbackFor_NullLanguage()
        {
            // Arrange
            int wrongOriginCityNodeID = 11;
            int destinationCityNodeID = 5;
            var repository = new RoutesRepository(_datacontext);
            var originCityNode = _datacontext.CityNodes.SingleOrDefault(m => m.CityNodeId == wrongOriginCityNodeID);
            var destinationCityNode = _datacontext.CityNodes.SingleOrDefault(m => m.CityNodeId == destinationCityNodeID);
            var routeNode = new RouteNode { RouteNodeId = 1, OriginCityNodeId = 11, DestinationCityNodeId = 5, OriginCityNode = originCityNode, DestinationCityNode = destinationCityNode };
            string lang = null;

            //Act
            var result = repository.GetRoutDetailsFeedbackFor(routeNode, lang);
        }

    }
}
