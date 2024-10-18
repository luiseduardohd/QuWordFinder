using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// WordFinder class that searches for sequences of integers in a given matrix (both horizontally and vertically).
/// </summary>
public class IntMatrix
{
    /// <summary>
    /// Stores the matrix's horizontal lines (rows) of integers.
    /// </summary>
    private readonly List<int[]> _horizontalLines;

    /// <summary>
    /// Stores the matrix's vertical lines (columns) of integers.
    /// </summary>
    private readonly List<int[]> _verticalLines;

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
    /// Preprocesses the matrix into horizontal and vertical integer lines for efficient sequence searching.
    /// </summary>
    /// <param name="matrix">The integer matrix where sequences are searched for.</param>
    public IntMatrix(IEnumerable<IEnumerable<int>> matrix)
    {
        // Convert the input matrix to a list of integer arrays for easy access
        var matrixList = matrix.Select(row => row.ToArray()).ToList();

        // Initialize row and column counts
        _rows = matrixList.Count;
        _cols = matrixList[0].Length;

        // Store the horizontal lines (rows)
        _horizontalLines = matrixList;

        // Preprocess and store the vertical lines (columns)
        _verticalLines = new List<int[]>();
        for (int col = 0; col < _cols; col++)
        {
            int[] verticalSequence = new int[_rows];
            for (int row = 0; row < _rows; row++)
            {
                verticalSequence[row] = matrixList[row][col];
            }
            _verticalLines.Add(verticalSequence);
        }
    }

    /// <summary>
    /// Searches for sequences from the sequence stream in the matrix (both horizontally and vertically)
    /// and returns the top 10 most frequent sequences found.
    /// </summary>
    /// <param name="sequenceStream">The stream of sequences to search for in the matrix.</param>
    /// <returns>A list of the top 10 most frequent sequences found in the matrix.</returns>
    public IEnumerable<IEnumerable<int>> Find(IEnumerable<IEnumerable<int>> sequenceStream)
    {
        // Convert the sequence stream to a HashSet to remove duplicates and optimize lookup
        var uniqueSequences = new HashSet<string>(sequenceStream.Select(seq => string.Join(",", seq)));

        // Dictionary to keep track of the frequency of found sequences
        var foundSequencesCount = new Dictionary<string, int>();

        // Search for each sequence in the matrix
        foreach (var sequenceStr in uniqueSequences)
        {
            var sequence = sequenceStr.Split(',').Select(int.Parse).ToArray();

            // Search horizontally and vertically for the sequence
            bool isFound = SearchSequenceInLines(sequence, _horizontalLines) || SearchSequenceInLines(sequence, _verticalLines);

            // If the sequence is found, increment its count or initialize it in the dictionary
            if (isFound)
            {
                if (!foundSequencesCount.ContainsKey(sequenceStr))
                {
                    foundSequencesCount[sequenceStr] = 1;
                }
            }
        }

        // Return the top 10 most frequent sequences found in the matrix
        return foundSequencesCount
               .OrderByDescending(sequence => sequence.Value) // Sort by frequency in descending order
               .Take(10)                                      // Take the top 10 results
               .Select(sequence => sequence.Key.Split(',').Select(int.Parse)); // Convert sequence strings back to integer arrays
    }

    /// <summary>
    /// Searches for a sequence of integers in the given list of lines (horizontal or vertical).
    /// </summary>
    /// <param name="sequence">The sequence of integers to search for.</param>
    /// <param name="lines">The list of lines (either horizontal or vertical).</param>
    /// <returns>True if the sequence is found in any of the lines, otherwise false.</returns>
    private bool SearchSequenceInLines(int[] sequence, List<int[]> lines)
    {
        // Iterate through each line to check if the sequence is present
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
    /// Checks if a sequence is present in a line of integers.
    /// </summary>
    /// <param name="line">The line of integers.</param>
    /// <param name="sequence">The sequence of integers to search for.</param>
    /// <returns>True if the sequence is found in the line, otherwise false.</returns>
    private bool ContainsSequence(int[] line, int[] sequence)
    {
        // Check each possible starting position in the line
        for (int i = 0; i <= line.Length - sequence.Length; i++)
        {
            bool match = true;
            for (int j = 0; j < sequence.Length; j++)
            {
                if (line[i + j] != sequence[j])
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
