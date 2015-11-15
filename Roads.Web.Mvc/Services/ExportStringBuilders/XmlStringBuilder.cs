using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Roads.Web.Mvc.Integrations;

namespace Roads.Web.Mvc.Services.ExportStringBuilders
{
    public class XmlStringBuilder : ExportStringBuilderBase
    {
        /// <summary>
        /// Creates the XML export string.
        /// </summary>
        /// <typeparam name="T">Concrete type of data.</typeparam>
        /// <param name="dataList">The data list.</param>
        /// <returns>The XML export string.</returns>
        protected override string CreateExportString<T>(IEnumerable<T> dataList)
        {
            using (var writer = new StringWriter())
            {
                var xml = new XmlSerializer(dataList.GetType());
                xml.Serialize(writer, dataList);
                return writer.ToString();
            }
        }
    }
}