using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using ChallengeQu;
using System;
using System.Collections.Generic;
using System.Linq;

public class WordFinderBenchmark
{
    private readonly IEnumerable<string> _matrix;
    private readonly IEnumerable<string> _wordStream;

    // Initialize test data in the constructor or use Setup method
    public WordFinderBenchmark()
    {
        _matrix = new List<string>
        {
            "cats",
            "dogs",
            "rats",
            "mice"
        };

        _wordStream = new List<string>
        {
            "cat",
            "dog",
            "rat",
            "mice",
            "fox"
        };
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
    public void WordFinderV2()
    {
        var wordFinder = new WordFinder1(_matrix);
        var result = wordFinder.Find(_wordStream);
    }

    // WordFinder Version 3 - Parallelized Implementation
    [Benchmark]
    public void WordFinderV3()
    {
        var wordFinder = new WordFinder2(_matrix);
        var result = wordFinder.Find(_wordStream);
    }

    // WordFinder Version 3 - Parallelized Implementation
    [Benchmark]
    public void WordFinderV4()
    {
        var wordFinder = new WordFinder3(_matrix);
        var result = wordFinder.Find(_wordStream);
    }
}