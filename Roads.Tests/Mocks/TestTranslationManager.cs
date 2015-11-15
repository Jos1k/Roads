using Roads.Web.Mvc.Services;

namespace Roads.Tests.Mocks
{
    /// <summary>
    /// Mock of TranslationManager
    /// </summary>
    public class TestTranslationManager : ITranslationManager
    {
        public string GetLabelTranslation(string labelId, string userLangusge)
        {
            switch (labelId)
            {
                case "ARS1_Placeholder_From":
                    return "From";

                case "ARS1_Placeholder_To":
                    return "To";
                default:
                    return "Not Found";
            }
        }

        public string GetDynamicTranslation(string labelId, string userLanguage)
        {
            throw new System.NotImplementedException();
        }
    }
}