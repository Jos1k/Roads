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
    
    public partial class RouteNode
    {
        public RouteNode()
        {
            this.Feedbacks = new HashSet<Feedback>();
        }
    
        public int RouteNodeId { get; set; }
        public int OriginCityNodeId { get; set; }
        public int DestinationCityNodeId { get; set; }
    
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual CityNode OriginCityNode { get; set; }
        public virtual CityNode DestinationCityNode { get; set; }
    }
}