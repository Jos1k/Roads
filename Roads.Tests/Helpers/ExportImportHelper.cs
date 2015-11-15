using System.Collections.Generic;
using Roads.Web.Mvc.Models.ExportModels;

namespace Roads.Tests.Helpers
{
    /// <summary>
    /// Helper class which provide mock data for tests.
    /// </summary>
    public class ExportImportHelper
    {
        public ExportImportHelper()
        {
            Initialize();
        }

        public List<StaticTranslationExport> StaticExportDataList { get; private set; }

        public List<DynamicTranslationExport> DynamicExportDatalist { get; private set; }

        public void Initialize()
        {
            StaticExportDataList = new List<StaticTranslationExport>
            {
                new StaticTranslationExport
                {
                    EnumKey = "a23,67",
                    LanguageId = 1,
                    LanguageName = "uk",
                    Value = "value1"
                },
                new StaticTranslationExport
                {
                    EnumKey = "a24\"",
                    LanguageId = 2,
                    LanguageName = "en",
                    Value = "value2"
                },
                new StaticTranslationExport
                {
                    EnumKey = "a25;",
                    LanguageId = 3,
                    LanguageName = "ru",
                    Value = "value3"
                }
            };

            DynamicExportDatalist = new List<DynamicTranslationExport>
            {
                new DynamicTranslationExport
                {
                    DescriptionValue = "desc1",
                    DynamicKey = "d23",
                    LanguageId = 1,
                    LanguageName = "uk",
                    Value = "value1d"
                },
                new DynamicTranslationExport
                {
                    DescriptionValue = "desc2",
                    DynamicKey = "d24",
                    LanguageId = 2,
                    LanguageName = "en",
                    Value = "value2d"
                },
                new DynamicTranslationExport
                {
                    DescriptionValue = "desc3",
                    DynamicKey = "d25",
                    LanguageId = 3,
                    LanguageName = "ru",
                    Value = "value3d"
                }
            };
        }
    }
}
