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
        var terms = searchQuery.Split(" ").ToList();
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