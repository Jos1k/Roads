using System.Web;
using Roads.Web.Mvc.Models.Enums;
using Roads.Web.Mvc.RoadsServiceClient;

namespace Roads.Web.Mvc.Integrations
{
    public interface IImportFileProvider
    {
        /// <summary>
        /// Gets or sets the report.
        /// </summary>
        /// <value>
        /// The report.
        /// </value>
        string Report { get; }

        /// <summary>
        /// Updates the specified file.
        /// </summary>
        /// <param name="client">WCF client.</param>
        /// <param name="file">The file.</param>
        /// <param name="dataType">Type of the data.</param>
        /// <returns></returns>
        void Update(IRoadsService client, HttpPostedFileBase file, ExportDataType dataType);
    }
}
