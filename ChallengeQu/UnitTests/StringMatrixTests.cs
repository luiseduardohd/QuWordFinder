using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StringMatrixTests
{
    [TestFixture]
    public class StringMatrixTests
    {
        private StringMatrix _matrix = new StringMatrix(new List<string>());

        [SetUp]
        public void SetUp()
        {
            // Sample matrix for testing
            var matrixData = new List<string>
            {
                "cats",
                "dogs",
                "rats",
                "mice"
            };

            _matrix = new StringMatrix(matrixData);
        }

        [Test]
        public void Constructor_ShouldInitializeRowsAndColumnsCorrectly()
        {
            // Arrange
            var expectedRows = new List<string> { "cats", "dogs", "rats", "mice" };
            var expectedColumns = new List<string> { "cdrm", "aoai", "tgtc", "ssse" };

            // Act
            var actualRows = _matrix.Rows.ToList();
            var actualColumns = _matrix.Columns.ToList();

            // Assert
            Assert.That(actualRows, Is.EqualTo(expectedRows));
            Assert.That(actualColumns, Is.EqualTo(expectedColumns));
        }

        [Test]
        public void FindMatches_ShouldReturnCorrectWordCounts_WhenWordsExistInMatrix()
        {
            // Arrange
            var wordStream = new List<string> { "cat", "dog", "rat", "ice", "fox" };

            // Act
            var result = _matrix.FindMatches(wordStream);

            // Assert
            Assert.That(result.Count, Is.EqualTo(4)); // "cat", "dog", "rat", and ice should be found.
            Assert.That(result.ContainsKey("cat"), Is.True);
            Assert.That(result.ContainsKey("dog"), Is.True);
            Assert.That(result.ContainsKey("rat"), Is.True);
            Assert.That(result.ContainsKey("fox"), Is.False); // "fox" is not in the matrix.
        }

        [Test]
        public void FindMatches_ShouldReturnEmpty_WhenNoWordsMatch()
        {
            // Arrange
            var wordStream = new List<string> { "lion", "tiger", "bear" };

            // Act
            var result = _matrix.FindMatches(wordStream);

            // Assert
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void FindMatches_ShouldHandleCaseWhereAllWordsAreInMatrix()
        {
            // Arrange
            var wordStream = new List<string> { "cat", "dog", "rat", "mice" };

            // Act
            var result = _matrix.FindMatches(wordStream);

            // Assert
            Assert.That(result.Count, Is.EqualTo(4)); // All words should be found.
            Assert.That(result.ContainsKey("cat"), Is.True);
            Assert.That(result.ContainsKey("dog"), Is.True);
            Assert.That(result.ContainsKey("rat"), Is.True);
            Assert.That(result.ContainsKey("mice"), Is.True);
        }

        [Test]
        public void FindMatches_ShouldHandleEmptyWordStream()
        {
            // Arrange
            var wordStream = new List<string>(); // Empty word stream

            // Act
            var result = _matrix.FindMatches(wordStream);

            // Assert
            Assert.That(result.Count, Is.EqualTo(0)); // No matches should be found.
        }
    }
}
