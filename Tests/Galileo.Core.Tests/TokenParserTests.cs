using Xunit;

namespace Galileo;

[Collection(nameof(Galileo))]
public class TokenParserTests
{
    [Fact]
    public void Parse_ReturnsTokenizedDocument_WithCorrectListPosition()
    {
        // Arrange
        var documents = new List<string>()
        {
            "banana",
            "apple"
        };

        // Act
        var tokenizedDocuments = TokenParser.Parse(documents);
        var tokenizedAppleDocument = tokenizedDocuments.FirstOrDefault(t => t.Term.Equals("apple"));
        
        // Assert
        Assert.NotNull(tokenizedAppleDocument);
        Assert.Equal(1, tokenizedAppleDocument.DocumentPosition);
    }
    
    [Fact]
    public void Parse_ReturnsTokenizedDocument_WithCorrectTermCount()
    {
        // Arrange
        var documents = new List<string>()
        {
            "banana",
            "john is eating an apple , lisa is also eating an apple"
        };

        // Act
        var tokenizedDocuments = TokenParser.Parse(documents);
        var tokenizedAppleDocument = tokenizedDocuments.FirstOrDefault(t => t.Term.Equals("apple"));
        
        // Assert
        Assert.NotNull(tokenizedAppleDocument);
        Assert.Equal(2, tokenizedAppleDocument.CountOfOccurrences);
    }
}