namespace Calculator.Core;

public interface INumberStack
{
    int Count { get; }

    void Push(double number);
    double Pop();
}
