using System;
using System.Collections.Generic;
using System.Linq;

public class WordFinder1
{
    private readonly char[,] _matrix;
    private readonly int _rows;
    private readonly int _cols;

    // Constructor that receives a list of strings as the matrix
    public WordFinder1(IEnumerable<string> matrix)
    {
        // Convert matrix into a 2D character array for easy searching
        var matrixList = matrix.ToList();
        _rows = matrixList.Count;
        _cols = matrixList[0].Length;
        _matrix = new char[_rows, _cols];

        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _cols; j++)
            {
                _matrix[i, j] = matrixList[i][j];
            }
        }
    }

    // Method to find top 10 most repeated words in the matrix
    public IEnumerable<string> Find(IEnumerable<string> wordstream)
    {
        var wordCount = new Dictionary<string, int>();
        var wordSet = new HashSet<string>(wordstream);

        // For each word in the word stream, search horizontally and vertically
        foreach (var word in wordSet)
        {
            if (SearchWord(word))
            {
                if (!wordCount.ContainsKey(word))
                    wordCount[word] = 0;

                wordCount[word]++;
            }
        }

        // Return the top 10 most repeated words
        return wordCount.OrderByDescending(x => x.Value).Take(10).Select(x => x.Key);
    }

    // Search for the word horizontally and vertically in the matrix
    private bool SearchWord(string word)
    {
        int wordLength = word.Length;

        // Horizontal search (left to right)
        for (int row = 0; row < _rows; row++)
        {
            for (int col = 0; col <= _cols - wordLength; col++)
            {
                if (CheckHorizontal(row, col, word))
                    return true;
            }
        }

        // Vertical search (top to bottom)
        for (int col = 0; col < _cols; col++)
        {
            for (int row = 0; row <= _rows - wordLength; row++)
            {
                if (CheckVertical(row, col, word))
                    return true;
            }
        }

        return false;
    }

    // Check if the word is found horizontally starting at (row, col)
    private bool CheckHorizontal(int row, int col, string word)
    {
        for (int i = 0; i < word.Length; i++)
        {
            if (_matrix[row, col + i] != word[i])
                return false;
        }
        return true;
    }

    // Check if the word is found vertically starting at (row, col)
    private bool CheckVertical(int row, int col, string word)
    {
        for (int i = 0; i < word.Length; i++)
        {
            if (_matrix[row + i, col] != word[i])
                return false;
        }
        return true;
    }
}