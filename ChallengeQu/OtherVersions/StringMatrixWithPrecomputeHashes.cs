﻿using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// The StringMatrix class represents a matrix of strings where each string is treated as a row.
/// It provides functionality to search for words in both horizontal and vertical lines of the matrix,
/// optimized with precomputed hash codes for substring searching.
/// </summary>
public class StringMatrixWithPrecomputeHashes
{
    #region Private Properties

    /// <summary>
    /// Stores the horizontal rows of the matrix.
    /// </summary>
    public readonly List<string> Rows;

    /// <summary>
    /// Stores the vertical columns of the matrix.
    /// </summary>
    public readonly List<string> Columns;

    /// <summary>
    /// The number of rows in the matrix.
    /// </summary>
    public readonly int rowsCount;

    /// <summary>
    /// The number of columns in the matrix.
    /// </summary>
    public readonly int columnsCount;

    /// <summary>
    /// Maps each row index to a set of hash codes of all substrings of that row.
    /// </summary>
    private readonly Dictionary<int, HashSet<int>> rowHashes;

    /// <summary>
    /// Maps each column index to a set of hash codes of all substrings of that column.
    /// </summary>
    private readonly Dictionary<int, HashSet<int>> columnHashes;

    #endregion

    #region Constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="StringMatrix"/> class.
    /// Precomputes hash codes for all possible substrings of rows and columns to optimize word searching.
    /// </summary>
    /// <param name="matrix">The input matrix as an IEnumerable of strings where each string represents a row.</param>
    public StringMatrixWithPrecomputeHashes(IEnumerable<string> matrix)
    {
        Rows = matrix.ToList();
        rowsCount = Rows.Count;
        columnsCount = Rows.Count > 0 ? Rows[0].Length : 0;
        Columns = GetColumnsFromMatrixList(Rows, rowsCount, columnsCount);

        rowHashes = new Dictionary<int, HashSet<int>>();
        columnHashes = new Dictionary<int, HashSet<int>>();

        // Precompute hash codes for all substrings in rows and columns
        PrecomputeHashes();
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Finds the number of appearances of words from the wordstream in the matrix (both horizontally and vertically).
    /// Optimized by checking hash codes before using String.Contains.
    /// </summary>
    /// <param name="words">A stream of words to search for in the matrix.</param>
    /// <returns>A dictionary where keys are words found in the matrix and values are their counts.</returns>
    public Dictionary<string, int> FindMatches(IEnumerable<string> words)
    {
        var wordSet = FilterWords(words);
        var result = new Dictionary<string, int>();

        foreach (var word in wordSet)
        {
            int wordHash = word.GetHashCode();
            bool isInLine = false;

            // Check if word hash is present in row hashes
            for (int i = 0; i < rowsCount; i++)
            {
                if (rowHashes[i].Contains(wordHash) && Rows[i].Contains(word))
                {
                    isInLine = true;
                    break;
                }
            }

            // Check if word hash is present in column hashes
            if (!isInLine)
            {
                for (int i = 0; i < columnsCount; i++)
                {
                    if (columnHashes[i].Contains(wordHash) && Columns[i].Contains(word))
                    {
                        isInLine = true;
                        break;
                    }
                }
            }

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
    /// Precomputes hash codes for all substrings of each row and column.
    /// </summary>
    private void PrecomputeHashes()
    {
        // Precompute for rows
        for (int i = 0; i < rowsCount; i++)
        {
            rowHashes[i] = new HashSet<int>();
            AddSubstringsToHashSet(Rows[i], rowHashes[i]);
        }

        // Precompute for columns
        for (int i = 0; i < columnsCount; i++)
        {
            columnHashes[i] = new HashSet<int>();
            AddSubstringsToHashSet(Columns[i], columnHashes[i]);
        }
    }

    /// <summary>
    /// Adds all possible substring hash codes of the input string to the given hash set.
    /// </summary>
    /// <param name="line">The input string (row or column).</param>
    /// <param name="hashSet">The hash set to store the hash codes of substrings.</param>
    private void AddSubstringsToHashSet(string line, HashSet<int> hashSet)
    {
        for (int start = 0; start < line.Length; start++)
        {
            for (int length = 1; length <= line.Length - start; length++)
            {
                string substring = line.Substring(start, length);
                hashSet.Add(substring.GetHashCode());
            }
        }
    }

    /// <summary>
    /// Extracts and returns the vertical columns from a list of strings representing the rows of a matrix.
    /// </summary>
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
    /// Filters the input collection of words, returning only words that are 64 characters or fewer.
    /// The filtered words are stored in a HashSet to ensure uniqueness.
    /// </summary>
    private HashSet<string> FilterWords(IEnumerable<string> words)
    {
        words = words.Where(x => x.Length <= 64);
        return new HashSet<string>(words);
    }

    #endregion
}
