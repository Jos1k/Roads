using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Roads.Web.Mvc.Integrations;
using Roads.Web.Mvc.Models.Enums;
using Roads.Web.Mvc.Models.ExportModels;

namespace Roads.Web.Mvc.Services.ImportFileProviders
{
    public class ImportCsvProvider: ImportFileProviderBase
    {
        #region Private Constants

        private const string Quotes = "\"";
        private const string Comma = ",";
        private const char CharQuotes = '"';
        private const char CharComma = ',';

        #endregion

        #region Override Methods

        /// <summary>
        /// Parses the .csv file.
        /// </summary>
        /// <typeparam name="T">Type of items element.</typeparam>
        /// <param name="file">The file for upload.</param>
        /// <param name="dataType">Type of data.</param>
        /// <returns>
        /// The IEnumerable of <see cref="T" />.
        /// </returns>
        protected override IEnumerable<T> ParseFile<T>(HttpPostedFileBase file, ExportDataType dataType)
        {
            var reader = new StreamReader(file.InputStream, Encoding.Default);
            IEnumerable<List<string>> dataList = GetDataList(reader, out RawItemsCount);

            switch (dataType)
            {
                case ExportDataType.Dynamic:
                    List<DynamicTranslationExport> dynamicInput = dataList.Select(e =>
                    {
                        var xDynamicKey = e[0];
                        var xValue = e[1];
                        var xDescriptionValue = e[2];
                        var xLanguageId = e[3];
                        int languageId;

                        return (xValue != null &&
                                xDescriptionValue != null &&
                                xDynamicKey != null &&
                                xLanguageId != null &&
                                int.TryParse(xLanguageId, out languageId))
                            //check for languageId is number          
                            ? new DynamicTranslationExport
                            {
                                DynamicKey = xDynamicKey,
                                Value = xValue,
                                DescriptionValue = xDescriptionValue,
                                LanguageId = languageId
                            }
                            : new DynamicTranslationExport();
                    }).ToList();
                    return dynamicInput as List<T>;

                case ExportDataType.Static:
                    List<StaticTranslationExport> staticInput = dataList.Select(e =>
                    {
                        var xEnumKey = e[0];
                        var xValue = e[1];
                        var xLanguageId = e[2];
                        int languageId;

                        return (xValue != null &&
                                xEnumKey != null &&
                                xLanguageId != null &&
                                int.TryParse(xLanguageId, out languageId))
                            //check for languageId is number          
                            ? new StaticTranslationExport
                            {
                                EnumKey = xEnumKey,
                                Value = xValue,
                                LanguageId = languageId
                            }
                            : new StaticTranslationExport();
                    }).ToList();

                    return staticInput as List<T>;

                default:
                    RawItemsCount = default(int);
                    return default(IEnumerable<T>);
            }
        }

        #endregion

        #region Private Methods

        private IEnumerable<List<string>> GetDataList(StreamReader reader, out int count)
        {
            var rawData = new List<List<string>>();
            count = -1; // 0 is headers
            while (reader.Peek() != -1)
            {
                count++;
                var outputLine = new List<string>();
                var line = reader.ReadLine();
                while (!string.IsNullOrEmpty(line))
                {
                    outputLine.Add(GetNextElement(ref line));
                }
                rawData.Add(outputLine);
            }
            return rawData;
        }

        private string GetNextElement(ref string text)
        {
            var index = text.StartsWith(Quotes)
                ? text.IndexOf(Quotes, 1, StringComparison.Ordinal)
                : text.IndexOf(Comma, StringComparison.Ordinal);

            string element;

            if (index == -1)
            {
                element = text;
                text = string.Empty;
            }
            else
            {
                element = text.Substring(0, index).Trim(CharQuotes);
                text = text.Substring(index + 1).TrimStart(CharComma);
            }

            return element;
        }

        #endregion
    }
}