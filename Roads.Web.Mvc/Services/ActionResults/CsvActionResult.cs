using System;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Roads.Web.Mvc.Services.ActionResults
{
    public class CsvActionResult : ActionResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CsvActionResult"/> class.
        /// </summary>
        /// <param name="report">The report.</param>
        /// <param name="fileName">Name of the file.</param>
        public CsvActionResult(string report, string fileName = null)
        {
            FileName = fileName;
            Report = report;
        }

        public string Report { get; private set; }
        public string FileName { get; private set; }

        /// <summary>
        /// Enables processing of the result of an action method by a custom type that inherits from the <see cref="T:System.Web.Mvc.ActionResult" /> class.
        /// </summary>
        /// <param name="context">The context in which the result is executed. The context information includes the controller, HTTP content, request context, and route data.</param>
        public override void ExecuteResult(ControllerContext context)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "text/csv";
            HttpContext.Current.Response.BufferOutput = true;

            if (String.IsNullOrEmpty(FileName))
            {
                FileName = DateTime.Now.ToString("yyyy.MM.dd_hh.mm");
            }
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + FileName + ".csv");

            HttpContext.Current.Response.ContentEncoding = Encoding.Default;
            HttpContext.Current.Response.Charset = "utf-16";
            HttpContext.Current.Response.Write(Report);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
    }
}