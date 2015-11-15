using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;
using Roads.Common.Models.DataContract;
using Roads.Web.Mvc.Integrations;
using Roads.Web.Mvc.Models.Enums;
using Roads.Web.Mvc.Models.ExportModels;

namespace Roads.Web.Mvc.Services.ImportFileProviders
{
    public class ImportXmlProvider: ImportFileProviderBase
    {
        /// <summary>
        /// Parses the file.
        /// </summary>
        /// <typeparam name="T">Type of items element.</typeparam>
        /// <param name="file">The file for upload.</param>
        /// <param name="dataType">Type of data.</param>
        /// <returns>
        /// The IEnumerable of <see cref="T" />.
        /// </returns>
        protected override IEnumerable<T> ParseFile<T>(HttpPostedFileBase file, ExportDataType dataType)  
        {
            var document = XDocument.Load(new StreamReader(file.InputStream, Encoding.Default));
            
            switch (dataType)
            {
                case ExportDataType.Dynamic:

                    RawItemsCount = document.Descendants("DynamicTranslationExport").Count();

                    List<DynamicTranslationExport> dynamicInput =
                        document.Descendants("DynamicTranslationExport")
                            .Select(d =>
                            {
                                var xDynamicKey = d.Element("DynamicKey");
                                var xValue = d.Element("Value");
                                var xDescriptionValue = d.Element("DescriptionValue");
                                var xLanguageId = d.Element("LanguageId");
                                int languageId;

                                return (xValue != null &&
                                        xDescriptionValue != null &&
                                        xDynamicKey != null &&
                                        xLanguageId != null &&
                                        int.TryParse(xLanguageId.Value, out languageId))
                                    //check for languageId is number          
                                    ? new DynamicTranslationExport
                                    {
                                        DynamicKey = xDynamicKey.Value,
                                        Value = xValue.Value,
                                        DescriptionValue = xDescriptionValue.Value,
                                        LanguageId = languageId
                                    }
                                    : new DynamicTranslationExport();
                            })
                            .ToList();
                    return dynamicInput as List<T>;

                case ExportDataType.Static:

                    RawItemsCount = document.Descendants("StaticTranslationExport").Count();

                    List<StaticTranslationExport> staticInput =
                        document.Descendants("StaticTranslationExport")
                            .Select(d =>
                            {
                                var xEnumKey = d.Element("EnumKey");
                                var xValue = d.Element("Value");
                                var xLanguageId = d.Element("LanguageId");
                                int languageId;

                                return (xValue != null &&
                                        xEnumKey != null &&
                                        xLanguageId != null &&
                                        int.TryParse(xLanguageId.Value, out languageId))
                                    //check for languageId is number
                                    ? new StaticTranslationExport
                                    {
                                        EnumKey = xEnumKey.Value,
                                        Value = xValue.Value,
                                        LanguageId = languageId
                                    }
                                    : new StaticTranslationExport();
                            })
                            .ToList();
                    return staticInput as List<T>;

                default:
                    RawItemsCount = default(int);
                    return default(IEnumerable<T>);
            }
        }
    }
}