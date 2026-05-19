using Calculator.Core.Tokens.Operations;

namespace Calculator.Core.Tokens;

[TokenName("min")]
internal sealed class Minimum : AggregateOperation
{
    public override int Precedence => OperationPrecedence.FunctionCall;

    protected override double Apply(IReadOnlyList<double> values) => values.Min();
}