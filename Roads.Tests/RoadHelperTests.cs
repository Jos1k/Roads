using System;
using Moq;
using NUnit.Framework;
using Roads.Tests.Mocks;
using Roads.Web.Mvc.Models;
using Roads.Web.Mvc.Services;
using Suggestion = Roads.Web.Mvc.RoadsServiceClient.Suggestion;

namespace Roads.Tests
{
    [TestFixture]
    class RoadHelperTests
    {

        #region CheckIfAddRoadModelValid Tests
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RoadHelper_AddRoadStepOne_CheckIfAddRoadModelValid_ArgumentsNull_ModelNull_GetException()
        {
            AddRoadModel model = null;
            RoadHelper.CheckIfAddRoadModelValid( ref model);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RoadHelper_AddRoadStepOne_CheckIfAddRoadModelValid_ArgumentsNull_CityPointsNull_GetException()
        {
            var model = new AddRoadModel();
            model.CityPoints = null;
            RoadHelper.CheckIfAddRoadModelValid( ref model);
        }

        [Test]
        public void RoadHelper_AddRoadStepOne_CheckIfAddRoadModelValid_BadArguments_GetFalse()
        {
            var client = new Mock<Roads.Web.Mvc.RoadsServiceClient.IRoadsService>();
            client.Setup(m => m.GetSearchingDepth()).Returns(10);

            var model = new AddRoadModel();
            model = RoadHelper.FillAddRoadModel(client.Object,model, new TestTranslationManager(), "en_En");
            ValidateDefaultAddRoadModel(model);

            bool result = RoadHelper.CheckIfAddRoadModelValid(ref model);
            Assert.IsFalse(result);
        }

        [Test]
        public void RoadHelper_AddRoadStepOne_CheckIfAddRoadModelValid_GoodArguments_GetTrue()
        {
            var client = new Mock<Roads.Web.Mvc.RoadsServiceClient.IRoadsService>();
            client.Setup(m => m.GetSearchingDepth()).Returns(10);

            var model = new AddRoadModel();
            model = RoadHelper.FillAddRoadModel(client.Object, model, new TestTranslationManager(), "en_En");
            ValidateDefaultAddRoadModel(model);

            // Modify Model
            model.CityPoints[0].CityName = "CityOne";
            model.CityPoints[0].CityId = "IdOne";
            model.CityPoints[1].CityName = "CityOne";
            model.CityPoints[1].CityId = "IdOne";
            
            bool result = RoadHelper.CheckIfAddRoadModelValid(ref model);
            Assert.IsTrue(result);
        }
        #endregion

        #region SmartSuggestion Tests
        [Test]
        public void RoadHelper_SmartSuggestion_GetSuggestions_SuggestionsFound()
        {
            var client = new Mock<Roads.Web.Mvc.RoadsServiceClient.IRoadsService>();
            var arrayOfSuggestions = new[]
            {
                new Suggestion {SuggestionCityName = "Vinnytsia", CityNodeId = 432},
                new Suggestion {SuggestionCityName = "Kiev", CityNodeId = 844}
            };

            client.Setup(m => m.GetSuggestions(It.IsAny<string>(), It.IsAny<string>())).Returns(() => arrayOfSuggestions);
            SuggestionsModel model = RoadHelper.GetSuggestions(client.Object, String.Empty, String.Empty);

            Assert.NotNull(model);
            Assert.NotNull(model.suggestions);
            Assert.AreEqual(2, model.suggestions.Count);

            Assert.AreEqual("432", model.suggestions[0].data);
            Assert.AreEqual("Vinnytsia", model.suggestions[0].value);

            Assert.AreEqual("844", model.suggestions[1].data);
            Assert.AreEqual("Kiev", model.suggestions[1].value);

        }
        [Test]
        public void RoadHelper_SmartSuggestion_GetSuggestions_SuggetsionNotFound()
        {
            var client = new Mock<Roads.Web.Mvc.RoadsServiceClient.IRoadsService>();
            client.Setup(m => m.GetSuggestions(It.IsAny<string>(), It.IsAny<string>())).Returns(() => new Suggestion[] {});
            SuggestionsModel model = RoadHelper.GetSuggestions(client.Object, String.Empty, String.Empty);

            Assert.NotNull(model);
            Assert.NotNull(model.suggestions);
            Assert.AreEqual(0, model.suggestions.Count);
        }
        #endregion

        #region Validation Methods
        internal protected static void ValidateDefaultAddRoadModel(AddRoadModel model)
        {
            Assert.IsNotNull(model);

            Assert.IsNotNull(model.CityPoints);
            Assert.AreEqual(2, model.CityPoints.Count);
            Assert.AreEqual("From", model.CityPoints[0].Placeholder);
            Assert.AreEqual("To", model.CityPoints[1].Placeholder);
            Assert.AreEqual(null, model.CityPoints[0].CityName);
            Assert.AreEqual(null, model.CityPoints[1].CityName);
            Assert.AreEqual(null, model.CityPoints[0].CityId);
            Assert.AreEqual(null, model.CityPoints[1].CityId);
        }
        #endregion
    }
}
