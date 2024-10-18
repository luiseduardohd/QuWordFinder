using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace ChallengeQu.Tests
{
    [TestFixture]
    public class WordFinderTests
    {
        private WordFinder _wordFinder;
        private Mock<StringMatrix> _mockStringMatrix;

        [SetUp]
        public void SetUp()
        {
            // Mock the StringMatrix to isolate WordFinder
            _mockStringMatrix = new Mock<StringMatrix>(new List<string>());

            // Inject the mocked StringMatrix into WordFinder
            _wordFinder = new WordFinder(new List<string>());
        }

        [Test]
        public void Find_ShouldReturnTopTenWords_WhenWordsExistInMatrix()
        {
            // Arrange
            var wordStream = new List<string> { "cat", "dog", "rat", "ice", "fox" };

            // Simulate FindMatches returning 5 words with their counts
            var wordCounts = new Dictionary<string, int>
            {
                { "cat", 5 },
                { "dog", 4 },
                { "rat", 3 },
                { "ice", 2 },
                { "fox", 1 }
            };

            // Mock the behavior of FindMatches in StringMatrix
            _mockStringMatrix
                .Setup(m => m.FindMatches(wordStream))
                .Returns(wordCounts);

            // Act
            var result = _wordFinder.Find(wordStream).ToList();

            // Assert
            Assert.That(result.Count, Is.EqualTo(5));
            Assert.That(result[0], Is.EqualTo("cat"));
            Assert.That(result[1], Is.EqualTo("dog"));
            Assert.That(result[2], Is.EqualTo("rat"));
            Assert.That(result[3], Is.EqualTo("ice"));
            Assert.That(result[4], Is.EqualTo("fox"));
        }

        [Test]
        public void Find_ShouldReturnTopTenWords_WhenMoreThanTenWordsAreFound()
        {
            // Arrange
            var wordStream = new List<string> { "cat", "dog", "rat", "ice", "fox", "bat", "cow", "ant", "owl", "eel", "bee" };

            // Simulate FindMatches returning more than 10 words
            var wordCounts = new Dictionary<string, int>
            {
                { "cat", 10 }, { "dog", 9 }, { "rat", 8 }, { "ice", 7 }, { "fox", 6 },
                { "bat", 5 }, { "cow", 4 }, { "ant", 3 }, { "owl", 2 }, { "eel", 1 },
                { "bee", 0 }
            };

            // Mock the behavior of FindMatches in StringMatrix
            _mockStringMatrix
                .Setup(m => m.FindMatches(wordStream))
                .Returns(wordCounts);

            // Act
            var result = _wordFinder.Find(wordStream).ToList();

            // Assert
            Assert.That(result.Count, Is.EqualTo(10));
            Assert.That(result.Contains("bee"), Is.False); // 'bee' should not be in the top 10.
        }

        [Test]
        public void Find_ShouldReturnEmpty_WhenNoWordsMatch()
        {
            // Arrange
            var wordStream = new List<string> { "lion", "tiger", "bear" };

            // Simulate FindMatches returning an empty result
            var wordCounts = new Dictionary<string, int>();

            // Mock the behavior of FindMatches in StringMatrix
            _mockStringMatrix
                .Setup(m => m.FindMatches(wordStream))
                .Returns(wordCounts);

            // Act
            var result = _wordFinder.Find(wordStream).ToList();

            // Assert
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void Find_ShouldHandleEmptyWordStream()
        {
            // Arrange
            var wordStream = new List<string>(); // Empty word stream

            // Simulate FindMatches returning an empty result
            var wordCounts = new Dictionary<string, int>();

            // Mock the behavior of FindMatches in StringMatrix
            _mockStringMatrix
                .Setup(m => m.FindMatches(wordStream))
                .Returns(wordCounts);

            // Act
            var result = _wordFinder.Find(wordStream).ToList();

            // Assert
            Assert.That(result.Count, Is.EqualTo(0)); // No matches should be found.
        }

        [Test]
        public void Find_ShouldReturnTopWords_WhenFewerThanTenWordsAreFound()
        {
            // Arrange
            var wordStream = new List<string> { "cat", "dog", "rat" };

            // Simulate FindMatches returning 3 words
            var wordCounts = new Dictionary<string, int>
            {
                { "cat", 5 },
                { "dog", 4 },
                { "rat", 3 }
            };

            // Mock the behavior of FindMatches in StringMatrix
            _mockStringMatrix
                .Setup(m => m.FindMatches(wordStream))
                .Returns(wordCounts);

            // Act
            var result = _wordFinder.Find(wordStream).ToList();

            // Assert
            Assert.That(result.Count, Is.EqualTo(3)); // Only 3 words exist, so return those 3.
            Assert.That(result[0], Is.EqualTo("cat"));
            Assert.That(result[1], Is.EqualTo("dog"));
            Assert.That(result[2], Is.EqualTo("rat"));
        }
    }
}
