namespace Galileo;

public class TokenizedDocument
{
    public TokenizedDocument(int documentPosition, string term, int countOfOccurrences)
    {
        DocumentPosition = documentPosition;
        Term = term;
        CountOfOccurrences = countOfOccurrences;
    }

    public int DocumentPosition { get; }

    public string Term { get; }

    public int CountOfOccurrences { get; }
}