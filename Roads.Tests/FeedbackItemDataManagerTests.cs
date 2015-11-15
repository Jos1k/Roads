using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Roads.Common.Managers;
using Roads.Common.Models.DataContract;
using Roads.Common.Repositories;
using Roads.Tests.Helpers;
using Roads.Tests.Mocks;

namespace Roads.Tests
{
    [TestFixture]
    class FeedbackItemDataManagerTests
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
            DataBaseMockFiller.Instance.AddDataForFeedbackSettingsTest(_datacontext.DataContext);
        }

        [Test]
        public void FeedbackItemDataManager_GetFeedbackItemsData_CheckIfDBContainFourSetting()
        {
            //arrange
            var feedbackItemRepository = new FeedbackItemRepository(_datacontext);
            FeedbackItemDataManager feedbackItemDataManager = new FeedbackItemDataManager(feedbackItemRepository);

            //act
            var result = feedbackItemDataManager.GetFeedbackItemsData().ToList().Count;

            //assert
            Assert.AreEqual(5, result);
        }

        [Test]
        public void FeedbackItemDataManager_AddNewFeedbackItem_CheckIfDBContainNewFeedbackItem()
        {
            //arrange
            var feedbackItemRepository = new FeedbackItemRepository(_datacontext);
            FeedbackItemDataManager feedbackItemDataManager = new FeedbackItemDataManager(feedbackItemRepository);

            FeedbackItemData newFeedbackItem = new FeedbackItemData
            {
                FeedbackItemId = 5,
                Mandatory = false,
                SortNumber = 5,
                IsNumeric = false,
                FeedbackModelId = 2,
                NameTranslationKey = "NameTranslKey5",
                DescriptionTranslationKey = "DescrTranslation Key5"
            };

            //act
            feedbackItemDataManager.AddNewFeedbackItem(newFeedbackItem);
            List<FeedbackItemData> result = feedbackItemDataManager.GetFeedbackItemsData();
            FeedbackItemData checkedFeedbackItem = result.Single(x => x.FeedbackItemId == newFeedbackItem.FeedbackItemId);

            //assert
            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(newFeedbackItem.Mandatory, checkedFeedbackItem.Mandatory);
            Assert.AreEqual(newFeedbackItem.SortNumber, checkedFeedbackItem.SortNumber);
            Assert.AreEqual(newFeedbackItem.IsNumeric, checkedFeedbackItem.IsNumeric);
            Assert.AreEqual(newFeedbackItem.FeedbackModelId, checkedFeedbackItem.FeedbackModelId);
            Assert.AreEqual(newFeedbackItem.NameTranslationKey, checkedFeedbackItem.NameTranslationKey);
            Assert.AreEqual(newFeedbackItem.DescriptionTranslationKey, checkedFeedbackItem.DescriptionTranslationKey);
            Assert.AreEqual(newFeedbackItem.FeedbackItemId, checkedFeedbackItem.FeedbackItemId);
        }

        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void FeedbackItemDataManager_SetFeedbackItemsData_UpdateNonExistingItem()
        {
            //arrange
            var feedbackItemRepository = new FeedbackItemRepository(_datacontext);
            FeedbackItemDataManager feedbackItemDataManager = new FeedbackItemDataManager(feedbackItemRepository);
            FeedbackItemData newFeedbackItem = new FeedbackItemData
            {
                FeedbackItemId = 50,
                Mandatory = false,
                SortNumber = 50,
                IsNumeric = false,
                FeedbackModelId = 2,
                NameTranslationKey = "NameTranslKey50",
                DescriptionTranslationKey = "DescrTranslation Key50"
            };

            //act
            feedbackItemDataManager.SetFeedbackItemsData(new List<FeedbackItemData>() { newFeedbackItem });
        }

        [Test]
        public void FeedbackItemDataManager_SetFeedbackItemsData_CheckIfDBContainUpdatedFeedbackItem()
        {
            //arrange
            var feedbackItemRepository = new FeedbackItemRepository(_datacontext);
            FeedbackItemDataManager feedbackItemDataManager = new FeedbackItemDataManager(feedbackItemRepository);
            FeedbackItemData newFeedbackItem = new FeedbackItemData
            {
                FeedbackItemId = 6,
                Mandatory = false,
                SortNumber = 5,
                IsNumeric = false,
                FeedbackModelId = 2,
                NameTranslationKey = "NameTranslKey5",
                DescriptionTranslationKey = "DescrTranslation Key5"
            };

            //act
            feedbackItemDataManager.AddNewFeedbackItem(newFeedbackItem);

            newFeedbackItem.Mandatory = true;
            newFeedbackItem.SortNumber = 100;
            newFeedbackItem.IsNumeric = true;
            newFeedbackItem.FeedbackModelId = 1;
            newFeedbackItem.NameTranslationKey = "NameTranslKey1";
            newFeedbackItem.DescriptionTranslationKey = "DescrTranslation Key1";

            feedbackItemDataManager.SetFeedbackItemsData(new List<FeedbackItemData>() { newFeedbackItem });
            var updatedList = feedbackItemDataManager.GetFeedbackItemsData().ToList();

            FeedbackItemData checkedFeedbackItem = updatedList.Single(x => x.FeedbackItemId == newFeedbackItem.FeedbackItemId);
            //assert

            Assert.AreEqual(6, updatedList.Count);
            Assert.AreEqual(newFeedbackItem.Mandatory, checkedFeedbackItem.Mandatory);
            Assert.AreEqual(newFeedbackItem.SortNumber, checkedFeedbackItem.SortNumber);
            Assert.AreEqual(newFeedbackItem.IsNumeric, checkedFeedbackItem.IsNumeric);
            Assert.AreEqual(newFeedbackItem.FeedbackModelId, checkedFeedbackItem.FeedbackModelId);
            Assert.AreEqual(newFeedbackItem.NameTranslationKey, checkedFeedbackItem.NameTranslationKey);
            Assert.AreEqual(newFeedbackItem.DescriptionTranslationKey, checkedFeedbackItem.DescriptionTranslationKey);
            Assert.AreEqual(newFeedbackItem.FeedbackItemId, checkedFeedbackItem.FeedbackItemId);
        }

        [Test]
        public void FeedbackItemDataManager_DeleteFeedbackItem_CheckIfDBContainFourSettingAfterDeleting()
        {
            //arrange
            var feedbackItemRepository = new FeedbackItemRepository(_datacontext);
            FeedbackItemDataManager feedbackItemDataManager = new FeedbackItemDataManager(feedbackItemRepository);

            FeedbackItemData newFeedbackItem = new FeedbackItemData
           {
               FeedbackItemId = 5,
               Mandatory = false,
               SortNumber = 5,
               IsNumeric = false,
               FeedbackModelId = 2,
               NameTranslationKey = "NameTranslKey5",
               DescriptionTranslationKey = "DescrTranslation Key5"
           };

            //act
            feedbackItemDataManager.AddNewFeedbackItem(newFeedbackItem);
            var resultCountAfterAdding = feedbackItemDataManager.GetFeedbackItemsData().ToList().Count;
            feedbackItemDataManager.DeleteFeedbackItem(newFeedbackItem);
            var resultCountAfterDeleting = feedbackItemDataManager.GetFeedbackItemsData().ToList().Count;

            //assert
            Assert.AreEqual(6, resultCountAfterAdding);
            Assert.AreEqual(5, resultCountAfterDeleting);
        }

        [Test]
        public void FeedbackItemDataManager_DeleteFeedbackItem_DeleteNonExistingItem()
        {
            //arrange
            var feedbackItemRepository = new FeedbackItemRepository(_datacontext);
            FeedbackItemDataManager feedbackItemDataManager = new FeedbackItemDataManager(feedbackItemRepository);

            FeedbackItemData newFeedbackItem = new FeedbackItemData
            {
                FeedbackItemId = 50,
                Mandatory = false,
                SortNumber = 50,
                IsNumeric = false,
                FeedbackModelId = 2,
                NameTranslationKey = "NameTranslKey50",
                DescriptionTranslationKey = "DescrTranslation Key50"
            };

            //act
            //feedbackItemDataManager.AddNewFeedbackItem(newFeedbackItem);
            var resultCountBeforeDeleting = feedbackItemDataManager.GetFeedbackItemsData().ToList().Count;
            feedbackItemDataManager.DeleteFeedbackItem(newFeedbackItem);
            var resultCountAfterDeleting = feedbackItemDataManager.GetFeedbackItemsData().ToList().Count;

            //assert
            Assert.AreEqual(resultCountBeforeDeleting, resultCountAfterDeleting);
        }

    }
}
