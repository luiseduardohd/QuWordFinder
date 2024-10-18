using System;
using System.Collections.Generic;
using System.Linq;

public class WordFinderV2
{
    private readonly List<string> _horizontalLines;
    private readonly List<string> _verticalLines;
    private readonly int _rows;
    private readonly int _cols;

    /// <summary>
    /// Constructor that receives a list of strings as the matrix.
    /// </summary>
    /// <param name="matrix">The matrix to search within.</param>
    public WordFinderV2(IEnumerable<string> matrix)
    {
        var matrixList = matrix.ToList();
        _rows = matrixList.Count;
        _cols = matrixList[0].Length;

        // Preprocess matrix into horizontal and vertical strings
        _horizontalLines = matrixList;
        _verticalLines = new List<string>();

        // Convert columns into vertical strings
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
    /// Finds the top 10 most repeated words in the matrix.
    /// </summary>
    /// <param name="wordstream">The stream of words to search for in the matrix.</param>
    /// <returns>An IEnumerable of the top 10 most repeated words.</returns>
    public IEnumerable<string> Find(IEnumerable<string> wordstream)
    {
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
    /// <param name="word">The word to search for.</param>
    /// <param name="lines">The list of lines (horizontal or vertical).</param>
    /// <returns>True if the word is found in any line, otherwise false.</returns>
    private bool SearchWordInLines(string word, List<string> lines)
    {
        foreach (var line in lines)
        {
            if (line.Contains(word)) // Fast string search using built-in method
            {
                return true;
            }
        }
        return false;
    }
}