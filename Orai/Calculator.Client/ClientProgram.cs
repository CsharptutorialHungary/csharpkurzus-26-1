using System.Diagnostics;

using Calculator.Core;

internal class ClientProgram
{
    private static int Main(string[] args)
    {
        Console.WriteLine("Welcome to the calculator!");
        Console.Write("> ");

        string expression = Console.ReadLine() ?? string.Empty;

        ICalculator calculator = CalculatorFactory.Create();
        Either<double, Exception> result = calculator.Calculate(expression);

        if (result.TryGetSuccess(out double? validResult))
        {
            Console.WriteLine(validResult);
            return 0;
        }
        else if (result.TryGetError(out Exception? ex))
        {
            Console.WriteLine(ex.Message);
            return -1;
        }

        throw new UnreachableException("This shouldn't happen");
    }
}