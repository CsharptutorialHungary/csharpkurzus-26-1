using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Core.Tokens.Operations;

internal abstract class AggregateOperation : Operation
{
    public override void Apply(INumberStack stack)
    {
        if (stack.Count < 1)
        {
            throw new InvalidOperationException("Not enough values on stack");
        }

        IReadOnlyList<double> values = stack.PopAll();

        double result = Apply(values);

        stack.Push(result);
    }

    protected abstract double Apply(IReadOnlyList<double> values);
}
