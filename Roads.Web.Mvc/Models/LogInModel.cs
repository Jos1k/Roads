using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Roads.Web.Mvc.Models
{
	public class LogInModel
	{
		[RoadsRequired("LIP_Label_ErrorMsgRequiredUserName")]
		[RoadsDisplayName("LIP_Label_UserName")]
		public string userName { get; set; }

		[RoadsRequired("SSP_Label_ErrorMsgRequiredPassword")]
		[RoadsDisplayName("SSP_Label_Password")]
		public string password { get; set; }
	}
}