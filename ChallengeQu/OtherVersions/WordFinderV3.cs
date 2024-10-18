using System;
namespace ChallengeQu;

public class WordFinderV3
{
    private readonly StringMatrix StringMatrix;


    public WordFinderV3(IEnumerable<string> matrix)
    {
        StringMatrix = new StringMatrix(matrix);
    }
    public IEnumerable<string> Find(IEnumerable<string> wordstream)
    {
        return FindTopTen(wordstream);
    }

    private IEnumerable<string> FindTopTen(IEnumerable<string> wordstream)
    {
        return StringMatrix.FindMatches(wordstream).OrderByDescending(x => x.Value).Take(10).Select(x => x.Key);
    }
}

