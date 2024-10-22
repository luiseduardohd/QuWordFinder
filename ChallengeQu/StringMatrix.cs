using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// The StringMatrix class represents a matrix of strings where each string is treated as a row.
/// It provides functionality to search for words in both horizontal and vertical lines of the matrix.
/// </summary>
public class StringMatrix
{
    #region Private Properties

    /// <summary>
    /// Stores the horizontal rows of the matrix.
    /// </summary>
    public readonly IEnumerable<string> Rows;

    /// <summary>
    /// Stores the vertical columns of the matrix.
    /// </summary>
    public readonly IEnumerable<string> Columns;

    /// <summary>
    /// The number of rows in the matrix.
    /// </summary>
    public readonly int rowsCount;

    /// <summary>
    /// The number of columns in the matrix.
    /// </summary>
    public readonly int columnsCount;

    #endregion

    #region Constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="StringMatrix"/> class.
    /// Preprocesses the matrix into horizontal and vertical lines.
    /// </summary>
    /// <param name="matrix">The input matrix as an IEnumerable of strings where each string represents a row.</param>
    public StringMatrix(IEnumerable<string> matrix)
    {
        var matrixList = matrix.ToList();
        rowsCount = matrixList.Count;
        columnsCount = matrixList.Count > 0 ?  matrixList[0].Length:0;

        Rows = matrixList;
        Columns = GetColumnsFromMatrixList(matrixList, rowsCount, columnsCount);

        // In here the matrix memory can be discarded by the gc,
        // so the spatial complexity in this point is constant O(1)
        // where space = 2 *  rows * columns aprox
        // time complexity O(n^2)
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Finds the number of appearances of words from the wordstream in the matrix (both horizontally and vertically).
    /// </summary>
    /// <param name="words">A stream of words to search for in the matrix.</param>
    /// <returns>A dictionary where keys are words found in the matrix and values are their counts.</returns>
    public virtual Dictionary<string, int> FindMatches(IEnumerable<string> words)
    {
        //We filter repeated words
        var wordSet = FilterWords(words);
        var result = new Dictionary<string, int>();

        foreach (var word in wordSet)
        {
            bool isInLine = FindInLines(word, Rows) || FindInLines(word, Columns);

            if (isInLine)
            {
                if (!result.ContainsKey(word))
                {
                    result[word] = 1;
                }
            }
        }

        // Return the new dictionary of word counts
        return result;
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Extracts and returns the vertical columns from a list of strings representing the rows of a matrix.
    /// Each string in the input list represents a row, and this method processes the matrix to extract the columns
    /// as a list of strings, where each string corresponds to a column.
    /// </summary>
    /// <param name="matrixList">A list of strings where each string represents a row of the matrix.</param>
    /// <returns>A list of strings where each string represents a column of the matrix.</returns>
    private List<string> GetColumnsFromMatrixList(List<string> matrixList, int rowsCount, int columnsCount)
    {
        var resultColumns = new List<string>();

        for (int col = 0; col < columnsCount; col++)
        {
            char[] verticalWord = new char[rowsCount];
            for (int row = 0; row < rowsCount; row++)
            {
                verticalWord[row] = matrixList[row][col];
            }
            ((List<string>)resultColumns).Add(new string(verticalWord));
        }
        return resultColumns;
    }

    /// <summary>
    /// Searches for a word in a list of lines (either horizontal or vertical).
    /// </summary>
    /// <param name="word">The word to search for.</param>
    /// <param name="lines">The list of lines (either horizontal or vertical) where the word will be searched for.</param>
    /// <returns>True if the word is found in any of the lines, otherwise false.</returns>
    private bool FindInLines(string word, IEnumerable<string> lines)
    {
        foreach (var line in lines)
        {
            // Here we have the core of the search,
            // I'm using the default built-in method because of speed.
            if (line.Contains(word))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Filters the input collection of words, returning only words that are 64 characters or fewer.
    /// The filtered words are stored in a HashSet to ensure uniqueness.
    /// </summary>
    /// <param name="words">The collection of words to filter.</param>
    /// <returns>A HashSet containing the filtered words, with words longer than 64 characters excluded.</returns>
    private HashSet<string> FilterWords(IEnumerable<string> words)
    {
        words = words.Where(x => x.Count() <= 64);
        HashSet<string> result = new HashSet<string>(words);
        return result;
    }

    #endregion
}
