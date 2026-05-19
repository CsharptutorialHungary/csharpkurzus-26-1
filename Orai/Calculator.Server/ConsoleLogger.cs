using Calculator.HTTP;

namespace Calculator.Server;

internal class ConsoleLogger : ILogger
{
    private readonly object _lock = new object();

    public void Error(string message)
    {
        lock (_lock)
        {
            Console.WriteLine(message);
        }

    }

    public void Info(string message)
    {
        lock (_lock)
        {
            Console.WriteLine(message);
        }
    }
}