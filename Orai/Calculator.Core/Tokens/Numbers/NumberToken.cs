namespace Calculator.Core.Tokens.Numbers;

public class NumberToken(double number) : IToken
{
    public void Apply(INumberStack stack)
    {
        stack.Push(number);
    }
}
