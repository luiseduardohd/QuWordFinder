using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Generic Matrix class that handles storage and operations on a matrix of type T.
/// </summary>
/// <typeparam name="T">The type of elements in the matrix.</typeparam>
public class Matrix<T>
{
    /// <summary>
    /// The matrix rows (horizontal lines).
    /// </summary>
    private readonly List<T[]> _rows;

    /// <summary>
    /// The matrix columns (vertical lines).
    /// </summary>
    private readonly List<T[]> _columns;

    /// <summary>
    /// The number of rows in the matrix.
    /// </summary>
    public int RowCount { get; }

    /// <summary>
    /// The number of columns in the matrix.
    /// </summary>
    public int ColCount { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Matrix{T}"/> class.
    /// Preprocesses the matrix to store both rows and columns for easy access.
    /// </summary>
    /// <param name="data">The 2D collection representing the matrix.</param>
    public Matrix(IEnumerable<IEnumerable<T>> data)
    {
        _rows = data.Select(row => row.ToArray()).ToList();
        RowCount = _rows.Count;
        ColCount = _rows[0].Length;

        // Preprocess and store the vertical columns (transpose of rows)
        _columns = new List<T[]>();
        for (int col = 0; col < ColCount; col++)
        {
            T[] column = new T[RowCount];
            for (int row = 0; row < RowCount; row++)
            {
                column[row] = _rows[row][col];
            }
            _columns.Add(column);
        }
    }

    /// <summary>
    /// Gets the horizontal lines (rows) of the matrix.
    /// </summary>
    /// <returns>The list of horizontal rows.</returns>
    public List<T[]> GetRows()
    {
        return _rows;
    }

    /// <summary>
    /// Gets the vertical lines (columns) of the matrix.
    /// </summary>
    /// <returns>The list of vertical columns.</returns>
    public List<T[]> GetColumns()
    {
        return _columns;
    }

    /// <summary>
    /// Checks if a sequence is present in any row or column of the matrix.
    /// </summary>
    /// <param name="sequence">The sequence of type T to search for.</param>
    /// <returns>True if the sequence is found in the matrix, otherwise false.</returns>
    public bool ContainsSequence(IEnumerable<T> sequence)
    {
        var sequenceArray = sequence.ToArray();
        return SearchInLines(sequenceArray, _rows) || SearchInLines(sequenceArray, _columns);
    }

    /// <summary>
    /// Searches for a sequence in a list of lines (rows or columns).
    /// </summary>
    /// <param name="sequence">The sequence to search for.</param>
    /// <param name="lines">The list of lines (rows or columns).</param>
    /// <returns>True if the sequence is found in any line, otherwise false.</returns>
    private bool SearchInLines(T[] sequence, List<T[]> lines)
    {
        foreach (var line in lines)
        {
            if (ContainsSequence(line, sequence))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Checks if a sequence is present in a line.
    /// </summary>
    /// <param name="line">The line of type T.</param>
    /// <param name="sequence">The sequence to search for in the line.</param>
    /// <returns>True if the sequence is found, otherwise false.</returns>
    private bool ContainsSequence(T[] line, T[] sequence)
    {
        for (int i = 0; i <= line.Length - sequence.Length; i++)
        {
            bool match = true;
            for (int j = 0; j < sequence.Length; j++)
            {
                if (!line[i + j].Equals(sequence[j]))
                {
                    match = false;
                    break;
                }
            }
            if (match)
            {
                return true;
            }
        }
        return false;
    }
}
