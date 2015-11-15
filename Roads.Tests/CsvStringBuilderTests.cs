using System;
using System.Collections.Generic;
using NUnit.Framework;
using Roads.Tests.Helpers;
using Roads.Web.Mvc.Integrations;
using Roads.Web.Mvc.Models.ExportModels;
using Roads.Web.Mvc.Services.ExportStringBuilders;

namespace Roads.Tests
{
    [TestFixture]
    class CsvStringBuilderTests
    {
        private List<StaticTranslationExport> _staticTranslation;
        private List<DynamicTranslationExport> _dynamicTranslation;

        [TestFixtureSetUp]
        public void Initialize()
        {
            var helper = new ExportImportHelper();
            _dynamicTranslation = helper.DynamicExportDatalist;
            _staticTranslation = helper.StaticExportDataList;
        }

        [Test]
        public void CsvStringBuilder_GetExportString_SendDynamicDataList_ExpectedSpecificString()
        {
            //arrange
            IExportBuilder csvExportBuilder = new CsvStringBuilder();
            var expected = "DynamicKey,Value,DescriptionValue,LanguageId,LanguageName\r\nd23,value1d,desc1,1,uk\r\nd24,value2d,desc2,2,en\r\nd25,value3d,desc3,3,ru\r\n";

            //act
            var actual = csvExportBuilder.GetExportString(_dynamicTranslation);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CsvStringBuilder_GetExportString_SendStaticDataList_ExpectedSpecificString()
        {
            //arrange
            IExportBuilder csvExportBuilder = new CsvStringBuilder();
            var expected = "EnumKey,Value,LanguageId,LanguageName\r\n\"a23,67\",value1,1,uk\r\n\"a24\"\"\",value2,2,en\r\na25;,value3,3,ru\r\n";

            //act
            var actual = csvExportBuilder.GetExportString(_staticTranslation);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CsvStringBuilder_GetExportString_SendNullWithStringType_ExpectedException()
        {
            //arrange
            IExportBuilder csvExportBuilder = new CsvStringBuilder();
            var expected = String.Empty;

            //act
            var actual = csvExportBuilder.GetExportString<String>(null);

            //assert
            Assert.AreEqual(expected, actual);
        }
    }
}
