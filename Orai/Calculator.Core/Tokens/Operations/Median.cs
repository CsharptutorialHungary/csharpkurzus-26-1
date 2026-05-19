using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Core.Tokens.Operations;

[TokenName("median")]
internal sealed class Median : AggregateOperation
{
    public override int Precedence => OperationPrecedence.FunctionCall;

    protected override double Apply(IReadOnlyList<double> values)
    {
        List<double> sortedValues = [.. values.Order()];

        int valueCount = sortedValues.Count;
        int middleIndex = valueCount / 2;

        return valueCount % 2 == 0
            ? sortedValues.Skip(middleIndex - 1).Take(2).Average()
            : sortedValues[middleIndex];
    }
}
