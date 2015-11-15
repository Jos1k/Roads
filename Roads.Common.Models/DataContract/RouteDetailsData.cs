using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Roads.Common.Models.DataContract
{
    /// <summary>
    /// Model for collecting details about route nodes
    /// </summary>
    [DataContract]
    public class RouteDetailsData
    {
        [DataMember]
        public List<List<RouteDetailsFeedback>> RouteDetailsItems = new List<List<RouteDetailsFeedback>>();
    }
}
