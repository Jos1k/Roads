namespace Roads.Web.Mvc.Models.ExportModels
{
    public class DynamicTranslationExport
    {
        public string DynamicKey { get; set; }   
        public string Value { get; set; }
        public string DescriptionValue { get; set; }
        public int LanguageId { get; set; }
        public string LanguageName { get; set; }
    }
}