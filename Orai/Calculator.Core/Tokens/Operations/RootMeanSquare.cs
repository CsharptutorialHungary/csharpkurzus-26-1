namespace Calculator.Core.Tokens.Operations;

[TokenName("rms")]
internal sealed class RootMeanSquare : AggregateOperation
{
    public override int Precedence => OperationPrecedence.FunctionCall;

    protected override double Apply(IReadOnlyList<double> values)
    {
        double sumOfSquares = values.Sum(value => value * value);

        return Math.Sqrt(sumOfSquares / values.Count);
    }
}
