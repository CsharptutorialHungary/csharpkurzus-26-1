namespace Calculator.Core.Tokens.Operations;

internal abstract class UnaryOperation : Operation
{
    public override void Apply(INumberStack stack)
    {
        if (stack.Count < 1)
        {
            throw new InvalidOperationException("Not enough values on stack");
        }

        double value = stack.Pop();

        double result = Apply(value);

        stack.Push(result);
    }

    protected abstract double Apply(double value);
}
