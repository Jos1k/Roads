using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Roads.Web.Mvc.Integrations;

namespace Roads.Web.Mvc.Services.ExportStringBuilders
{
    public class CsvStringBuilder : ExportStringBuilderBase
    {
        /// <summary>
        /// Creates the export string.
        /// </summary>
        /// <typeparam name="T">Concrete type of data.</typeparam>
        /// <param name="dataList">The data list.</param>
        /// <returns>
        /// The specific export string.
        /// </returns>
        protected override string CreateExportString<T>(IEnumerable<T> dataList)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            var result = new StringBuilder();

            //adds headers
            foreach (PropertyInfo propertyInfo in properties)
            {
                result.Append(propertyInfo.Name).Append(",");
            }
            result.Remove(result.Length - 1, 1).AppendLine();

            //fills the spreadsheet
            var lines = dataList.Select(row => properties
                .Select(p => p.GetValue(row, null))
                .Select(v => StringToCsvCell(Convert.ToString(v))))
                .Select(values => string.Join(",", values));

            foreach (string line in lines)
            {
                result.AppendLine(line);
            }
            return result.ToString();
        }

        /// <summary>
        /// Strings to CSV cell.
        /// </summary>
        /// <param name="str">The specific string.</param>
        /// <returns>Csv cell as string.</returns>
        protected string StringToCsvCell(string str)
        {
            bool mustQuote = (str.Contains(",") || 
                              str.Contains("\"") ||
                              str.Contains("\r") || 
                              str.Contains("\n"));
            if (mustQuote)
            {
                var sb = new StringBuilder();
                sb.Append("\"");

                foreach (char nextChar in str)
                {
                    sb.Append(nextChar);
                    if (nextChar == '"')
                    {
                        sb.Append("\"");
                    }
                }
                sb.Append("\"");
                return sb.ToString();
            }
            return str;
        }
    }
}