using System;
using System.Collections.Generic;
using System.Linq;

public class WordFinder2
{
    private readonly List<string> _horizontalLines;
    private readonly List<string> _verticalLines;
    private readonly int _rows;
    private readonly int _cols;

    // Constructor that receives a list of strings as the matrix
    public WordFinder2(IEnumerable<string> matrix)
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

    // Method to find top 10 most repeated words in the matrix
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

    // Optimized search method that checks for word in a list of lines (horizontal or vertical)
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