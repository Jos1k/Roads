using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Roads.Common.Repositories;
using Roads.DataBase.Model.Models;
using Roads.Tests.Helpers;
using Roads.Tests.Mocks;

namespace Roads.Tests
{
    [TestFixture]
    class FeedbackItemRepositoryTests
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
        public void SettingRepository_GetAllFeedbackSettings_CheckIfDBContainFourSetting()
        {
            //arrange
            var feedbackItemRepository = new FeedbackItemRepository(_datacontext);

            //act
            var result = feedbackItemRepository.GetAllFeedbackItemData().ToList().Count;

            //assert
            Assert.AreEqual(5, result);
        }

        [Test]
        public void SettingRepository_AddNewFeedbackItem_CheckIfDBContainNewFeedbackItem()
        {
            //arrange
            var feedbackItemRepository = new FeedbackItemRepository(_datacontext);
            FeedbackItem newFeedbackItem = new FeedbackItem
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
            feedbackItemRepository.AddNewFeedbackItem(newFeedbackItem);
            var resultCount = feedbackItemRepository.GetAllFeedbackItemData().ToList().Count;

            //assert
            Assert.AreEqual(5, resultCount);
            Assert.Contains(newFeedbackItem, feedbackItemRepository.GetAllFeedbackItemData().ToList());
        }

        [Test]
        public void SettingRepository_UpdateFeedbackItemData_CheckIfDBContainUpdatedFeedbackItem()
        {
            //arrange
            var feedbackItemRepository = new FeedbackItemRepository(_datacontext);
            FeedbackItem newFeedbackItem = new FeedbackItem
            {
                FeedbackItemId = 100,
                Mandatory = false,
                SortNumber = 5,
                IsNumeric = false,
                FeedbackModelId = 2,
                NameTranslationKey = "NameTranslKey5",
                DescriptionTranslationKey = "DescrTranslation Key5"
            };
            
            //act
            feedbackItemRepository.AddNewFeedbackItem(newFeedbackItem);
            
            newFeedbackItem.Mandatory = true;
            newFeedbackItem.SortNumber = 100;
            newFeedbackItem.IsNumeric = true;
            newFeedbackItem.FeedbackModelId = 1;
            newFeedbackItem.NameTranslationKey = "NameTranslKey100";
            newFeedbackItem.DescriptionTranslationKey = "DescrTranslation Key100";

            feedbackItemRepository.UpdateFeedbackItemData(new List<FeedbackItem>() { newFeedbackItem });
            var updatedList = feedbackItemRepository.GetAllFeedbackItemData().ToList();

            //assert
            Assert.Contains(newFeedbackItem, updatedList);
        }

        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void SettingRepository_UpdateFeedbackItemData_UpdateNonExistingItem()
        {
            //arrange
            var feedbackItemRepository = new FeedbackItemRepository(_datacontext);
            FeedbackItem newFeedbackItem = new FeedbackItem
            {
                FeedbackItemId = 50,
                Mandatory = false,
                SortNumber = 5,
                IsNumeric = false,
                FeedbackModelId = 2,
                NameTranslationKey = "NameTranslKey5",
                DescriptionTranslationKey = "DescrTranslation Key5"
            };

            //act
            feedbackItemRepository.UpdateFeedbackItemData(new List<FeedbackItem>() { newFeedbackItem });
            //assert
        }

        [Test]
        public void SettingRepository_DeleteFeedbackItemData_CheckIfDBContainFourSettingAfterDeleting()
        {
            //arrange
            var feedbackItemRepository = new FeedbackItemRepository(_datacontext);
            FeedbackItem newFeedbackItem = new FeedbackItem
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
            feedbackItemRepository.AddNewFeedbackItem(newFeedbackItem);
            var resultCountAfterAdding = feedbackItemRepository.GetAllFeedbackItemData().ToList().Count;
            feedbackItemRepository.DeleteFeedbackItemData(newFeedbackItem);
            var resultCountAfterDeleting = feedbackItemRepository.GetAllFeedbackItemData().ToList().Count;

            //assert
            Assert.AreEqual(6, resultCountAfterAdding);
            Assert.AreEqual(5, resultCountAfterDeleting);
        }

        [Test]
        public void SettingRepository_DeleteFeedbackItemData_DeleteNonExistingItem()
        {
            //arrange
            var feedbackItemRepository = new FeedbackItemRepository(_datacontext);
            FeedbackItem newFeedbackItem = new FeedbackItem
            {
                FeedbackItemId = 200,
                Mandatory = true,
                SortNumber = 200,
                IsNumeric = false,
                FeedbackModelId = 2,
                NameTranslationKey = "NameTranslKey200",
                DescriptionTranslationKey = "DescrTranslation Key200"
            };

            var resultCountAfterAdding = feedbackItemRepository.GetAllFeedbackItemData().ToList().Count;
            feedbackItemRepository.DeleteFeedbackItemData(newFeedbackItem);
            var resultCountAfterDeleting = feedbackItemRepository.GetAllFeedbackItemData().ToList().Count;

            //act
            Assert.AreEqual(resultCountAfterDeleting, resultCountAfterAdding);
        }
    }
}
