using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Roads.Web.Mvc.Controllers
{
    public class ErrorController : BaseController
    {
        public ActionResult Error(int statusCode, Exception exception)
        {
            Response.TrySkipIisCustomErrors = true;
            Response.StatusCode = statusCode;
            ViewBag.ErrorCode = statusCode;
            ViewBag.Description = exception.Message;
            return View();
        }

    }
}
