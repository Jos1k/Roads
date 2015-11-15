using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc.Html;
using System.Xml.Linq;
using Roads.Web.Mvc.Models.Enums;
using Roads.Web.Mvc.Models.ExportModels;
using Roads.Web.Mvc.RoadsServiceClient;
using Roads.Web.Mvc.Services;

namespace Roads.Web.Mvc.Integrations
{
    public abstract class ImportFileProviderBase : IImportFileProvider
    {
        #region Protected Fileds

        protected int RawItemsCount;
        protected int ActualItemsCount;

        #endregion

        #region IImportFileProvider Implementation

        /// <summary>
        /// Gets or sets the report.
        /// </summary>
        /// <value>
        /// The report.
        /// </value>
        public string Report { get; protected set; }

        /// <summary>
        /// Updates specific data in db.
        /// </summary>
        /// <param name="client">WCF client.</param>
        /// <param name="file">The file.</param>
        /// <param name="dataType">Type of the data.</param>
        public void Update(IRoadsService client, HttpPostedFileBase file, ExportDataType dataType)
        {
            ResetReport(); // sets counts values to defaults.
            switch (dataType)
            {
                case ExportDataType.Dynamic:
                    UpdateData(client, GetValidTranslationObjects(ParseFile<DynamicTranslationExport>(file, dataType)));
                    break;

                case ExportDataType.Static:
                    UpdateData(client, GetValidTranslationObjects(ParseFile<StaticTranslationExport>(file, dataType)));
                    break;

                case ExportDataType.MapObject:
                    UpdateData(client, GetValidTranslationObjects(GetMapObjectTranslationDataListFromFile(file)));
                    break;

                default:
                    Report = RoadsExtensionMethods.GetLabel("ERP_Label_Error");
                    break;
            }
            BuildReport();
        }

        #endregion

        #region Abstract Members

        /// <summary>
        /// Parses the file.
        /// </summary>
        /// <typeparam name="T">Type of items element.</typeparam>
        /// <param name="file">The file for upload.</param>
        /// <param name="dataType">Type of data.</param>
        /// <returns>
        /// The IEnumerable of <see cref="T" />.
        /// </returns>
        protected abstract IEnumerable<T> ParseFile<T>(HttpPostedFileBase file, ExportDataType dataType);

        #endregion

        #region Private Methods

        // Verifies the static objects.
        private IEnumerable<StaticTranslationExport> GetValidTranslationObjects(IEnumerable<StaticTranslationExport> input)
        {
            using (var client = new RoadsServiceClient.RoadsServiceClient())
            {
                StaticTranslationData[] allStatic = client.GetAllStaticTranslations();
                IEnumerable<int> languagesIds = CultureHelper.Languages.Select(l => l.LanguageId);

                //check for validity of DynamicKey.
                List<StaticTranslationExport> validObjects = input.Where(d => allStatic.Any(o => o.EnumKey == d.EnumKey)).ToList();

                //check for validity of LanguageId.
                validObjects = validObjects.Where(d => languagesIds.Any(l => l.Equals(d.LanguageId))).ToList();

                ActualItemsCount = validObjects.Count();
                return validObjects;
            }
        }


        // Verifies the dynamic objects.  
        private IEnumerable<DynamicTranslationExport> GetValidTranslationObjects(IEnumerable<DynamicTranslationExport> input)
        {
            using (var client = new RoadsServiceClient.RoadsServiceClient())
            {
                DynamicTranslationsData[] allDynamic = client.GetAllDynamicTranslationsData();
                List<int> languagesIds = CultureHelper.Languages.Select(l => l.LanguageId).ToList();

                //check for validity of DynamicKey.
                List<DynamicTranslationExport> validObjects = input.Where(d => allDynamic.Any(o => o.DynamicKey == d.DynamicKey)).ToList();

                //check for validity of LanguageId.
                validObjects = validObjects.Where(d => languagesIds.Any(l => l.Equals(d.LanguageId))).ToList();

                ActualItemsCount = validObjects.Count();
                return validObjects;
            }
        }

