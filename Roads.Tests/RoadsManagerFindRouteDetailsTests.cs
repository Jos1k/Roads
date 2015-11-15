using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Roads.Common.Managers;
using Roads.Common.Repositories;
using Roads.DataBase.Model.Models;
using Roads.Tests.Helpers;
using Roads.Tests.Mocks;
using Roads.Web.Mvc.Models;
using Roads.Web.Mvc.RoadsServiceClient;
using Roads.Web.Mvc.Services;
using Suggestion = Roads.Web.Mvc.RoadsServiceClient.Suggestion;

namespace Roads.Tests
{

    [TestFixture]
    internal class RoadsManagerFindRouteDetailsTests
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
            DataBaseMockFiller.Instance.Fill(_datacontext.DataContext);
            DataBaseMockFiller.Instance.AddDataForFeedbackSettingsTest(_datacontext.DataContext);
            DataBaseMockFiller.Instance.AddDataForFindRouteDetailsTest(_datacontext.DataContext);
        }

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        [TestFixtureTearDownAttribute]
        public void Cleanup()
        {
            _datacontext.Dispose();
        }

        [Test]
        [TestCase("a67bfc184b2b9d3c8b49375b9899bff4", "en", new[] { "Vinnitsa", "Jmerinka", "Kiev" })]
        [TestCase("f180b901a62379b199f0b40f65558964", "en", new[] { "Vinnitsa", "Kiev" })]
        [TestCase("e7a16a5daa2abc55691a1ede30815603", "ru", new[] { "Киев", "Винница" })]
        [TestCase("ea74f2805d7830b6bb58d9918c4dff20", "uk", new[] { "Київ", "Жмеренка", "Вінниця" })]
        public void RoadsManager_GetFindRouteDetailsModel_PositiveTests(string hash, string language, string[] expected)
        {
            var routesRepository = new RoutesRepository(_datacontext);
            var manager = new RoadsManager(routesRepository);

            Common.Models.DataContract.RouteDetailsData routeDetailsData = manager.GetRouteDetailsByHash(hash, language);

            Assert.IsNotNull(routeDetailsData);
            Assert.IsNotNull(routeDetailsData.RouteDetailsItems);
            Assert.AreEqual(routeDetailsData.RouteDetailsItems.Count, expected.Length-1);
            for (int index = 0; index < routeDetailsData.RouteDetailsItems.Count; index++)
            {
                var routeDetailsItem = routeDetailsData.RouteDetailsItems[index];

                Assert.IsNotNull(routeDetailsItem);
                foreach (var routeDetailsFeedback in routeDetailsItem)
                {
                    Assert.IsNotNull(routeDetailsFeedback);
                    Assert.AreEqual(routeDetailsFeedback.OriginCityName, expected[index]);
                    Assert.AreEqual(routeDetailsFeedback.DestinationCityName,
                        index == (expected.Length - 1) ? expected[expected.Length - 1] : expected[index + 1]);
                    Assert.IsNotNull(routeDetailsFeedback.DestinationCityId);
                    Assert.IsNotNull(routeDetailsFeedback.OriginCityId);
                    Assert.IsNotNull(routeDetailsFeedback.SubmitTime);
                    Assert.IsNotNull(routeDetailsFeedback.UserName);
                    Assert.IsNotNull(routeDetailsFeedback.FeedbackId);
                    Assert.IsNotNull(routeDetailsFeedback.FeedbackValues);
                    foreach (var feedbackValueData in routeDetailsFeedback.FeedbackValues)
                    {
                        Assert.IsNotNull(feedbackValueData);
                        Assert.IsNotNull(feedbackValueData.FeedbackId);
                        Assert.IsNotNull(feedbackValueData.FeedbackItem);
                        Assert.IsNotNull(feedbackValueData.FeedbackItemId);
                        Assert.IsNotNull(feedbackValueData.FeedbackValueId);
                        Assert.IsNotNull(feedbackValueData.Value);
                        Assert.IsNotNullOrEmpty(feedbackValueData.FeedbackItem.DescriptionTranslationKey);
                        Assert.IsNotNull(feedbackValueData.FeedbackItem.FeedbackItemId);
                        Assert.IsNotNull(feedbackValueData.FeedbackItem.FeedbackModelId);
                        Assert.IsNotNull(feedbackValueData.FeedbackItem.IsNumeric);
                        Assert.IsNotNull(feedbackValueData.FeedbackItem.Mandatory);
                        Assert.IsNotNullOrEmpty(feedbackValueData.FeedbackItem.NameTranslationKey);
                        Assert.IsNotNull(feedbackValueData.FeedbackItem.SortNumber);
                    }
                }
            }
        }


        [Test]
        [TestCase("", "en")]
        [TestCase("wronghash", "en")]
        public void RoadsManager_GetFindRouteDetailsModel_NegativeTests(string hash, string language)
        {
            //arrange
            var routesRepository = new RoutesRepository(_datacontext);
            var manager = new RoadsManager(routesRepository);

            //act
            Common.Models.DataContract.RouteDetailsData routeDetailsData = manager.GetRouteDetailsByHash(hash, language);

            //assert
            Assert.IsNotNull(routeDetailsData);
            Assert.IsNotNull(routeDetailsData.RouteDetailsItems);
            Assert.AreEqual(routeDetailsData.RouteDetailsItems.Count, 0);
        }

        [Test]
        [TestCase(null, "en")]
        [TestCase("a67bfc184b2b9d3c8b49375b9899bff4", null)]
        [TestCase(null, null)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RoadsManager_GetFindRouteDetailsModel_NegativeTestsForArgumentNullException(string hash, string language)
        {
            //arrange
            var routesRepository = new RoutesRepository(_datacontext);
            var manager = new RoadsManager(routesRepository);

            //act
            Common.Models.DataContract.RouteDetailsData routeDetailsData = manager.GetRouteDetailsByHash(hash, language);
        }    
       
    }
}
