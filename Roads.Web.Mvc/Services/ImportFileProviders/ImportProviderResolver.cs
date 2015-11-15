using System.Web;
using Roads.Web.Mvc.Integrations;

namespace Roads.Web.Mvc.Services.ImportFileProviders
{
    public static class ImportProviderResolver
    {
        /// <summary>
        /// Resolves the import provider.
        /// </summary>
        /// <param name="file">The import file.</param>
        /// <returns>Specific import provider which implements IImportFileProvider or null.</returns>
        public static IImportFileProvider ResolveImportProvider(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                if (file.ContentType == "text/xml" || file.ContentType == "application/xml")
                {
                    return new ImportXmlProvider();
                }
                if (file.ContentType == "text/csv" || file.ContentType == "application/csv")
                {
                    return new ImportCsvProvider();
                }
                return null;
            }
            return null;
        }
    }
}