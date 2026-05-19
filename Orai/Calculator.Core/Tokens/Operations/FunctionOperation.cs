namespace Calculator.Core.Tokens.Operations;

internal class FunctionOperation : UnaryOperation
{
    private readonly SingleParamFunction _function;

    public delegate double SingleParamFunction(double number);

    public override int Precedence => OperationPrecedence.FunctionCall;

    public FunctionOperation(SingleParamFunction function)
    {
        _function = function;
    }

    protected override double Apply(double value)
        => _function(value);
}
