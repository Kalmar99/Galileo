using System.Collections.Concurrent;

namespace Galileo;

public class SearchIndex
{
    private readonly List<TokenizedDocument> _documents;

    private readonly ConcurrentDictionary<string, List<TokenizedDocument>> _index;

    public SearchIndex(List<TokenizedDocument> documents)
    {
        _documents = documents;
        _index = new ConcurrentDictionary<string, List<TokenizedDocument>>();
    }
    
    public void Build()
    {
        foreach (var document in _documents)
        {
            if (_index.TryGetValue(document.Term, out var documentsMatchingTerm))
            {
                documentsMatchingTerm.Add(document);
            }

            _index.TryAdd(document.Term, new List<TokenizedDocument>() { document });
        }
    }

    public IReadOnlyCollection<int> GetDocumentIndexMatchingTerm(string term)
    {
        if (_index.TryGetValue(term, out var documents))
        {
            return documents.Select(d => d.DocumentPosition).ToList();
        }

        return new List<int>();
    }
}