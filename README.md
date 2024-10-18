# QuWordFinder


README:      I created this solution with this objective on mind:
             "The objective of this challenge is not necessarily just to solve the problem 
             but to evaluate your software development skills, code quality, analysis, creativity, 
             and resourcefulness as a potential future colleague."
             
             For this i tried to solve the problem but focused more on the code quality simplicity and extensibility
             rather than on speed, by example, the WordFinderV1 has the best speed to solve the problem, but i tried to
             apply SOLID principles to have classes with a single responsability, delegatin the matrix operations and search 
             to the StringMatrix class, and providing an option to extend or generalize the operations to work not only with
             strings but also with any data type with the generic class Matrix, at the end checking the benchmarks 
             the generalization works slower than the very optimized method "contains" of the dot net framework, 
             but the option is available to show the posibility.
             
             The code has been created trying to comply the most recent code guidelines from Microsoft:
             https://github.com/dotnet/runtime/blob/main/docs/coding-guidelines/coding-style.md
             
             The solution includes Unit tests and Benchmarks in order to show these skills also in the solution of a problem.
             
             There could be some minor improvements on the code but i think this is the cleanest and more understandable code, 
             so i choosed it as final version.

File:        Program.cs

Description: This class is responsible for finding the top N most frequent words in a matrix of strings.
             It provides functionality to search for words both horizontally and vertically.
             
             
             
Author:      Luis Hernandez

Created:     October 18, 2024

Version:     1.0

Modification History:
  - Version 1.0: Created by Luis Hernandez on October 18, 2024

