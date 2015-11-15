using System.Linq;
using NUnit.Framework;
using Roads.Common.Managers;
using Roads.Common.Repositories;
using Roads.Tests.Helpers;
using Roads.Tests.Mocks;

namespace Roads.Tests
{
    /// <summary>
    /// The test class for Suggestions functionality. 
    /// </summary>
    [TestFixture]
    public class SmartSuggestionsTests
    {
        readonly DataContextMock _datacontext = new DataContextMock();

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestFixtureSetUp]
        public void Init()
        {
            DataBaseMockFiller.Instance.Fill(_datacontext.DataContext);
        }



        /// <summary>
        /// Smarts the suggestions manager_ get suggestions_ send text_ get list.
        /// </summary>
        /// <param name="entries">The string entries.</param>
        /// <param name="keyboard">The keyboard.</param>
        /// <param name="languageId">The language identifier.</param>
        /// <param name="numberOfMatches">The number of matches.</param>
        [Test]
        [TestCase(null, "ua", 1, 0)]
        [TestCase("", "ua", 1, 0)]
        [TestCase("   ", "ua", 1, 1)]
        [TestCase("    k     ", "ua", 1, 3)]
        [TestCase("    К", "ua", 1, 4)]
        [TestCase("К", "ua", 1, 4)]
        [TestCase("Ко", "ua", 1, 4)]
        [TestCase("Ко", "ru", 1, 4)]
        [TestCase("Ko", "en", 1, 2)]
        [TestCase("Co", "en", 1, 2)]
        [TestCase("Корса", "ru", 1, 1)]
        [TestCase("Корса", "ru", 2, 1)]
        [TestCase("Корса", "ru", 3, 1)]
        [TestCase("Корсаро", "ru", 3, 1)]
        public void SmartSuggestionsManager_GetSuggestions_SendText_GetList(string entries, string keyboard, int languageId, int numberOfMatches)
        {
            //arrange
            var repository = new MapObjectTranslationsRepository(_datacontext);

            var manager = new SmartSuggestionsManager(repository);

            //act

            var result = manager.GetSuggestions(entries, languageId, 10).Count();

            //assert
            Assert.IsTrue(result == numberOfMatches);
        }
    }
}
