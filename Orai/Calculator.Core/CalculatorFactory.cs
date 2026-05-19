using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Core;

public static class CalculatorFactory
{
    public static ICalculator Create()
    {
        Tokenizer tokenizer = new Tokenizer(new TokenProvider());
        NumberStack numberStack = new NumberStack();

        return new Calculator(tokenizer, numberStack);
    }
}
