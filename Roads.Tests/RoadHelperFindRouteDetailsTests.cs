using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
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
    internal class RoadsHelperFindRouteDetailsTests
    {
        [Test]
        [TestCase("a67bfc184b2b9d3c8b49375b9899bff4-Vinnytsia-Jmerinka-Kiev", "en", new[] { "Vinnytsia", "Jmerinka", "Kiev" }, 2)]
        [TestCase("f180b901a62379b199f0b40f65558964-Vinnytsia-Kiev", "en", new[] { "Vinnytsia", "Kiev" }, 1)]
        [TestCase("ea74f2805d7830b6bb58d9918c4dff20-Kiev-Vinnytsia", "en", new[] { "Kiev", "Vinnytsia" }, 1)]
        [TestCase("e7a16a5daa2abc55691a1ede30815603-Kiev-Jmerinka-Vinnytsia", "en", new[] { "Kiev", "Jmerinka", "Vinnytsia" }, 2)]
        public void GetFindRouteDetailsModel(string urlId, string language, string[] cityNames, int nodeCount)
        {
            var client = new Mock<Roads.Web.Mvc.RoadsServiceClient.IRoadsService>();
            client.Setup(m => m.GetFeedbackControlsListForNewFeedback()).Returns(GetMockFeedmackModel());
            client.Setup(m => m.GetRouteDetailsByHash(It.IsAny<string>(), It.IsAny<string>())).Returns((string x, string y) => { return GetMockRouteDetailsData(x, y); });

            FindRoadDatailsModel model = RoadHelper.GetFindRouteDetailsModel(client.Object, urlId, language);

            Assert.IsNotNull(model);
            Assert.IsNotNull(model.CityPointsNames);
            Assert.AreEqual(model.CityPointsNames.ToArray(), cityNames);

            Assert.IsNotNull(model.RDNodesData);
            Assert.AreEqual(model.RDNodesData.Count, nodeCount);
            foreach (var rdNodeData in model.RDNodesData)
            {
                Assert.IsNotNull(rdNodeData);
                Assert.IsNotNull(rdNodeData.Feedbacks);
                foreach (var rdNodeFeedback in rdNodeData.Feedbacks)
                {
                    Assert.IsNotNull(rdNodeFeedback);
                    Assert.IsNotNullOrEmpty(rdNodeFeedback.UserName);
                    Assert.IsNotNull(rdNodeFeedback.SubmitTime);
                    Assert.IsNotNull(rdNodeFeedback.FeedbackId);
                    Assert.IsNotNull(rdNodeFeedback.FeedbackValues);
                    foreach (var rdNodeFeedbackValue in rdNodeFeedback.FeedbackValues)
                    {
                        Assert.IsNotNull(rdNodeFeedbackValue);
                        Assert.IsNotNullOrEmpty(rdNodeFeedbackValue.DescriptionTranslationKey);
                        Assert.IsNotNullOrEmpty(rdNodeFeedbackValue.NameTranslationKey);
                        Assert.IsNotNullOrEmpty(rdNodeFeedbackValue.Value);
                        Assert.IsNotNull(rdNodeFeedbackValue.SortNumber);
                        Assert.IsNotNull(rdNodeFeedbackValue.IsNumeric);
                        Assert.IsNotNull(rdNodeFeedbackValue.Mandatory);
                    }
                }

                Assert.IsNotNull(rdNodeData.Summary);
                Assert.IsNotNull(rdNodeData.Summary.Controls);
                foreach (var rdSummaryControl in rdNodeData.Summary.Controls)
                {
                    Assert.IsNotNull(rdSummaryControl);
                    Assert.IsNotNullOrEmpty(rdSummaryControl.DescriptionTranslationKey);
                    Assert.IsNotNullOrEmpty(rdSummaryControl.NameTranslationKey);
                    Assert.IsNotNull(rdSummaryControl.Value);
                    Assert.IsTrue(rdSummaryControl.Value.Count > 0);
                }

                Assert.IsNotNull(rdNodeData.LeaveFeedbackWindow);
                Assert.IsNotNull(rdNodeData.LeaveFeedbackWindow.PostModel);
                Assert.IsNull(rdNodeData.LeaveFeedbackWindow.PostModel.RDLeaveFeedbackValues);
                Assert.IsNotNull(rdNodeData.LeaveFeedbackWindow.PostModel.SubmitTime);
                Assert.AreEqual(rdNodeData.LeaveFeedbackWindow.PostModel.UserId, 0);
                Assert.IsNotNull(rdNodeData.LeaveFeedbackWindow.PostModel.DestinationCityId);
                Assert.IsNotNull(rdNodeData.LeaveFeedbackWindow.PostModel.OriginCityId);
                Assert.IsNotNull(rdNodeData.LeaveFeedbackWindow.Controls);
                foreach (var feedbackControl in rdNodeData.LeaveFeedbackWindow.Controls)
                {
                    Assert.IsNotNull(feedbackControl);
                    Assert.IsNotNullOrEmpty(feedbackControl.DescriptionTranslationKey);
                    Assert.IsNotNull(feedbackControl.FeedBackItemId);
                    Assert.IsNotNull(feedbackControl.IsMandatory);
                    Assert.IsNotNullOrEmpty(feedbackControl.JSCode);
                    Assert.IsNotNullOrEmpty(feedbackControl.HTMLCode);
                    Assert.IsNotNull(feedbackControl.SortNumber);
                }
            }
            Assert.IsNotNull(model.RouteSummary);
            Assert.IsNotNull(model.RouteSummary.Controls);
            foreach (var rdSummaryControl in model.RouteSummary.Controls)
            {
                Assert.IsNotNull(rdSummaryControl);
                Assert.IsNotNullOrEmpty(rdSummaryControl.DescriptionTranslationKey);
                Assert.IsNotNullOrEmpty(rdSummaryControl.NameTranslationKey);
                Assert.IsNotNull(rdSummaryControl.Value);
                Assert.IsTrue(rdSummaryControl.Value.Count > 0);
            }
            Assert.AreEqual(model.RouteSummary.From, cityNames[0]);
            Assert.AreEqual(model.RouteSummary.To, cityNames[cityNames.Length - 1]);
        }


        [Test]
        [TestCase("a67bfc184b2b9d3c8b49375b9899bff4-Vinnytsia-Jmerinka-Kiev", "a67bfc184b2b9d3c8b49375b9899bff4")]
        [TestCase("f180b901a62379b199f0b40f65558964-Vinnytsia-Kiev", "f180b901a62379b199f0b40f65558964")]
        [TestCase("ea74f2805d7830b6bb58d9918c4dff20-Kiev-Vinnytsia", "ea74f2805d7830b6bb58d9918c4dff20")]
        [TestCase("e7a16a5daa2abc55691a1ede30815603-Kiev-Jmerinka-Vinnytsia", "e7a16a5daa2abc55691a1ede30815603")]
        public void GetHashFromUrl(string urlId, string expected)
        {
            string actual = RoadHelper.GetHashFromUrl(urlId);

            Assert.AreEqual(actual, expected);
        }

        #region helper methods


        /// <summary>
        /// Gets the mock route details data.
        /// </summary>
        /// <param name="hash">The hash.</param>
        /// <param name="language">The language.</param>
        /// <returns>Returns mock of RouteDetailsData.</returns>
        private static RouteDetailsData GetMockRouteDetailsData(string hash, string language)
        {
            var routeDetailsData = new RouteDetailsData();

            var routeDetailsItems = new List<RouteDetailsFeedback[]>();

            switch (hash)
            {
                case "a67bfc184b2b9d3c8b49375b9899bff4":
                    //Vinnytsia-Jmerinka
                    routeDetailsItems.Add(GetRoutData("Vinnytsia", 11, "Jmerinka", 5).ToArray());
                    //Jmerinka-Kiev
                    routeDetailsItems.Add(GetRoutData("Jmerinka", 5, "Kiev", 1).ToArray());
                    break;

                case "f180b901a62379b199f0b40f65558964":
                    //Vinnytsia-Kiev
                    routeDetailsItems.Add(GetRoutData("Vinnytsia", 11, "Kiev", 1).ToArray());
                    break;

                case "ea74f2805d7830b6bb58d9918c4dff20":
                    //Kiev-Vinnytsia
                    routeDetailsItems.Add(GetRoutData("Kiev", 1, "Vinnytsia", 11).ToArray());
                    break;

                case "e7a16a5daa2abc55691a1ede30815603":
                    //Kiev-Jmerinka
                    routeDetailsItems.Add(GetRoutData("Kiev", 1, "Jmerinka", 5).ToArray());
                    //Jmerinka-Vinnytsia
                    routeDetailsItems.Add(GetRoutData("Jmerinka", 5, "Vinnytsia", 11).ToArray());
                    break;
            }
            routeDetailsData.RouteDetailsItems = routeDetailsItems.ToArray();
            return routeDetailsData;
        }


        /// <summary>
        /// Gets the rout data.
        /// </summary>
        /// <param name="OriginCityName">Name of the origin city.</param>
        /// <param name="OriginCityId">The origin city identifier.</param>
        /// <param name="DestinationCityName">Name of the destination city.</param>
        /// <param name="DestinationCityId">The destination city identifier.</param>
        /// <returns>Returns mock array of RouteDetailsFeedback elements.</returns>
        private static List<RouteDetailsFeedback> GetRoutData(string OriginCityName, int OriginCityId, string DestinationCityName, int DestinationCityId)
        {
            var routeDetailsItem = new List<RouteDetailsFeedback>();

            for (int i = 0; i < 3; i++)
            {
                var feedbackValueData = new List<FeedbackValueData>();
                for (int j = 0; j < 3; j++)
                {
                    var feedbackItem = new FeedbackItemData
                    {
                        DescriptionTranslationKey = "DescriptionTranslationKey",
                        FeedbackItemId = j,
                        NameTranslationKey = "NameTranslationKey",
                        SortNumber = j,
                        IsNumeric = true,
                        Mandatory = true,
                        FeedbackModelId = j
                    };

                    feedbackValueData.Add(new FeedbackValueData
                    {
                        Value = j.ToString(),
                        FeedbackId = j,
                        FeedbackItemId = j,
                        FeedbackValueId = j,
                        FeedbackItem = feedbackItem
                    });
                }
                routeDetailsItem.Add(new RouteDetailsFeedback
                {
                    OriginCityId = OriginCityId,
                    OriginCityName = OriginCityName,
                    DestinationCityId = DestinationCityId,
                    DestinationCityName = DestinationCityName,
                    Value = i.ToString(),
                    SubmitTime = DateTime.Now,
                    UserName = "User " + i,
                    FeedbackId = i,
                    FeedbackValues = feedbackValueData.ToArray()
                });
            }
            return routeDetailsItem;
        }

        /// <summary>
        /// Gets the mock feedmack model.
        /// </summary>
        /// <returns>Returns array of HolisticFeedback.</returns>
        private static HolisticFeedback[] GetMockFeedmackModel()
        {
            var feedmackModel = new List<HolisticFeedback>();
            for (int i = 0; i < 3; i++)
            {
                feedmackModel.Add(new HolisticFeedback
                {
                    FeedbackItem = new FeedbackItemData
                    {
                        DescriptionTranslationKey = "DescriptionTranslationKey",
                        SortNumber = (i + 1),
                        NameTranslationKey = "NameTranslationKey",
                        FeedbackItemId = (i + 1),
                        Mandatory = true
                    },
                    FeedbackModel = new FeedbackModelData
                    {
                        HtmlCode = "HtmlCode",
                        JavascriptCode = "JavascriptCode"
                    }

                });
            }
            return feedmackModel.ToArray();
        }
        #endregion
    }
}
