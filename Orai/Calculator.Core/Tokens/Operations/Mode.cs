using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Core.Tokens.Operations;

[TokenName("mode")]
internal sealed class Mode : AggregateOperation
{
    public override int Precedence => OperationPrecedence.FunctionCall;

    protected override double Apply(IReadOnlyList<double> values)
    {
        return values
            .GroupBy(value => value)
            .OrderByDescending(group => group.Count())
            .ThenBy(group => group.Key)
            .First()
            .Key;
    }
}
