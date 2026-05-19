using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Core.Tokens.Operations;

[TokenName("sum")]
internal sealed class Sum : AggregateOperation
{
    public override int Precedence => OperationPrecedence.FunctionCall;

    protected override double Apply(IReadOnlyList<double> values) => values.Sum();
}
