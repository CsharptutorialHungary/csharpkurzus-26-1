namespace Calculator.Core.Tokens.Operations;

[TokenName("max")]
internal sealed class Maximum : AggregateOperation
{
    public override int Precedence => OperationPrecedence.FunctionCall;

    protected override double Apply(IReadOnlyList<double> values) => values.Max();
}
