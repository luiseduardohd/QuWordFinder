using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// WordFinder class that searches for words in a given matrix (both horizontally and vertically).
/// </summary>
public class WordFinder
{
    /// <summary>
    /// Stores the matrix's horizontal lines (rows).
    /// </summary>
    private readonly List<string> _horizontalLines;

    /// <summary>
    /// Stores the matrix's vertical lines (columns).
    /// </summary>
    private readonly List<string> _verticalLines;

    /// <summary>
    /// The number of rows in the matrix.
    /// </summary>
    private readonly int _rows;

    /// <summary>
    /// The number of columns in the matrix.
    /// </summary>
    private readonly int _cols;

    /// <summary>
    /// Initializes a new instance of the <see cref="WordFinder"/> class.
    /// Preprocesses the matrix into horizontal and vertical lines for word searching.
    /// </summary>
    /// <param name="matrix">The matrix as an IEnumerable of strings, where each string represents a row.</param>
    public WordFinder(IEnumerable<string> matrix)
    {
        // Convert the input matrix to a list for easy access
        var matrixList = matrix.ToList();
        _rows = matrixList.Count;
        _cols = matrixList[0].Length;

        // Store the horizontal lines (rows)
        _horizontalLines = matrixList;

        // Preprocess and store the vertical lines (columns)
        _verticalLines = new List<string>();
        for (int col = 0; col < _cols; col++)
        {
            char[] verticalWord = new char[_rows];
            for (int row = 0; row < _rows; row++)
            {
                verticalWord[row] = matrixList[row][col];
            }
            _verticalLines.Add(new string(verticalWord));
        }
    }

    /// <summary>
    /// Finds the top 10 most repeated words from the wordstream that exist in the matrix.
    /// </summary>
    /// <param name="wordstream">The stream of words to search for in the matrix.</param>
    /// <returns>An IEnumerable of the top 10 most repeated words found in the matrix.</returns>
    public IEnumerable<string> Find(IEnumerable<string> wordstream)
    {
        // Convert the wordstream to a HashSet to avoid duplicate word searches
        var wordSet = new HashSet<string>(wordstream);
        var wordCount = new Dictionary<string, int>();

        // For each word, search in both the horizontal and vertical lines
        foreach (var word in wordSet)
        {
            bool found = SearchWordInLines(word, _horizontalLines) || SearchWordInLines(word, _verticalLines);

            if (found)
            {
                if (!wordCount.ContainsKey(word))
                {
                    wordCount[word] = 1;
                }
            }
        }

        // Return the top 10 most repeated words
        return wordCount.OrderByDescending(x => x.Value).Take(10).Select(x => x.Key);
    }

    /// <summary>
    /// Searches for a word in a list of lines (horizontal or vertical).
    /// </summary>
    /// <param name="word">The word to search for in the lines.</param>
    /// <param name="lines">The list of lines (either horizontal or vertical) where the search will take place.</param>
    /// <returns>True if the word is found in any of the lines, otherwise false.</returns>
    private bool SearchWordInLines(string word, List<string> lines)
    {
        // Iterate through each line to check if the word exists
        foreach (var line in lines)
        {
            if (line.Contains(word)) // Use string's built-in Contains method for fast searching
            {
                return true;
            }
        }
        return false;
    }
}