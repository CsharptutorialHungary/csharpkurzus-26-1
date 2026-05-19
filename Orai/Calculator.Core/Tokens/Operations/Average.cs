namespace Calculator.Core.Tokens.Operations;

[TokenName("avg")]
internal sealed class Average : AggregateOperation
{
    public override int Precedence => OperationPrecedence.FunctionCall;

    protected override double Apply(IReadOnlyList<double> values) => values.Average();
}
