using System.Collections.Generic;
using Roads.Common.Models.DataContract;
using Roads.DataBase.Model.Models;

namespace Roads.Common.Integrations
{
    public interface IFeedbackItemRepository : IRepositoryBase
    {
        /// <summary>
        /// Gets all feedback item data.
        /// </summary>
        /// <returns>ICollection of <see cref="FeedbackItem"/>.</returns>
        ICollection<FeedbackItem> GetAllFeedbackItemData();

        /// <summary>
        /// Gets all feedback item data.
        /// </summary>
        /// <param name="feedbackItemsData">The feedback items data.</param>
        void UpdateFeedbackItemData(ICollection<FeedbackItem> feedbackItemsData);

        /// <summary>
        /// Deletes the feedback item data.
        /// </summary>
        /// <param name="feedbackItem">The feedback item.</param>
        void DeleteFeedbackItemData(FeedbackItem feedbackItem);

        /// <summary>
        /// Adds the new feedback item.
        /// </summary>
        /// <param name="feedbackItem">The feedback item.</param>
        void AddNewFeedbackItem(FeedbackItem feedbackItem);

        /// <summary>
        /// Gets the feedback models.
        /// </summary>
        /// <returns>The List of <see cref="FeedbackModelData"/></returns>
        List<FeedbackModelData> GetFeedbackModels();

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="userPassword">The user password.</param>
        /// <param name="userType">Type of the user.</param>
        /// <returns>Return Id of created user</returns>
        int CreateUser(string userName, string userPassword, string userType);

        /// <summary>
        /// Gets the name of the user by.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>Return user object</returns>
        User GetUserByName(string userName);
    }
}
