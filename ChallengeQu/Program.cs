// See https://aka.ms/new-console-template for more information



/** 
 * 
 * README:      I created this solution with this objective on mind:
 *              "The objective of this challenge is not necessarily just to solve the problem 
 *              but to evaluate your software development skills, code quality, analysis, creativity, 
 *              and resourcefulness as a potential future colleague."
 *              
 *              For this i tried to solve the problem but focused more on the code quality simplicity and extensibility
 *              rather than on speed, by example, the WordFinderV1 has the best speed to solve the problem, but i tried to
 *              apply SOLID principles to have classes with a single responsability, delegatin the matrix operations and search 
 *              to the StringMatrix class, and providing an option to extend or generalize the operations to work not only with
 *              strings but also with any data type with the generic class Matrix, at the end checking the benchmarks 
 *              the generalization works slower than the very optimized method "contains" of the dot net framework, 
 *              but the option is available to show the posibility.
 *              
 *              The code has been created trying to comply the most recent code guidelines from Microsoft:
 *              https://github.com/dotnet/runtime/blob/main/docs/coding-guidelines/coding-style.md
 *              
 *              The solution includes Unit tests and Benchmarks in order to show these skills also in the solution of a problem.
 *              
 *              There could be some minor improvements on the code but i think this is the cleanest and more understandable code, 
 *              so i choosed it as final version.
 * 
 * File:        Program.cs
 * 
 * Description: This class is responsible for finding the top N most frequent words in a matrix of strings.
 *              It provides functionality to search for words both horizontally and vertically.
 *              
 *              
 *              
 * Author:      Luis Hernandez
 * 
 * Created:     October 18, 2024
 * 
 * Version:     1.0
 * 
 * Modification History:
 *   - Version 1.0: Created by Luis Hernandez on October 18, 2024
**/

using BenchmarkDotNet.Running;
using NUnitLite;
using ChallengeQu;

Console.WriteLine("Please press enter to start running Word Finder");
Console.ReadLine();

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
Console.WriteLine();


Console.WriteLine("Please press enter to start running Unit Tests");
Console.ReadLine();

new AutoRun().Execute(args);

#if ! DEBUG
Console.WriteLine("Please press enter to start running Benchmarks");
Console.ReadLine();

// Run the benchmark
var summary = BenchmarkRunner.Run<WordFinderBenchmark>();
#else
Console.WriteLine("In order to run benchmarks please run it on release configuration");
#endif

Console.WriteLine("Please press enter to Exit");
Console.ReadLine();

