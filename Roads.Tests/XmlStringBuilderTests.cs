using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using NUnit.Framework;
using Roads.Tests.Helpers;
using Roads.Web.Mvc.Integrations;
using Roads.Web.Mvc.Models.ExportModels;
using Roads.Web.Mvc.Services.ExportStringBuilders;

namespace Roads.Tests
{
    [TestFixture]
    public class XmlStringBuilderTests
    {
        #region SetUp

        private List<StaticTranslationExport> _staticTranslation;
        private List<DynamicTranslationExport> _dynamicTranslation;

        [TestFixtureSetUp]
        public void Initialize()
        {
            var helper = new ExportImportHelper();
            _dynamicTranslation = helper.DynamicExportDatalist;
            _staticTranslation = helper.StaticExportDataList;
        }

        #endregion

        [Test]
        public void XmlStringBuilder_CreateExportString_SendStaticDataList_EqualsToSpecificStringValue()
        {
            //arrange
            IExportBuilder xmlbuilder = new XmlStringBuilder();
            var serializer = new XmlSerializer(_staticTranslation.GetType());
            var writer = new StringWriter();
            serializer.Serialize(writer, _staticTranslation);

            //act
            var actual = xmlbuilder.GetExportString(_staticTranslation);

            //assert
            Assert.AreEqual(actual, writer.ToString());
        }

        [Test]
        public void XmlStringBuilder_CreateExportString_SendDynamicDataList_EqualsToSpecificStringValue()
        {
            //arrange
            IExportBuilder xmlbuilder = new XmlStringBuilder();
            var serializer = new XmlSerializer(_dynamicTranslation.GetType());
            var writer = new StringWriter();
            serializer.Serialize(writer, _dynamicTranslation);

            //act
            var actual = xmlbuilder.GetExportString(_dynamicTranslation);

            //assert
            Assert.AreEqual(actual, writer.ToString());
        }

        [Test]
        public void XmlStringBuilder_CreateExportString_SendNullWithStringType_ExpectedEmptyString()
        {
            //arrange
            IExportBuilder xmlbuilder = new XmlStringBuilder();

            //act
            var actual = xmlbuilder.GetExportString<String>(null);

            //assert
            Assert.AreEqual(actual, String.Empty);
        }

        [Test]
        public void XmlStringBuilder_CreateExportString_SendNullWithInt32Type_ExpectedEmptyString()
        {
            //arrange
            IExportBuilder xmlbuilder = new XmlStringBuilder();

            //act
            var actual = xmlbuilder.GetExportString<Int32>(null);

            //assert
            Assert.AreEqual(actual, String.Empty);
        }
    }
}
