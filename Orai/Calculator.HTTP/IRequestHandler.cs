using System.Net.Sockets;

namespace Calculator.HTTP;

public interface IRequestHandler
{
    Task<bool> HandleRequest(HttpRequest request,
                             NetworkStream stream,
                             CancellationToken cancellationToken);
}
