using System.Web;
using Moq;
using NUnit.Framework;
using Roads.Web.Mvc.Integrations;
using Roads.Web.Mvc.Services.ImportFileProviders;

namespace Roads.Tests
{
    [TestFixture]
    public class ImportProviderResolverTests
    {
        [TestCase("text/xml", 1000)]
        [TestCase("application/xml", 3)]
        public void FileParserResolver_ResolveImportProvider_SendFakeXmlFile_ExpectedXmlProviderResolve(
            string contentType, int contentLength)
        {
            //arrange
            var fileMock = new Mock<HttpPostedFileBase>();
            fileMock.Setup(@base => @base.ContentType).Returns(contentType);
            fileMock.Setup(@base => @base.ContentLength).Returns(1);
            HttpPostedFileBase fileObj = fileMock.Object;
            //act
            IImportFileProvider provider = ImportProviderResolver.ResolveImportProvider(fileObj);

            //assert
            Assert.IsTrue(provider is ImportXmlProvider);
            Assert.IsFalse(provider is ImportCsvProvider);
        }

        [TestCase("text/csv", 1)]
        [TestCase("application/csv", 400)]
        public void FileParserResolver_ResolveImportProvider_SendFakeCsvFile_ExpectedCsvProviderResolve(
            string contentType, int contentLength)
        {
            //arrange
            var fileMock = new Mock<HttpPostedFileBase>();
            fileMock.Setup(@base => @base.ContentType).Returns(contentType);
            fileMock.Setup(@base => @base.ContentLength).Returns(contentLength);
            HttpPostedFileBase fileObj = fileMock.Object;

            //act
            IImportFileProvider provider = ImportProviderResolver.ResolveImportProvider(fileObj);

            //assert
            Assert.IsTrue(provider is ImportCsvProvider);
            Assert.IsFalse(provider is ImportXmlProvider);
        }

        [Test]
        public void FileParserResolver_ResolveImportProvider_SendNull_ExpectedNull()
        {
            //act
            IImportFileProvider provider = ImportProviderResolver.ResolveImportProvider(null);

            //assert
            Assert.IsNull(provider);
        }

        [TestCase("text/pdf", 0)]
        [TestCase("text/pdf", -1)]
        [TestCase("text/pdf", -1000)]
        [TestCase("text/csv", 0)]
        [TestCase("text/csv", -1)]
        [TestCase("application/csv", -1)]
        [TestCase("application/doc", 400)]
        [TestCase("application/xml", 0)]
        [TestCase("application/xml", -1)]
        [TestCase("application/xml", -1000)]
        [TestCase("text/doc", 1)]
        [TestCase("text/txt", 0)]
        [TestCase("text/pdf", 1)]
        public void FileParserResolver_ResolveImportProvider_SendUnExpectableValues_ExpectedNull(string contentType,
            int contentLength)
        {
            //arrange
            var fileMock = new Mock<HttpPostedFileBase>();
            fileMock.Setup(@base => @base.ContentType).Returns(contentType);
            fileMock.Setup(@base => @base.ContentLength).Returns(contentLength);
            HttpPostedFileBase fileObj = fileMock.Object;

            //act
            IImportFileProvider provider = ImportProviderResolver.ResolveImportProvider(fileObj);

            //assert
            Assert.IsNull(provider);
        }
    }
}
