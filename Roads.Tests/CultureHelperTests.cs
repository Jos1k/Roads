using System;
using System.Threading;
using NUnit.Framework;
using Roads.Web.Mvc.Services;

namespace Roads.Tests
{
    [TestFixture]
    class CultureHelperTests
    {
        [TestCase("EN")]
        [TestCase("en")]
        [TestCase("En")]
        [TestCase("eN")]
        [TestCase("en-US")]
        [TestCase("en-abracadabra")]
        public void CultureHelper_GetCulture_EnglishCultureName_ExpectedEnglishCulture(string name)
        {
            //arrange
            string expectedCulture = "en";

            //act
            var result = CultureHelper.GetCulture(name);

            //assert
            Assert.AreEqual(expectedCulture, result);
        }

        [TestCase("UK")]
        [TestCase("uk")]
        [TestCase("Uk")]
        [TestCase("uK")]
        [TestCase("uk-UA")]
        [TestCase("uk-abracadabra")]
        public void CultureHelper_GetCulture_UkrainianCultureName_ExpectedUkrainianCulture(string name)
        {
            //arrange
            string expectedCulture = "uk";

            //act
            var result = CultureHelper.GetCulture(name);

            //assert
            Assert.AreEqual(expectedCulture, result);
        }

        [TestCase("RU")]
        [TestCase("ru")]
        [TestCase("Ru")]
        [TestCase("rU")]
        [TestCase("rU-Sb")]
        [TestCase("ru-abracadabra")]
        public void CultureHelper_GetCulture_RussianCultureName_ExpectedRussianCulture(string name)
        {
            //arrange
            string expectedCulture = "ru";

            //act
            var result = CultureHelper.GetCulture(name);

            //assert
            Assert.AreEqual(expectedCulture, result);
        }
        [TestCase("Cz")]
        [TestCase("Pl")]
        [TestCase("us")]
        [TestCase("culture")]
        [TestCase("")]
        [TestCase(null)]
        public void CultureHelper_GetCulture_UnAvailableCultureName_ExpectedDefaultCulture(string name)
        {
            //arrange
            string expectedCulture = "en";

            //act
            var result = CultureHelper.GetCulture(name);

            //assert
            Assert.AreEqual(expectedCulture, result);
        }

        [Test]
        public void CultureHelper_GetDefaultCulture_ExpectedDefaultCulture()
        {
            //arrange
            string expectedCulture = "en";

            //act
            var result = CultureHelper.GetDefaultCulture();

            //assert
            Assert.AreEqual(expectedCulture, result);
        }

        [Test]
        public void Culturehelper_GetCurrentCulture_ExpectedCurrentCulture()
        {
            //arrange
            string expectedCulture = Thread.CurrentThread.CurrentCulture.Name;

            //act
            var result = CultureHelper.GetCurrentCulture();

            //assert
            Assert.AreEqual(expectedCulture, result);
        }

        [Test]
        public void CultureHelper_GetCurrentNeutralCulture_ExpectedCurrentNeutralCulture()
        {
            //arrange
            string currentCulture = Thread.CurrentThread.CurrentCulture.Name;
            string expectedCulture = currentCulture.Substring(0, 2).ToLower();

            //act
            var result = CultureHelper.GetCurrentNeutralCulture();

            //assert
            Assert.AreEqual(expectedCulture, result);
        }

        [TestCase("en")]
        [TestCase("en-US")]
        [TestCase("en-abracadabra")]
        [TestCase("en-")]
        public void CultureHelper_GetNeutralCulture_CultureName_ExpectedNeutralCulture(string name)
        {
            //arrange
            string expectedCulture = "en";

            //act
            var result = CultureHelper.GetNeutralCulture(name);

            //assert
            Assert.AreEqual(expectedCulture, result);
        }

        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void CultureHelper_GetNeutralCulture_SendNull_ExpectedException()
        {
            //act
            CultureHelper.GetNeutralCulture(null);
        }

        [Test]
        public void CultureHelper_GetNeutralCulture_SendEmptyString_ExpectedEmptyString()
        {
            //arrange
            var expectedString = String.Empty;

            //act
            var result = CultureHelper.GetNeutralCulture("");

            //assert
            Assert.AreEqual(expectedString, result);
        }

        [TestCase("en")]
        [TestCase("uA")]
        [TestCase("Ru")]
        [TestCase("UK")]
        public void CultureHelper_FirstLetterUp_DifferentStrings_ExpectedFirstLetterInUpperCase(string name)
        {
            //arrange
            var expectedValue = name.Substring(0, 1).ToUpper() + name.Substring(1);
            //act
            var result = CultureHelper.FirstLetterUp(name);

            //assert
            Assert.AreEqual(expectedValue, result);
        }
        [TestCase(null)]
        [TestCase("")]
        public void CultureHelper_FirstLetterUp_SendNullOrEmpty_ExpectedException(string name)
        {
            //arrange
            var expectedValue = String.Empty;

            //act
            var result = CultureHelper.FirstLetterUp(name);

            //assert
            Assert.AreEqual(expectedValue, result);
        }
    }
}
