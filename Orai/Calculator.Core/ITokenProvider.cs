using Calculator.Core.Tokens;

namespace Calculator.Core;

internal interface ITokenProvider
{
    IReadOnlyDictionary<string, IToken> Tokens { get; }
}