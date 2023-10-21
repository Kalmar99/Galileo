namespace Galileo;

public class Searcher
{
    private readonly SearchIndex _index;
    private readonly List<string> _documents;

    public Searcher(SearchIndex index, List<string> documents)
    {
        _index = index;
        _documents = documents;
    }

    public IReadOnlyCollection<string> Search(string searchQuery)
    {
        var termsGroups = searchQuery.ToLower().Split("and").ToList();

        var result = new List<string>();

        foreach (var termsGroup in termsGroups)
        {
            var terms = termsGroup.Split(" ").ToList();
            var results = SearchTerms(terms);
            result.AddRange(results);
        }

        if (ContainsAndOperator(termsGroups))
        {
            var resultsGroupedByContent = result.GroupBy(r => r);
            var resultsMatchingAllTerms = resultsGroupedByContent.Where(r => r.ToList().Count == termsGroups.Count);
            return resultsMatchingAllTerms.Select(g => g.Key).ToList();
        }

        return result;
    }

    private bool ContainsAndOperator(IReadOnlyCollection<string> termGroups) => termGroups.Count > 1;

    private IReadOnlyCollection<string> SearchTerms(IReadOnlyCollection<string> terms)
    {
        var searchResult = new List<string>();

        foreach (var term in terms)
        {
            var indexedDocuments = _index.GetDocumentsMatchingTerm(term);
            
            var documentsMatchingTerm = indexedDocuments.Select(i => _documents[i.DocumentPosition]);
            
            searchResult.AddRange(documentsMatchingTerm);
        }

        return searchResult.Distinct().ToList();
    }
}