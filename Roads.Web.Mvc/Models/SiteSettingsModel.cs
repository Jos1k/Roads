using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Roads.Web.Mvc.Models
{
    public class SiteSettingsModel
    {
        [RoadsDisplayName("SSP_Label_NumberOfRecordsPerPage")]
        [RoadsRange(1, 1000, "SSP_ErrorMessage_InvalidValue")]
        [RoadsRegularExpression("([1-9][0-9]*)", "SSP_ErrorMessage_InvalidValue")]
        public int numberOfRecordsPerPage{ get; set; }

        [RoadsDisplayName("SSP_Label_SearchDepth")]
        [RoadsRange(1, 1000, "SSP_ErrorMessage_InvalidValue")]
        [RoadsRegularExpression("([1-9][0-9]*)", "SSP_ErrorMessage_InvalidValue")]
        public int searchDepth{ get; set; }
    }
}