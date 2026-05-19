using System;
using System.Collections.Generic;
using System.Text;

using Calculator.Core;

namespace Calculator.Tests;

internal class CalculatorTests
{
    [TestCase("11 11 +", 22d)]
    [TestCase("11 11 -", 0d)]
    [TestCase("11 3 *", 33d)]
    [TestCase("pi 2 / sin", 1d)]
    public void Calculate_Returns_Expected(string input, double expected)
    {
        ICalculator calculator = CalculatorFactory.Create();
        Either<double, Exception> actual = calculator.Calculate(input);

        bool result = actual.TryGetSuccess(out double? actualNumber);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.True);
            Assert.That(actualNumber, Is.EqualTo(expected));
        });
    }
}
