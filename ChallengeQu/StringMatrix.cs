using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// The StringMatrix class represents a matrix of strings where each string is treated as a row.
/// It provides functionality to search for words both horizontally and vertically in the matrix.
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
    /// Time complexity: O(n * m), where n is the number of rows and m is the number of columns.
    /// Space complexity: O(n * m), since we store both the rows and columns.
    /// </summary>
    /// <param name="matrix">The input matrix as an IEnumerable of strings where each string represents a row.</param>
    public StringMatrix(IEnumerable<string> matrix)
    {
        var matrixList = matrix.ToList();
        rowsCount = matrixList.Count;
        columnsCount = matrixList.Count > 0 ? matrixList[0].Length : 0;

        Rows = matrixList;
        Columns = GetColumnsFromMatrixList(matrixList, rowsCount, columnsCount);

        // Time complexity: O(n * m) where n is the number of rows and m is the number of columns
        // Space complexity: O(n * m) since we store both rows and columns.
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Finds the number of appearances of words from the wordstream in the matrix (both horizontally and vertically).
    /// Time complexity: O(k * n * m) for this method and O( k * n^2 * m^2 ) total, where k is the number of words, n is the number of rows, and m is the number of columns.
    /// Space complexity: O(k) due to the dictionary storing words and their counts.
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

        return result;
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Extracts and returns the vertical columns from a list of strings representing the rows of a matrix.
    /// Each string in the input list represents a row, and this method processes the matrix to extract the columns
    /// as a list of strings, where each string corresponds to a column.
    /// Time complexity: O(n * m), where n is the number of rows and m is the number of columns.
    /// Space complexity: O(n * m), as we are storing a new set of columns.
    /// </summary>
    /// <param name="matrixList">A list of strings where each string represents a row of the matrix.</param>
    /// <param name="rowsCount">The number of rows in the matrix.</param>
    /// <param name="columnsCount">The number of columns in the matrix.</param>
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
            resultColumns.Add(new string(verticalWord));
        }

        return resultColumns;
    }

    /// <summary>
    /// Searches for a word in a list of lines (either horizontal or vertical).
    /// Time complexity: O(n * m) , where n is the number of rows (or columns) and m is the length of the word.
    /// Space complexity: O(1), as we are not using extra space apart from the input, besides of what String.Contains generates.
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
            // The time complexity of String.Contains, should be O(n * m) in the worst case, where n is the length of the main string, and m is the length of the substring
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
    /// Time complexity: O(k), where k is the number of words.
    /// Space complexity: O(k), where k is the number of words filtered.
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
