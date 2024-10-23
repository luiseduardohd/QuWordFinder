using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using ChallengeQu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class WordFinderBenchmark
{
    private readonly IEnumerable<string> _matrix;
    private readonly IEnumerable<string> _wordStream;

    // Constructor that dynamically generates the matrix and wordstream
    public WordFinderBenchmark()
    {
        // Generate a matrix of 64 rows, each row with a random string of 64 characters
        _matrix = GenerateRandomMatrix(64, 64);
        Console.WriteLine("matrix:");
        foreach (var w in _matrix)
            Console.WriteLine();
        Console.WriteLine();

        // Generate a wordstream with at least 1000 random strings, with lengths from 1 to 64
        //_wordStream = GenerateRandomWordStream(10000, 1, 64);
        _wordStream = GenerateRandomWordStream(1000, 1, 64);
        /*
        Console.WriteLine("wordStream:");
        foreach (var w in _wordStream)
            Console.WriteLine();
        Console.WriteLine();*/
    }

    private IEnumerable<string> GenerateRandomMatrix(int rows, int wordLength)
    {
        var matrix = new List<string>();
        var random = new Random();

        for (int i = 0; i < rows; i++)
        {
            matrix.Add(GenerateRandomString(wordLength, random));
        }

        return matrix;
    }

    private string GenerateRandomString(int length, Random random)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyz";
        char[] stringChars = new char[length];

        for (int i = 0; i < length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        return new string(stringChars);
    }

    private IEnumerable<string> GenerateRandomWordStream(int count, int minLength, int maxLength)
    {
        var wordStream = new List<string>();
        var random = new Random();

        for (int i = 0; i < count; i++)
        {
            int wordLength = random.Next(minLength, maxLength + 1);
            wordStream.Add(GenerateRandomString(wordLength, random));
        }

        return wordStream;
    }

    public string wordGenerator(int length)
    {
        var chars = "abcdefghijklmnopqrstuvwxyz".ToArray();
        var random = new Random();
        string randomWord = Enumerable.Range(0, length)
                              .Aggregate(
                                  new StringBuilder(),
                                  (sb, n) => sb.Append((chars[random.Next(chars.Length)])),
                                  sb => sb.ToString());
        return randomWord;
    }

    // WordFinder Version 1 - Initial Implementation with linear search and for's
    [Benchmark]
    public void WordFinder()
    {
        var wordFinder = new WordFinder(_matrix);
        var result = wordFinder.Find(_wordStream);
    }

    // WordFinder Version 2 - Optimized Implementation
    // using built-in ".contains" method that is the fastest for search
    [Benchmark]
    public void WordFinderV1()
    {
        var wordFinder = new WordFinderV1(_matrix);
        var result = wordFinder.Find(_wordStream);
    }

    // WordFinder Version 2 - Optimized Implementation
    // using built-in ".contains" method that is the fastest for search 
    [Benchmark]
    public void WordFinderV2()
    {
        var wordFinder = new WordFinderV2(_matrix);
        var result = wordFinder.Find(_wordStream);
    }

    // WordFinder Version 3 - Refactored version using SOLID principles
    // adding the StringMatrix class for separation of concerns, and
    // reusability.
    [Benchmark]
    public void WordFinderV3()
    {
        var wordFinder = new WordFinderV3(_matrix);
        var result = wordFinder.Find(_wordStream);
    }

    // WordFinder Version 4 - Refactored code to use the new class
    // Matrix that is a generalization to use any data type not just text
    [Benchmark]
    public void WordFinderV4()
    {
        var wordFinder = new WordFinderV4(_matrix);
        var result = wordFinder.Find(_wordStream);
    }

    // WordFinder Version 5 - Refactor to reduce time with paralelization
    [Benchmark]
    public void WordFinderV5()
    {
        var wordFinder = new WordFinderV5(_matrix);
        var result = wordFinder.Find(_wordStream);
    }

    // WordFinder Version 6 - First version but refactored to use paralelization
    // to check if there is any advantage with those changes (it does not help)
    [Benchmark]
    public void WordFinderV6()
    {
        var wordFinder = new WordFinderV6(_matrix);
        var result = wordFinder.Find(_wordStream);
    }

    // WordFinder Version 6 - First version but refactored to use paralelization
    // to check if there is any advantage with those changes (it does not help)
    [Benchmark]
    public void WordFinderV7()
    {
        var wordFinder = new WordFinderV7(_matrix);
        var result = wordFinder.Find(_wordStream);
    }
}