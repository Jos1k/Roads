//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Roads.DataBase.Model.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class FeedbackModel
    {
        public FeedbackModel()
        {
            this.FeedbackItems = new HashSet<FeedbackItem>();
        }
    
        public int FeedbackModelId { get; set; }
        public string HtmlCode { get; set; }
        public string JavascriptCode { get; set; }
        public string FeedbackModelName { get; set; }
    
        public virtual ICollection<FeedbackItem> FeedbackItems { get; set; }
    }
}