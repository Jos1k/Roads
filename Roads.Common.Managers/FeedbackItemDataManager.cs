using System.Collections.Generic;
using System.Linq;
using Roads.Common.Integrations;
using Roads.Common.Models.DataContract;
using Roads.Common.Repositories;
using Roads.DataBase.Model.Models;

namespace Roads.Common.Managers
{
    public class FeedbackItemDataManager
    {
        #region Private fields

        /// <summary>
        /// The _feedback item repository
        /// </summary>
        private readonly IFeedbackItemRepository _feedbackItemRepository;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FeedbackItemDataManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public FeedbackItemDataManager(IFeedbackItemRepository repository)
		{
            _feedbackItemRepository = repository;
		}
        
        /// <summary>
        /// Initializes a new instance of the <see cref="FeedbackItemDataManager"/> class.
        /// </summary>
        public FeedbackItemDataManager()
		{
            _feedbackItemRepository = new FeedbackItemRepository();
		}

		#endregion

        #region Public Methods

        /// <summary>
        /// Gets the feedback items data.
        /// </summary>
        /// <returns>List of FeedbackItemData from repository</returns>
        public List<FeedbackItemData> GetFeedbackItemsData()
        {
            var resultFeedbackItemsDataList = new List<FeedbackItemData>();

            foreach (var feedbackItem in _feedbackItemRepository.GetAllFeedbackItemData().ToList())
            {
                resultFeedbackItemsDataList.Add(
                    new FeedbackItemData
                    {
                        FeedbackItemId = feedbackItem.FeedbackItemId,
                        NameTranslationKey = feedbackItem.NameTranslationKey,
                        SortNumber = feedbackItem.SortNumber,
                        DescriptionTranslationKey = feedbackItem.DescriptionTranslationKey,
                        IsNumeric = feedbackItem.IsNumeric,
                        Mandatory = feedbackItem.Mandatory,
                        FeedbackModelId = feedbackItem.FeedbackModelId
                    });
            }
            return resultFeedbackItemsDataList;
        }

        /// <summary>
        /// Sets the feedback items data.
        /// </summary>
        /// <param name="feedbackItemsDataList">The feedback items data list.</param>
        public void SetFeedbackItemsData(List<FeedbackItemData> feedbackItemsDataList)
        {
            var savingFeedbackItemsList = new List<FeedbackItem>();

            foreach (var feedbackItemData in feedbackItemsDataList)
            {
                savingFeedbackItemsList.Add(
                    new FeedbackItem
                    {
                        FeedbackItemId = feedbackItemData.FeedbackItemId,
                        NameTranslationKey = feedbackItemData.NameTranslationKey,
                        SortNumber = feedbackItemData.SortNumber,
                        DescriptionTranslationKey = feedbackItemData.DescriptionTranslationKey,
                        IsNumeric = feedbackItemData.IsNumeric,
                        Mandatory = feedbackItemData.Mandatory,
                        FeedbackModelId = feedbackItemData.FeedbackModelId
                    });
            }
            _feedbackItemRepository.UpdateFeedbackItemData(savingFeedbackItemsList);
        }

        /// <summary>
        /// Deletes the feedback item.
        /// </summary>
        /// <param name="feedbackItem">The feedback item.</param>
        public void DeleteFeedbackItem(FeedbackItemData feedbackItem)
        {
            var feedbackItemForSave =new FeedbackItem
                    {
                        FeedbackItemId = feedbackItem.FeedbackItemId,
                        NameTranslationKey = feedbackItem.NameTranslationKey,
                        SortNumber = feedbackItem.SortNumber,
                        DescriptionTranslationKey = feedbackItem.DescriptionTranslationKey,
                        IsNumeric = feedbackItem.IsNumeric,
                        Mandatory = feedbackItem.Mandatory,
                        FeedbackModelId = feedbackItem.FeedbackModelId
                    };
            _feedbackItemRepository.DeleteFeedbackItemData(feedbackItemForSave);
        }

        /// <summary>
        /// Adds the new feedback item.
        /// </summary>
        /// <param name="feedbackItem">The feedback item.</param>
        public void AddNewFeedbackItem(FeedbackItemData feedbackItem)
        {
            var feedbackItemForAdd = new FeedbackItem
            {
                FeedbackItemId = feedbackItem.FeedbackItemId,
                NameTranslationKey = feedbackItem.NameTranslationKey,
                SortNumber = feedbackItem.SortNumber,
                DescriptionTranslationKey = feedbackItem.DescriptionTranslationKey,
                IsNumeric = feedbackItem.IsNumeric,
                Mandatory = feedbackItem.Mandatory,
                FeedbackModelId = feedbackItem.FeedbackModelId
            };
            _feedbackItemRepository.AddNewFeedbackItem(feedbackItemForAdd);
        }

        /// <summary>
        /// Gets the feedback models.
        /// </summary>
        /// <returns>The List of <see cref="FeedbackModelData"/>.</returns>
        public List<FeedbackModelData> GetFeedbackModels()
        {
            return _feedbackItemRepository.GetFeedbackModels();
        }

        /// <summary>
        /// Gets the name of the user identifier by.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>Return user id</returns>
        public int? GetUserIdByName (string userName)
        {
            User user = _feedbackItemRepository.GetUserByName(userName);
            return user != null ? (int?) user.UserId : null;
        }

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="userPassword">The user password.</param>
        /// <param name="userType">Type of the user.</param>
        /// <returns>Return Id of created user</returns>
        public int CreateUser(string userName,string userPassword, string userType)
        {
            return _feedbackItemRepository.CreateUser(userName, userPassword, userType);
        }

        #endregion
    }
}
