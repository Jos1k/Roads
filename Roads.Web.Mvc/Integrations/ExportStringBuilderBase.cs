using System;
using System.Collections.Generic;

namespace Roads.Web.Mvc.Integrations
{
    public abstract class ExportStringBuilderBase : IExportBuilder
    {
        /// <summary>
        /// Gets the export string.
        /// </summary>
        /// <typeparam name="T">Concrete data type.</typeparam>
        /// <param name="dataList">The data list.</param>
        /// <returns>
        /// The specific export string.
        /// </returns>
        public string GetExportString<T>(IEnumerable<T> dataList)
        {
            try
            {
                return CreateExportString<T>(dataList);
            }
            catch (Exception e)
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// Creates the export string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataList">The data list.</param>
        /// <returns>The specific export string.</returns>
        protected abstract string CreateExportString<T>(IEnumerable<T> dataList);
    }
}