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

        // Store the rows
        Rows = matrixList;

        // Preprocess the columns
        Columns = GetColumnsFromMatrixList(matrixList);

        // In here the matrix memory can be discarded by the gc,
        // so the spatial complexity in this point is constant O(1)
        // where space = 2 *  rows * columns aprox
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Finds the number of appearances of words from the wordstream in the matrix (both horizontally and vertically).
    /// </summary>
    /// <param name="wordstream">A stream of words to search for in the matrix.</param>
    /// <returns>A dictionary where keys are words found in the matrix and values are their counts.</returns>
    public virtual Dictionary<string, int> FindMatches(IEnumerable<string> wordstream)
    {
        var wordSet = new HashSet<string>(wordstream);
        var wordCount = new Dictionary<string, int>();

        // For each word, search in both the horizontal and vertical lines
        foreach (var word in wordSet)
        {
            bool found = FindInLines(word, Rows) || FindInLines(word, Columns);

            if (found)
            {
                if (!wordCount.ContainsKey(word))
                {
                    wordCount[word] = 1;
                }
            }
        }

        // Return the new dictionary of word counts
        return wordCount;
    }

    #endregion

    #region Private Methods

    private List<string> GetColumnsFromMatrixList(List<string> matrixList)
    {
        var columns = new List<string>();

        for (int col = 0; col < columnsCount; col++)
        {
            char[] verticalWord = new char[rowsCount];
            for (int row = 0; row < rowsCount; row++)
            {
                verticalWord[row] = matrixList[row][col];
            }
            ((List<string>)columns).Add(new string(verticalWord));
        }
        return columns;
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
            if (line.Contains(word)) // Fast string search using built-in method
            {
                return true;
            }
        }
        return false;
    }

    #endregion
}
