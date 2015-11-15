using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Roads.Web.Mvc.Integrations;
using Roads.Web.Mvc.Models.Enums;
using Roads.Web.Mvc.Models.ExportModels;
using Roads.Web.Mvc.RoadsServiceClient;

namespace Roads.Web.Mvc.Services
{
    public class ExportDataProvider
    {
        #region Private Fields

        private List<DynamicTranslationExport> _dynamicTranslations;
        private List<StaticTranslationExport> _staticTranslations;
        private readonly IExportBuilder _exportStringBuilder;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportDataProvider"/> class.
        /// </summary>
        /// <param name="exportStringBuilder">The export string builder.</param>
        public ExportDataProvider(IExportBuilder exportStringBuilder)
        {
            _exportStringBuilder = exportStringBuilder;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates the export data string.
        /// </summary>
        /// <param name="client">IRoadsService client object(WCF).</param>
        /// <param name="exportDataType">Type of the export data.</param>
        /// <returns>Specific output string.</returns>
        public string CreateExportData(IRoadsService client, ExportDataType exportDataType)
        {
            switch (exportDataType)
            {
                case ExportDataType.Dynamic:

                    _dynamicTranslations = new List<DynamicTranslationExport>();
                    var dynamicTranslationsData = client.GetAllDynamicTranslationsData().ToList();

                    foreach (var data in dynamicTranslationsData)
                    {
                        _dynamicTranslations.Add(new DynamicTranslationExport()
                        {
                            DynamicKey = data.DynamicKey,
                            Value = data.Value,
                            DescriptionValue = data.DescriptionValue,
                            LanguageName = data.Lenguage.Name,
                            LanguageId = data.LanguageId
                        });
                    }
                    return _exportStringBuilder.GetExportString(_dynamicTranslations);

                case ExportDataType.Static:

                    _staticTranslations = new List<StaticTranslationExport>();
                    var staticTranslationsData = client.GetAllStaticTranslations().ToList();

                    foreach (var data in staticTranslationsData)
                    {
                        _staticTranslations.Add(new StaticTranslationExport()
                        {
                            EnumKey = data.EnumKey,
                            Value = data.Value,
                            LanguageName = data.Language.Name,
                            LanguageId = data.LanguageId
                        });
                    }
                    return _exportStringBuilder.GetExportString(_staticTranslations);

                case ExportDataType.MapObject:

                    return MapObjectsExportData(client);

                default:
                    return String.Empty;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Maps the objects export data.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <returns>the string value.</returns>
        private string MapObjectsExportData(IRoadsService client)
        {
            try
            {
                return _exportStringBuilder.GetExportString(client.GetExportedMapObjectTranslationData());
            }
            catch (Exception)
            {
                return string.Empty;
            } 
        }

        #endregion
    }
}