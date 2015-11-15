using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Roads.Web.Mvc.Services
{
    public static class SwitchLanguageHelper
    {
        /// <summary>
        /// Html helper which create a specific selector link for language switching.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="cultureName">Name of the culture.</param>
        /// <param name="selectedText">The text for selected language.</param>
        /// <param name="unselectedText">The text for unselected language.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <param name="languageRouteName">Name of the specific localization route.</param>
        /// <param name="strictSelected">if set to <c>true</c> [strict selected].</param>
        /// <returns>The link as <see cref="MvcHtmlString"/></returns>
        public static MvcHtmlString LanguageSelectorLink(this HtmlHelper helper,
                                                              string cultureName,
                                                              string selectedText,
                                                              string unselectedText,
                                                              IDictionary<string, object> htmlAttributes,
                                                              string languageRouteName = "culture",
                                                              bool strictSelected = false)
        {
            Language language = helper.LanguageUrl(cultureName, languageRouteName, strictSelected);

            MvcHtmlString link = helper.RouteLink(language.IsSelected ? selectedText : unselectedText,
                "Localization", language.RouteValues, htmlAttributes);

            return link;
        }

        /// <summary>
        /// Languages the action link.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="cultureName">Name of the culture.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <param name="languageRouteName">Name of the language route.</param>
        /// <param name="strictSelected">if set to <c>true</c> [strict selected].</param>
        /// <returns>The link as <see cref="MvcHtmlString"/></returns>
        public static MvcHtmlString LanguageActionLink(this HtmlHelper helper,
                                                              string cultureName,
                                                              IDictionary<string, object> htmlAttributes,
                                                              string languageRouteName = "culture",
                                                              bool strictSelected = false)
        {
            Language language = helper.LanguageUrl(cultureName, languageRouteName, strictSelected);

            MvcHtmlString link = helper.ActionLink(cultureName,
                "Localization", language.RouteValues, htmlAttributes);

            return link;
        }

        /// <summary>
        /// Determines whether [is selected culture] [the specified culture name].
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="cultureName">Name of the culture.</param>
        /// <returns>Bool value</returns>
        public static bool IsSelectedCulture(this HtmlHelper helper, string cultureName)
        {
            return helper.LanguageUrl(cultureName).IsSelected;
        }

        /// <summary>
        /// Cultures the URL.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="cultureName">Name of the culture.</param>
        /// <returns>String Url.</returns>
        public static string CultureUrl(this HtmlHelper helper, string cultureName)
        {
            return helper.LanguageUrl(cultureName).Url;
        }

        private static Language LanguageUrl(this HtmlHelper helper, 
                                                 string cultureName,
                                                 string languageRouteName = "culture",
                                                 bool strictSelected = false)
        {
            // set the input language to lower
            cultureName = cultureName.ToLower();

            // retrieve the route values from the view context
            var routeValues = new RouteValueDictionary(helper.ViewContext.RouteData.Values);

            // copy the query strings into the route values to generate the link
            NameValueCollection queryString = helper.ViewContext.HttpContext.Request.QueryString;

            foreach (string key in queryString)
            {
                if (queryString[key] != null && !string.IsNullOrWhiteSpace(key))
                {
                    if (routeValues.ContainsKey(key))
                    {
                        routeValues[key] = queryString[key];
                    }

                    else
                    {
                        routeValues.Add(key, queryString[key]);
                    }
                }
            }

            string actionName = routeValues["action"].ToString();
            string controllerName = routeValues["controller"].ToString();

            // set the language into route values
            routeValues[languageRouteName] = cultureName;

            // generate the language specify url
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext, helper.RouteCollection);
            string url = urlHelper.RouteUrl("Localization", routeValues);

            // check whether the current thread ui culture is this language
            string currentLangName = Thread.CurrentThread.CurrentUICulture.Name.ToLower();
            bool isSelected = strictSelected
                ? currentLangName == cultureName
                : currentLangName.StartsWith(cultureName);

            return new Language
            {
                Url = url,
                ActionName = actionName,
                ControllerName = controllerName,
                RouteValues = routeValues,
                IsSelected = isSelected
            };
        }

        private class Language
        {
            public string Url { get; set; }
            public string ActionName { get; set; }
            public string ControllerName { get; set; }
            public RouteValueDictionary RouteValues { get; set; }
            public bool IsSelected { get; set; }

            public MvcHtmlString HtmlSafeUrl
            {
                get { return MvcHtmlString.Create(Url); }
            }
        }
    }
}