using Xunit;

namespace Galileo;

[Collection(nameof(Galileo))]
public class SearcherTests
{
    [Fact]
    public void Search_ReturnsOnlyDocuments_MatchingBothTerms_WhenQueryUsesAndOperator()
    {
        // Arrange
        var documents = new List<string>()
        {
            "BANANA APPLE",
            "BANANA"
        };

        var index = SearchIndex.Create(documents);

        var searcher = new Searcher(index, documents);
        
        // Act
        var result = searcher.Search("banana and apple");
        
        // Assert
        Assert.Single(result);
        Assert.Equal("BANANA APPLE", result.SingleOrDefault());
    }
    
    [Fact]
    public void Search_ReturnsBothDocuments_MatchingASearchTerm()
    {
        // Arrange
        var documents = new List<string>()
        {
            "BANANA APPLE",
            "BANANA"
        };

        var index = SearchIndex.Create(documents);

        var searcher = new Searcher(index, documents);
        
        // Act
        var result = searcher.Search("banana");
        
        // Assert
        Assert.Equal(2,result.Count);
    }
}