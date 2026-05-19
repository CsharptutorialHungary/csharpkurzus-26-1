namespace Calculator.Core;

public interface INumberStack
{
    int Count { get; }

    double Pop();

    void Push(double number);
}
