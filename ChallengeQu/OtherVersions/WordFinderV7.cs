using System;
namespace ChallengeQu;

public class WordFinderV7
{
    private readonly StringMatrixWithPrecomputeHashes StringMatrix;


    public WordFinderV7(IEnumerable<string> matrix)
    {
        StringMatrix = new StringMatrixWithPrecomputeHashes(matrix);
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

