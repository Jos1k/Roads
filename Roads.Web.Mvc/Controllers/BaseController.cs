using System;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using Roads.Web.Mvc.Services;

namespace Roads.Web.Mvc.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// Begins to invoke the action in the current controller context.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <param name="state">The state.</param>
        /// <returns>
        /// Returns an IAsyncController instance.
        /// </returns>
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            // Attempt to read the culture value from Request
            var cultureName = RouteData.Values["culture"] as string ??
                              (Request.UserLanguages != null && Request.UserLanguages.Length > 0
                ? Request.UserLanguages[0]
                : null);

            // Validate culture name
            cultureName = CultureHelper.GetCulture(cultureName); // This is safe

            if (RouteData.Values["culture"] as string != cultureName)
            {
                // Force a valid culture in the URL
                RouteData.Values["culture"] = cultureName.ToLowerInvariant(); // lower case too

                // Redirect user
                Response.RedirectToRoute(RouteData.Values);
            }

            // Modify current thread's cultures           
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            return base.BeginExecuteCore(callback, state);
        }
    }
}