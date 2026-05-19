namespace Calculator.Core.Tokens.Numbers;

[TokenName("e")]
public class EToken : NumberToken
{
    public EToken() : base(Math.E)
    {
    }
}
