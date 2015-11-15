using System.Linq;
using Roads.Web.Mvc.Services;

namespace System.Web.Mvc.Html
{
    public static class RoadsExtensionMethods
    {
        private const string _noTextString = "No text";

        /// <summary>
        /// Gets the label. This method using on Views.
        /// </summary>
        /// <param name="helper">Html helper</param>
        /// <param name="id">The identifier of label.</param>
        /// <returns>Returns translated text.</returns>
        public static string GetLabel(this HtmlHelper helper, string id)
        {
            return GetLabel(id);
        }

        /// <summary>
        /// Gets the city name separation text.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="text">The text.</param>
        /// <param name="isName">if set to <c>true</c> [is name].</param>
        /// <returns></returns>
        public static string GetCityNameSeparationtext(this HtmlHelper helper, string text, bool isName)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            string[] cityElements = text.Split(',');

            if (cityElements.Count() > 1)
            {
                if (isName)
                {
                    return cityElements[0];
                }

                return cityElements[1];
            }

            return text;
        }


        /// <summary>
        /// Buils star control.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="labelId">The label identifier.</param>
        /// <param name="value">The value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <returns>Return html code of star control.</returns>
        public static MvcHtmlString GetBuilsStarControl(this HtmlHelper helper, string labelId, int value, int maxValue)
        {
            string result = "<label>" + GetLabel(labelId) + "</label>";

            for (int i = 0; i < maxValue; i++)
            {
                if (i < value)
                {
                    result = result + "<i class='starGold fa fa-star'></i>";
                }
                else
                {
                    result = result + "<i class='fa fa-star-o'></i>";
                }
            }
            return MvcHtmlString.Create(result);
        }

        /// <summary>
        /// Buils star control.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="labelId">The label identifier.</param>
        /// <param name="value">The value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <returns>Return html code of star control.</returns>
        public static MvcHtmlString GetBuilsStarControlWithoutLabel(this HtmlHelper helper,int value, int maxValue)
        {
            string result = String.Empty;

            for (int i = 0; i < maxValue; i++)
            {
                if (i < value)
                {
                    result = result + "<i class='starGold fa fa-star'></i>";
                }
                else
                {
                    result = result + "<i class='fa fa-star-o'></i>";
                }
            }
            return MvcHtmlString.Create(result);
        }


        /// <summary>
        ///     Gets the label. This method using in Models and Controllers.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Translated text.</returns>
        public static string GetLabel(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return _noTextString;
            }

            try
            {
                var manager = new TranslationManager();
                string label = manager.GetLabelTranslation(id, CultureHelper.GetCurrentNeutralCulture());

                return String.IsNullOrEmpty(label) ? _noTextString : label;
            }
            catch (Exception ex)
            {
                return _noTextString;
            }
        }

        /// <summary>
        /// Gets the dynamic resource. This method using on Views.
        /// </summary>
        /// <param name="helper">The html helper.</param>
        /// <param name="id">The identifier of resource.</param>
        /// <returns>Returns translated text.</returns>
        public static string GetDynamicResource(this HtmlHelper helper, string id)
        {
            return GetDynamicResource(id);
        }

        /// <summary>
        /// Gets the dynamic resource. This method using in Models and Controllers.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Returns translated text.</returns>
        public static string GetDynamicResource(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return _noTextString;
            }

            try
            {
                var manager = new TranslationManager();
                string resource = manager.GetDynamicTranslation(id, CultureHelper.GetCurrentNeutralCulture());

                return String.IsNullOrEmpty(resource) ? _noTextString : resource;
            }
            catch (Exception ex)
            {
                return _noTextString;
            }
        }
    }
}