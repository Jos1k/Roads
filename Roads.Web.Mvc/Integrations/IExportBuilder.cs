using System.Collections.Generic;

namespace Roads.Web.Mvc.Integrations
{
    public interface IExportBuilder
    {
        /// <summary>
        /// Gets the export string.
        /// </summary>
        /// <typeparam name="T">Concrete data type.</typeparam>
        /// <param name="dataList">The data list.</param>
        /// <returns>The specific export string.</returns>
        string GetExportString<T>(IEnumerable<T> dataList);
    }
}
