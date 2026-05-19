namespace Calculator.Core.Tokens.Numbers;

[TokenName("pi")]
public class PiToken : NumberToken
{
    public PiToken() : base(Math.PI)
    {
    }
}
