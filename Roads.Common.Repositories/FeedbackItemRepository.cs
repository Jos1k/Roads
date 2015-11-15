using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Roads.Common.Integrations;
using Roads.Common.Models.DataContract;
using Roads.DataBase.Model.Models;

namespace Roads.Common.Repositories
{
    public class FeedbackItemRepository : RepositoryBase, IFeedbackItemRepository
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FeedbackItemRepository"/> class.
        /// </summary>
        /// <param name="dataContext">DataContext object</param>
        public FeedbackItemRepository(IDataContext dataContext)
            : base(dataContext)
        {
            
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="FeedbackItemRepository"/> class.
        /// </summary>
        public FeedbackItemRepository()
            : base(new DataContext())
        {
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets all feedback item data.
        /// </summary>
        /// <returns>The List of FeedbackItem.</returns>
        /// <exception cref="System.ArgumentNullException">feedbackItemDataList</exception>
        public ICollection<FeedbackItem> GetAllFeedbackItemData()
        {
            List<FeedbackItem> feedbackItemDataList = DataContext.FeedbackItems.Select(x => x).ToList();
            
            if (feedbackItemDataList == null)
            {
                throw new ArgumentNullException("feedbackItemDataList");
            }

            return feedbackItemDataList;
        }

        /// <summary>
        /// Updates the feedback item data.
        /// </summary>
        /// <param name="feedbackItems">The feedback items.</param>
        public void UpdateFeedbackItemData(ICollection<FeedbackItem> feedbackItems)
        {
            foreach (var feedbackItem in feedbackItems)
            {
                var savedFeedbackItem =
                    DataContext.FeedbackItems.FirstOrDefault(m => m.FeedbackItemId == feedbackItem.FeedbackItemId);
                savedFeedbackItem.NameTranslationKey = feedbackItem.NameTranslationKey;
                savedFeedbackItem.SortNumber = feedbackItem.SortNumber;
                savedFeedbackItem.DescriptionTranslationKey = feedbackItem.DescriptionTranslationKey;
                savedFeedbackItem.IsNumeric = feedbackItem.IsNumeric;
                savedFeedbackItem.Mandatory = feedbackItem.Mandatory;
                savedFeedbackItem.FeedbackModelId = feedbackItem.FeedbackModelId;
            }
            Save();
        }

        /// <summary>
        /// Deletes the feedback item data.
        /// </summary>
        /// <param name="feedbackItem">The feedback item.</param>
        public async void DeleteFeedbackItemData(FeedbackItem feedbackItem)
        {
            var deleted = await DataContext.FeedbackItems.FirstAsync(w => w.FeedbackItemId == feedbackItem.FeedbackItemId);

            if (deleted != null)
            {
                List<FeedbackValue> feedbacks = await DataContext.FeedbackValues.Where(x => x.FeedbackItemId == deleted.FeedbackItemId).ToListAsync();

                feedbacks.ForEach(x => DataContext.DeleteDataObject(x));
                DataContext.DeleteDataObject(deleted);

                Save();
            }
        }

        /// <summary>
        /// Adds the new feedback item.
        /// </summary>
        /// <param name="feedbackItem">The feedback item.</param>
        public void AddNewFeedbackItem(FeedbackItem feedbackItem)
        {
            DataContext.AddDataObject(feedbackItem);
            Save();
        }

        /// <summary>
        /// Gets the feedback models.
        /// </summary>
        /// <returns>List of the <see cref="FeedbackModelData"/>.</returns>
        public List<FeedbackModelData> GetFeedbackModels()
        {
            var models = DataContext.FeedbackModels.ToList();
            return models.Select(model => new FeedbackModelData
            {
                FeedbackModelId = model.FeedbackModelId,
                HtmlCode = model.HtmlCode,
                JavascriptCode = model.JavascriptCode,
                FeedBackModalName = model.FeedbackModelName
            }).ToList();
        }

        /// <summary>
        /// Adds the new feedback.
        /// </summary>
        /// <param name="routeNodeId">The route node identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="submitDateTime">The submit date time.</param>
        /// <returns>Return id of added feedback</returns>
        public int AddNewFeedback(int routeNodeId,int userId,DateTime submitDateTime)
        {
            Feedback feedback = new Feedback()
            {
                RouteNodeId = routeNodeId,
                SubmitTime = submitDateTime,
                UserId = userId
            };
            DataContext.AddDataObject(feedback);
            Save();
            return feedback.FeedbackId;
        }

        /// <summary>
        /// Adds the new feedback value.
        /// </summary>
        /// <param name="feedbackValueData">The feedback value data.</param>
        public void AddNewFeedbackValue(FeedbackValueData feedbackValueData)
        {
            DataContext.AddDataObject(new FeedbackValue()
            {
                FeedbackId = feedbackValueData.FeedbackId,
                FeedbackItemId = feedbackValueData.FeedbackItemId,
                Value = feedbackValueData.Value
            });
            Save();
        }

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="userPassword">The user password.</param>
        /// <param name="userType">Type of the user.</param>
        /// <returns>Return created user ID</returns>
        public int CreateUser(string userName, string userPassword, string userType)
        {
            User user = new User(){ Name = userName, Password = userPassword, UserType = userType};
            DataContext.AddDataObject(user);
            Save();
            return user.UserId;
        }

        /// <summary>
        /// Gets the name of the user by.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>Return user object</returns>
        public User GetUserByName(string userName)
        {
            return DataContext.Users.FirstOrDefault(x => x.Name == userName);
        }

        #endregion
    }
}
