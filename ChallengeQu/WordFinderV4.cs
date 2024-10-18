using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// WordFinder class that finds the most frequent sequences in a matrix of type T.
/// </summary>
public class WordFinderV4
{
    private readonly Matrix<char> _matrix;

    /// <summary>
    /// Initializes a new instance of the WordFinder class using an IEnumerable of strings.
    /// Each string is treated as a row of the matrix where characters are stored as individual elements.
    /// </summary>
    /// <param name="matrix">The character matrix where words will be searched for.</param>
    public WordFinderV4(IEnumerable<string> matrix)
    {
        // Initialize the Matrix<char> using the provided string matrix
        _matrix = new Matrix<char>(matrix);
    }

    /// <summary>
    /// Finds the top 10 words from the given wordstream that exist in the matrix.
    /// </summary>
    /// <param name="wordstream">The stream of words to search for in the matrix.</param>
    /// <returns>An IEnumerable of the top 10 words found in the matrix, ordered by frequency.</returns>
    public IEnumerable<string> Find(IEnumerable<string> wordstream)
    {
        // Return the top 10 words found, ordered by frequency
        return FindTopTenMatches(wordstream);
    }

    /// <summary>
    /// Finds and returns the top 10 words from the wordstream that match the sequences in the matrix.
    /// </summary>
    /// <param name="wordstream">The stream of words to search for in the matrix.</param>
    /// <returns>An IEnumerable of the top 10 matching words found in the matrix, ordered by frequency.</returns>
    public IEnumerable<string> FindTopTenMatches(IEnumerable<string> wordstream)
    {
        // Return the top 10 words found, ordered by frequency
        return FindMatchesCount(wordstream)
               .OrderByDescending(word => word.Value)
               .Take(10)
               .Select(word => word.Key);
    }

    /// <summary>
    /// Searches the matrix for matches from the wordstream and returns a dictionary with the count of words found.
    /// </summary>
    /// <param name="wordstream">The stream of words to search for in the matrix.</param>
    /// <returns>A dictionary where the keys are the words found and the values are their counts (frequency of occurrences).</returns>
    public Dictionary<string, int> FindMatchesCount(IEnumerable<string> wordstream)
    {
        // Dictionary to store the count of found words
        var foundWordsCount = new Dictionary<string, int>();

        // Use a HashSet to avoid duplicate word searches
        var uniqueWords = new HashSet<string>(wordstream);

        // Search for each word in the matrix
        foreach (var word in uniqueWords)
        {
            if (_matrix.ContainsSequence(word))
            {
                if (!foundWordsCount.ContainsKey(word))
                {
                    foundWordsCount[word] = 1;
                }
            }
        }

        // Return the words found, ordered by frequency
        return foundWordsCount;
    }
}