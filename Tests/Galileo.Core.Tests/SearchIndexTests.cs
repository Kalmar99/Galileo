using Xunit;

namespace Galileo;

[Collection(nameof(Galileo))]
public class SearchIndexTests
{
    [Fact]
    public void SearchIndexCreate_CorrectlyCreatesIndex_WithCorrectDocumentPosition()
    {
        // Arrange
        var documents = new List<string>()
        {
            "BANANA",
            "APPLE"
        };

        var index = SearchIndex.Create(documents);

        // Act
        var indexResult = index.GetDocumentsMatchingTerm("APPLE");
        
        // Assert
        Assert.Single(indexResult);
        Assert.Equal(1, indexResult.First().DocumentPosition);
    }
    
    [Fact]
    public void GetDocumentsMatchingTerm_IsNotCaseSensitive()
    {
        // Arrange
        var documents = new List<string>()
        {
            "BanANA",
            "ApPLe"
        };

        var index = SearchIndex.Create(documents);

        // Act
        var indexResult = index.GetDocumentsMatchingTerm("APPLE");
        
        // Assert
        Assert.Single(indexResult);
        Assert.Equal(1, indexResult.First().DocumentPosition);
    }
}