using Calculator.Core.Tokens;

namespace Calculator.Core;

internal class Calculator(ITokenizer tokenizer, INumberStack numberStack) : ICalculator
{
    public Either<double, Exception> Calculate(string expression)
    {
        try
        {
            foreach (IToken token in tokenizer.Tokenize(expression))
            {
                token.Apply(numberStack);
            }

            return numberStack.Pop();
        }
        catch (Exception e)
        {
            return e;
        }
    }
}