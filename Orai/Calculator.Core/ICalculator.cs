namespace Calculator.Core;

public interface ICalculator
{
    Either<double, Exception> Calculate(string expression);
}
