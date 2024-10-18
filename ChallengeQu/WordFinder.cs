using System;
using System.Collections.Generic;
using System.Linq;

namespace ChallengeQu;

/// <summary>
/// The WordFinder class is responsible for finding the most frequent words in a string matrix.
/// It uses the StringMatrix class to search for words both horizontally and vertically.
/// </summary>
public class WordFinder
{
    /// <summary>
    /// The instance of the StringMatrix class that holds the matrix data.
    /// </summary>
    private readonly StringMatrix StringMatrix;

    /// <summary>
    /// Initializes a new instance of the <see cref="WordFinder"/> class.
    /// </summary>
    /// <param name="matrix">The matrix as an IEnumerable of strings where each string represents a row.</param>
    public WordFinder(IEnumerable<string> matrix)
    {
        StringMatrix = new StringMatrix(matrix);
    }

    /// <summary>
    /// Finds the top 10 most frequent words from the given wordstream in the matrix.
    /// </summary>
    /// <param name="wordstream">The stream of words to search for in the matrix.</param>
    /// <returns>An IEnumerable of the top 10 most frequent words found in the matrix.</returns>
    public IEnumerable<string> Find(IEnumerable<string> wordstream)
    {
        return FindTopTen(wordstream);
    }

    /// <summary>
    /// Finds and returns the top 10 most frequent words from the wordstream in the matrix.
    /// </summary>
    /// <param name="wordstream">The stream of words to search for in the matrix.</param>
    /// <returns>An IEnumerable of the top 10 words found in the matrix, ordered by frequency.</returns>
    private IEnumerable<string> FindTopTen(IEnumerable<string> wordstream)
    {
        return StringMatrix.FindAppeareances(wordstream)
                            .OrderByDescending(x => x.Value)
                            .Take(10)
                            .Select(x => x.Key);
    }
}
