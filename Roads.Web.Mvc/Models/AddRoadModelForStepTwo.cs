using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Providers.Entities;

namespace Roads.Web.Mvc.Models
{
    public class AddRoadModelForStepTwo 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddRoadModelForStepTwo"/> class.
        /// </summary>
        public AddRoadModelForStepTwo()
        {
            Feedbacks = new List<FeedbackToRouteNode>();
            FeedbackControls = new List<FeedbackControl>();
        }

        public List<FeedbackToRouteNode> Feedbacks { get; set; }

        /// <summary>
        /// Gets or sets the feedback controls.
        /// </summary>
        /// <value>
        /// The feedback controls.
        /// </value>
        public List<FeedbackControl> FeedbackControls { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        [DisplayName("User Name")]
        [Required(ErrorMessage = "Please input your name!")]
        public string UserName { get; set; }
    }
}