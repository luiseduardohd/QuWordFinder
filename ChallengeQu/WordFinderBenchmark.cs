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

    // WordFinder Version 1 - Initial Implementation
    [Benchmark]
    public void WordFinder()
    {
        var wordFinder = new WordFinder(_matrix);
        var result = wordFinder.Find(_wordStream);
    }

    // WordFinder Version 2 - Optimized Implementation
    [Benchmark]
    public void WordFinderV1()
    {
        var wordFinder = new WordFinder1(_matrix);
        var result = wordFinder.Find(_wordStream);
    }

    // WordFinder Version 3 - Parallelized Implementation
    [Benchmark]
    public void WordFinderV2()
    {
        var wordFinder = new WordFinder2(_matrix);
        var result = wordFinder.Find(_wordStream);
    }

    // WordFinder Version 3 - Parallelized Implementation
    [Benchmark]
    public void WordFinderV3()
    {
        var wordFinder = new WordFinder3(_matrix);
        var result = wordFinder.Find(_wordStream);
    }

    // WordFinder Version 3 - Parallelized Implementation
    [Benchmark]
    public void WordFinderV4()
    {
        var wordFinder = new WordFinderV4(_matrix);
        var result = wordFinder.Find(_wordStream);
    }

    // WordFinder Version 3 - Parallelized Implementation
    [Benchmark]
    public void WordFinderV5()
    {
        var wordFinder = new WordFinderV5(_matrix);
        var result = wordFinder.Find(_wordStream);
    }
}