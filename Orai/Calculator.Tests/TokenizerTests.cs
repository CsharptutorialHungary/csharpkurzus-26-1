using Calculator.Core;
using Calculator.Core.Tokens;

namespace Calculator.Tests;

internal class TokenizerTests
{
    [Test]
    public void Tokenize_ExpressionIsValid_ReturnsCorrectNumberOfTokens()
    {
        // Arrange
        Tokenizer tokenizer = new(new TokenProvider());

        // Act
        IReadOnlyList<IToken> tokens = tokenizer.Tokenize("3 4 +").ToList();

        // Assert
        Assert.That(tokens, Has.Exactly(3).Items);
    }

    [Test]
    public void Tokenize_ExpressionIsEmpty_ReturnsEmptyTokenList()
    {
        // Arrange
        Tokenizer tokenizer = new(new TokenProvider());

        // Act
        IReadOnlyList<IToken> tokens = tokenizer.Tokenize("").ToList();

        // Assert
        Assert.That(tokens, Is.Empty);
    }

    [Test]
    public void Tokenize_ExpressionContainsExtraSpaces_IgnoresExtraSpaces()
    {
        // Arrange
        Tokenizer tokenizer = new(new TokenProvider());

        // Act
        IReadOnlyList<IToken> tokens = tokenizer.Tokenize("  3   4   +  ").ToList();

        // Assert
        Assert.That(tokens, Has.Exactly(3).Items);
    }

    [Test]
    public void Tokenize_UnrecognizedToken_ThrowsInvalidOperationException()
    {
        // Arrange
        Tokenizer tokenizer = new(new TokenProvider());

        // Act & Assert
        Assert.That(() => tokenizer.Tokenize("3 4 §").ToList(), Throws.InvalidOperationException);
    }
}
