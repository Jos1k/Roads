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
    
    public partial class MapObjectTranslation
    {
        public long MapObjectId { get; set; }
        public string Value { get; set; }
        public int LanguageId { get; set; }
        public string LanguageKey { get; set; }
    
        public virtual Language Language { get; set; }
    }
}
