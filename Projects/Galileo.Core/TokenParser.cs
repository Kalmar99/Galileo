namespace Galileo;

public static class TokenParser
{
    public static List<TokenizedDocument> Parse(List<string> documents)
    {
        var tokenizedDocuments = new List<TokenizedDocument>();
        
        for (var index = 0; index < documents.Count; index++)
        {
            var document = documents[index];

            var tokenizedDocument = Parse(document, index);
            
            tokenizedDocuments.AddRange(tokenizedDocument);
        }

        return tokenizedDocuments;
    }
    
    private static IReadOnlyCollection<TokenizedDocument> Parse(string input, int position)
    {
        if (string.IsNullOrEmpty(input))
        {
            return new List<TokenizedDocument>(0);
        }
        
        var lowerCase = input.ToLower();

        var terms = lowerCase.Split(" ").GroupBy(s => s, (s, enumerable) => new KeyValuePair<string, int>(s, enumerable.Count()));

        return terms.Select(t => MapToTokenizedDocument(t, position)).ToList();
    }

    private static TokenizedDocument MapToTokenizedDocument(KeyValuePair<string, int> document, int position)
    {
        var term = document.Key;
        var countOfOccurences = document.Value;
        return new TokenizedDocument(position, term, countOfOccurences);
    }
}