        // Verifies the map objects.  
        private IEnumerable<MapObjectTranslationData> GetValidTranslationObjects(IEnumerable<MapObjectTranslationData> input)
        {
            List<int> languagesIds = CultureHelper.Languages.Select(l => l.LanguageId).ToList();

            //check for validity of DynamicKey.
            List<MapObjectTranslationData> validObjects = input.Where(d => languagesIds.Any(l => l.Equals(d.LanguageId))).ToList();

            ActualItemsCount = validObjects.Count();
            return validObjects;
        }

        /// <summary>
        /// Gets the list from file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>The list of <see cref="MapObjectTranslationData"/>.</returns>
        private List<MapObjectTranslationData> GetMapObjectTranslationDataListFromFile(HttpPostedFileBase file)
        {
            var document = XDocument.Load(new StreamReader(file.InputStream, Encoding.Default));

            return
            document.Descendants("MapObjectTranslationData").Select(d =>
            {
                var xMapObjectId = d.Element("MapObjectId");
                var xCityId = d.Element("CityNodeId");
                var xRegionId = d.Element("RegionNodeId");
                var xLanguageId = d.Element("LanguageId");
                var xTranslationKey = d.Element("LanguageKey");
                var xValue = d.Element("Value");
                long mapObjectId;
                int cityId;
                int regionId;
                int languageId;
                return (xMapObjectId != null &&
                        xValue != null &&
                        xCityId != null &&
                        xRegionId != null &&
                        xLanguageId != null &&
                        xTranslationKey != null &&
                        long.TryParse(xMapObjectId.Value, out mapObjectId) &&
                        int.TryParse(xLanguageId.Value, out languageId) &&
                        int.TryParse(xCityId.Value, out cityId) &&
                        int.TryParse(xRegionId.Value, out regionId))
                    ? new MapObjectTranslationData
                    {
                        MapObjectId = mapObjectId,
                        CityNodeId = cityId,
                        RegionNodeId = regionId,
                        LanguageId = languageId,
                        LanguageKey = xTranslationKey.Value,
                        Value = xValue.Value
                    }
                    : new MapObjectTranslationData();
            })
            .ToList();
        }

        // Updates the static objects.
        private void UpdateData(IRoadsService client, IEnumerable<StaticTranslationExport> data)
        {
            client.UpdateStaticTranslation(data.Select(staticTranslations =>
                new StaticTranslationData
                {
                    Value = staticTranslations.Value,
                    EnumKey = staticTranslations.EnumKey,
                    LanguageId = staticTranslations.LanguageId
                }).ToArray());
        }

        // Updates the dynamic objects.
        private void UpdateData(IRoadsService client, IEnumerable<DynamicTranslationExport> data)
        {
            client.UpdateDynamicTranslations(data.Select(dynamicTranslations =>
                new DynamicTranslationsData
                {
                    Value = dynamicTranslations.Value,
                    DynamicKey = dynamicTranslations.DynamicKey,
                    DescriptionValue = dynamicTranslations.DescriptionValue,
                    LanguageId = dynamicTranslations.LanguageId
                }).ToArray());
        }

        // Updates the map objects.
        private void UpdateData(IRoadsService client, IEnumerable<MapObjectTranslationData> data)
        {
            client.ProcessImportMapObject(data.Select(mapObject =>
                new MapObjectTranslationData
                {
                    MapObjectId = mapObject.MapObjectId,
                    CityNodeId = mapObject.CityNodeId,
                    RegionNodeId = mapObject.RegionNodeId,
                    LanguageId = mapObject.LanguageId,
                    LanguageKey = mapObject.LanguageKey,
                    Value = mapObject.Value
                }).ToArray());
        }

        private void BuildReport()
        {
            int failedCount = RawItemsCount - ActualItemsCount;

            string labelAll = RoadsExtensionMethods.GetLabel("ETP_Report_All");
            string labelSuccess = RoadsExtensionMethods.GetLabel("ETP_Report_Success");
            string labelFailed = RoadsExtensionMethods.GetLabel("ETP_Report_Failed");

            Report = String.Format("{0}:{1}  {2}:{3}  {4}:{5}", labelAll, RawItemsCount, 
                                                                labelSuccess, ActualItemsCount,
                                                                labelFailed, failedCount);
        }

        private void ResetReport()
        {
            Report = String.Empty;
            RawItemsCount = default(int);
            ActualItemsCount = default(int);
        }

        #endregion
    }
}