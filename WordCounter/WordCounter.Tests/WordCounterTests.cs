using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordCounter.Core.Interfaces;

namespace WordCounter.Tests
{
    [TestClass]
    public class WordCounterTests
    {
        private IWordCounter _wordCounter;

        [TestInitialize]
        public void TestInit()
        {
            _wordCounter = new Core.WordCounter();
        }

        [TestMethod]
        public void WordCounterTests_ReturnsCorrectWordCount()
        {
            // Arrange
            const string text = "The quick brown fox jumps over the lazy dog";

            var expectedResult = new Dictionary<string, int>
            {
                { "the", 2 },
                { "quick", 1 },
                { "brown", 1 },
                { "fox", 1 },
                { "jumps", 1 },
                { "over", 1 },
                { "lazy", 1 },
                { "dog", 1 }
            };

            // Act
            var actualResult = _wordCounter.CountWords(text);

            // Assert
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void WordCounterTests_ReturnsCorrectWordCountWhenTheSameWordsHaveDifferentCasing()
        {
            // Arrange
            const string text = "The the";

            var expectedResult = new Dictionary<string, int>
            {
                { "the", 2 },
            };

            // Act
            var actualResult = _wordCounter.CountWords(text);

            // Assert
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void WordCounterTests_ReturnsCorrectWordCountWhenTextContainsFullStops()
        {
            // Arrange
            const string text = "The quick brown fox jumps over the lazy dog. The quick brown fox jumps over the lazy dog.";

            var expectedResult = new Dictionary<string, int>
            {
                { "the", 4 },
                { "quick", 2 },
                { "brown", 2 },
                { "fox", 2 },
                { "jumps", 2 },
                { "over", 2 },
                { "lazy", 2 },
                { "dog", 2 }
            };

            // Act
            var actualResult = _wordCounter.CountWords(text);

            // Assert
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void WordCounterTests_ReturnsCorrectWordCountWhenTextContainsNonAlphanumericCharacters()
        {
            // Arrange
            const string text = @"£$%^ The quick brown fox jumps )(*& over the %^& lazy dog";

            var expectedResult = new Dictionary<string, int>
            {
                { "the", 2 },
                { "quick", 1 },
                { "brown", 1 },
                { "fox", 1 },
                { "jumps", 1 },
                { "over", 1 },
                { "lazy", 1 },
                { "dog", 1 }
            };

            // Act
            var actualResult = _wordCounter.CountWords(text);

            // Assert
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void WordCounterTests_ReturnsCorrectWordCountWhenTextContainsNewLines()
        {
            // Arrange
            var text = new StringBuilder();
            text.AppendLine("The quick brown fox jumps over the lazy dog.");
            text.AppendLine();
            text.AppendLine("Tell me and I forget. Teach me and I remember. Involve me and I learn.");

            var expectedResult = new Dictionary<string, int>
            {
                { "me", 3 },
                { "and", 3 },
                { "i", 3 },
                { "the", 2 },
                { "quick", 1 },
                { "brown", 1 },
                { "fox", 1 },
                { "jumps", 1 },
                { "over", 1 },
                { "lazy", 1 },
                { "dog", 1 },
                { "tell", 1 },
                { "forget", 1 },
                { "teach", 1 },
                { "remember", 1 },
                { "involve", 1 },
                { "learn", 1 }
            };

            // Act
            var actualResult = _wordCounter.CountWords(text.ToString());

            // Assert
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void WordCounterTests_ReturnsOnlyTheTopTwentyWordsWhenWordCountIsMoreThanTwenty()
        {
            // Arrange
            var text = new StringBuilder();
            text.AppendLine("The quick brown fox jumps over the lazy dog.");
            text.AppendLine();
            text.AppendLine("Tell me and I forget. Teach me and I remember. Involve me and I learn.");
            text.AppendLine();
            text.AppendLine("Success is not final; failure is not fatal: It is the courage to continue that counts..");

            var expectedWordCount = 20;

            // Act
            var actualWordCount = _wordCounter.CountWords(text.ToString()).Count;

            // Assert
            Assert.AreEqual(expectedWordCount, actualWordCount);
        }

        [TestMethod]
        public void WordCounterTests_ReturnsCorrectWordCountWhenCountingByLines()
        {
            // As this method uses the same approach to counting words as CountWords does, an assumption has been made
            // that the above tests will cover the code doing the counting and that this test is to ensure that the
            // async code doesn't have a negative affect.

            // Arrange
            const string text = "The quick brown fox jumps over the lazy dog";

            var expectedResult = new Dictionary<string, int>
            {
                { "the", 2 },
                { "quick", 1 },
                { "brown", 1 },
                { "fox", 1 },
                { "jumps", 1 },
                { "over", 1 },
                { "lazy", 1 },
                { "dog", 1 }
            };

            // Act
            var actualResult = _wordCounter.CountWordsByLine(text);

            // Assert
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }
    }
}