using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Generic Matrix class that handles storage and operations on a matrix of type T.
/// </summary>
/// <typeparam name="T">The type of elements in the matrix.</typeparam>
public class MatrixV4<T>
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
    private int _rowsCount { get; }

    /// <summary>
    /// The number of columns in the matrix.
    /// </summary>
    private int _columnsCount { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Matrix{T}"/> class.
    /// Preprocesses the matrix to store both rows and columns for easy access.
    /// Uses parallelization to improve performance on large matrices.
    /// </summary>
    /// <param name="data">The 2D collection representing the matrix.</param>
    public MatrixV4(IEnumerable<IEnumerable<T>> data)
    {
        _rows = data.Select(row => row.ToArray()).ToList();
        _rowsCount = _rows.Count;
        _columnsCount = _rows[0].Length;

        // Preprocess and store the vertical columns (transpose of rows) in parallel
        _columns = new List<T[]>(_columnsCount);

        Parallel.For(0, _columnsCount, col =>
        {
            T[] column = new T[_rowsCount];
            for (int row = 0; row < _rowsCount; row++)
            {
                column[row] = _rows[row][col];
            }
            lock (_columns) // Ensure thread-safe addition to the shared list
            {
                _columns.Add(column);
            }
        });
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

        // Perform search in rows and columns in parallel
        bool foundInRows = false, foundInColumns = false;

        Parallel.Invoke(
            () => { foundInRows = SearchInLines(sequenceArray, _rows); },
            () => { foundInColumns = SearchInLines(sequenceArray, _columns); }
        );

        return foundInRows || foundInColumns;
    }

    /// <summary>
    /// Searches for a sequence in a list of lines (rows or columns).
    /// Parallelized search across all lines.
    /// </summary>
    /// <param name="sequence">The sequence to search for.</param>
    /// <param name="lines">The list of lines (rows or columns).</param>
    /// <returns>True if the sequence is found in any line, otherwise false.</returns>
    private bool SearchInLines(T[] sequence, List<T[]> lines)
    {
        bool found = false;

        // Parallelize the search across all rows or columns
        Parallel.ForEach(lines, (line, state) =>
        {
            if (ContainsSubsequence(line, sequence))
            {
                found = true;
                state.Stop(); // Exit early if a match is found
            }
        });

        return found;
    }

    /// <summary>
    /// Checks if a sequence is present in a line
    /// </summary>
    /// <param name="line">The line of type T.</param>
    /// <param name="sequence">The sequence to search for in the line.</param>
    /// <returns>True if the sequence is found, otherwise false.</returns>
    private bool ContainsSubsequence(T[] line, T[] sequence)
    {
        // We check all possible subsequences of length equal to 'sequence'
        return line
            .Select((_, index) => line.Skip(index).Take(sequence.Length))
            .Where(subsequence => subsequence.Count() == sequence.Length)
            .Any(subsequence => subsequence.SequenceEqual(sequence));
    }
}