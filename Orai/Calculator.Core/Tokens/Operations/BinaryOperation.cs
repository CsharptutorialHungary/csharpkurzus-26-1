namespace Calculator.Core.Tokens.Operations;

internal abstract class BinaryOperation : Operation
{
    public override sealed void Apply(INumberStack stack)
    {
        if (stack.Count < 2)
        {
            throw new InvalidOperationException("Not enough values on stack");
        }

        double right = stack.Pop();
        double left = stack.Pop();

        double result = Apply(left, right);

        stack.Push(result);
    }

    protected abstract double Apply(double left, double right);
}
