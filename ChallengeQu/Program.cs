// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Running;
using ChallengeQu;
/*
Console.WriteLine("WordFinder starts");

var matrix = new List<string>()
{
    "abcgc",
    "fgwio",
    "chill",
    "pqnsd",
    "uvdxy",
};

var wordFinder = new WordFinder(matrix);

var wordstream = new List<string>()
{
    "cold",
    "wind",
    "snow",
    "chill",
};

var results = wordFinder.Find(wordstream);

foreach (var w in results)
    Console.WriteLine(w);

Console.WriteLine("WordFinder ends");
Console.ReadLine();
*/

// Run the benchmark
var summary = BenchmarkRunner.Run<WordFinderBenchmark>();
Console.ReadLine();
