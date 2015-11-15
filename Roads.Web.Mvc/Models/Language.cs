using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Roads.Web.Mvc.Models
{
    public class Language
    {
        public int LanguageId { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
    }
}