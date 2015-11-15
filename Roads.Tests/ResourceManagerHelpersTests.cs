using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Roads.Common.Helpers;

namespace Roads.Tests
{
    [TestFixture]
    public class ResourceManagerHelpersTests
    {
        #region Initial data

        private readonly List<string> _availableCultures = new List<string>
        {
            "en",
            "cz",
            "pl",
            "ua"
        };

        #endregion

        #region NormalizeCulture Method

        [Test]
        [TestCase("En")]
        [TestCase("English")]
        [TestCase("rU")]
        [TestCase("UKRAINE")]
        public void ResourceManagerHelpers_NormalizeCultureParameter_SendDistortedLetters_NormalValue_GetTrue(
            string value)
        {
            //arrange
            const int expectedLength = 2;
            var resHelpers = new ResourceManagerHelpers();

            //act
            resHelpers.NormalizeCultureParameter(ref value);

            //assert
            Assert.IsTrue(value.Length == expectedLength);
            Assert.IsTrue(value.All(char.IsLower));
        }

        [Test]
        public void ResourceManagerHelpers_NormalizeCultureParameter_SendSymbols_GetFirstTwoSymbols_()
        {
            //arrange
            var resHelpers = new ResourceManagerHelpers();
            string value = "&$&#$@";
            string expectedValue = "&$";

            //act
            resHelpers.NormalizeCultureParameter(ref value);

            //assert
            Assert.AreEqual(expectedValue, value);
        }

        [Test]
        [TestCase("")]
        [TestCase("0")]
        [TestCase("E")]
        public void
            ResourceManagerHelpers_NormalizeCultureParameter_SendTooShortOrEmptyString_EmptyString_AreEqualWithExpectedValue
            (string value)
        {
            //arrange
            var resHelpers = new ResourceManagerHelpers();
            string expectedValue = String.Empty;

            //act
            resHelpers.NormalizeCultureParameter(ref value);

            //assert
            Assert.AreEqual(expectedValue, value);
        }


        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void ResourceManagerHelpers_NormalizeCultureParameter_SendNullArgument_GetException()
        {
            //arrange
            var resHelpers = new ResourceManagerHelpers();
            string value = null;

            //act
            resHelpers.NormalizeCultureParameter(ref value);
        }

        #endregion

        #region ValidateCulture Method

        [Test]
        [TestCase("en")]
        [TestCase("pl")]
        [TestCase("cz")]
        [TestCase("ua")]
        public void ResourceManagerHelpersTests_ValidateCulture_SendCultureAndListOfAvailableCulture_GetTrueValidation(string culture)
        {
            //arrange
            var resHelpers = new ResourceManagerHelpers();

            //act
            var isValid = resHelpers.ValidateCulture(culture, _availableCultures);

            //assert
            Assert.IsTrue(isValid);
        }

        [Test]
        [TestCase("uk")]
        [TestCase("fr")]
        [TestCase("123")]
        [TestCase(@"&#$%@")]
        [TestCase("")]
        public void ResourceManagerHelpersTests_ValidateCulture_SendInvalidCultureAndListOfAvailableCulture_GetFalseValidation(string culture)
        {
            //arrange
            var resHelpers = new ResourceManagerHelpers();

            //act
            var isValid = resHelpers.ValidateCulture(culture, _availableCultures);

            //assert
            Assert.IsFalse(isValid);
        }

        [Test]
        public void ResourceManagerHelpersTests_ValidateCulture_SendCultureNullArgument_GetFalse()
        {
            //arrange
            string culture = null;
            var resHelpers = new ResourceManagerHelpers();

            //act
            var isValid = resHelpers.ValidateCulture(culture, _availableCultures);

            //assert
            Assert.IsFalse(isValid);
        }

        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void ResourceManagerHelpersTests_ValidateCulture_SendAvailableCulturesListNullArgument_ExpectException()
        {
            //arrange
            string culture = "ua";
            var resHelpers = new ResourceManagerHelpers();

            //act
            var isValid = resHelpers.ValidateCulture(culture, null);
        }
        #endregion      
    }
}