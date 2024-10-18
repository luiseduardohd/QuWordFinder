using System;
using System.Collections.Generic;
using System.Linq;

public class StringMatrix 
{
    public readonly IEnumerable<string> Rows;
    public readonly IEnumerable<string> Columns;
    public readonly int rowsCount;
    public readonly int columnsCount;

    // Constructor that receives a list of strings as the matrix
    public StringMatrix(IEnumerable<string> matrix)
    {
        var matrixList = matrix.ToList();
        rowsCount = matrixList.Count;
        columnsCount = matrixList[0].Length;

        // Preprocess matrix into horizontal and vertical strings
        Rows = matrixList;
        Columns = new List<string>();

        // Convert columns into vertical strings
        for (int col = 0; col < columnsCount; col++)
        {
            char[] verticalWord = new char[rowsCount];
            for (int row = 0; row < rowsCount; row++)
            {
                verticalWord[row] = matrixList[row][col];
            }
            ((List<string>)Columns).Add(new string(verticalWord));
        }
    }

    public Dictionary<string, int> FindAppeareances(IEnumerable<string> wordstream)
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

        // Return the top 10 most repeated words
        return wordCount;
    }


    // Optimized search method that checks for word in a list of lines (horizontal or vertical)
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
}