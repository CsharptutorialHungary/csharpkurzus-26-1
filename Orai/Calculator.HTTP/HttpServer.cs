using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Calculator.HTTP;

public class HttpServer : IDisposable
{
    private readonly TcpListener _listener;
    private readonly SemaphoreSlim _semaphore;
    private readonly CancellationTokenSource _source;
    private readonly ILogger _logger;
    private readonly int _port;
    private bool _isDisposed;

    public List<IRequestHandler> RequestHandlers { get; }

    public HttpServer(ILogger logger, int port = 8080)
    {
        if (port < 0 || port > ushort.MaxValue)
            throw new ArgumentOutOfRangeException($"{nameof(port)} was in invalid range");
        _logger = logger;
        _port = port;
        _listener = new TcpListener(IPAddress.Any, port);
        _semaphore = new SemaphoreSlim(10);
        _source = new CancellationTokenSource();
        RequestHandlers = new List<IRequestHandler>();
    }

    ~HttpServer()
    {
        Dispose(false);
    }

    protected virtual void Dispose(bool isDisposeCall)
    {
        ObjectDisposedException.ThrowIf(_isDisposed, this);
        _listener.Dispose();
        _semaphore.Dispose();
        _source.Dispose();
        _isDisposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Start()
    {
        ObjectDisposedException.ThrowIf(_isDisposed, this);
        _listener.Start();
        _logger.Info("Server started");
        Task.Run(ListenerTask, _source.Token);
    }

    private async Task ListenerTask()
    {
        try
        {
            while (!_source.Token.IsCancellationRequested)
            {
                var client = await _listener.AcceptTcpClientAsync(_source.Token);
                try
                {
                    await _semaphore.WaitAsync(_source.Token);
                    await HandleClient(client, _source.Token);
                }
                finally
                {
                    _semaphore.Release();
                }
            }
        }
        catch (OperationCanceledException)
        {
            _logger.Info("Operation canceled");
        }
    }

    private async Task HandleClient(TcpClient client, CancellationToken token)
    {
        using NetworkStream stream = client.GetStream();
        try
        {
            HttpRequest request = await HttpRequestParser.ParseAsync(stream, _port);
            foreach (var handler in RequestHandlers)
            {
                bool result = await handler.HandleRequest(request, stream, token);
                if (result)
                {
                    return;
                }
            }
            await HandleNotFound(stream, token);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            await HandleInternalErrors(stream, token);
        }
    }

    private async Task HandleNotFound(NetworkStream stream, CancellationToken token)
    {
        string message = "Not found";

        string response = $"""
            HTTP/1.1 404 Not Found
            Date: {DateTime.UtcNow:R}
            Content-Type: text/plain; charset=utf-8
            Content-Length: {Encoding.UTF8.GetByteCount(message)}

            {message}
            """;

        using StreamWriter writer = new StreamWriter(stream, Encoding.UTF8, leaveOpen: true);
        await writer.WriteAsync(response.AsMemory(), token);
    }

    private async Task HandleInternalErrors(NetworkStream stream, CancellationToken token)
    {
        string message = "Internal server error";

        string response = $"""
            HTTP/1.1 500 Not Found
            Date: {DateTime.UtcNow:R}
            Content-Type: text/plain; charset=utf-8
            Content-Length: {Encoding.UTF8.GetByteCount(message)}

            {message}
            """;

        using StreamWriter writer = new StreamWriter(stream, Encoding.UTF8, leaveOpen: true);
        await writer.WriteAsync(response.AsMemory(), token);
    }

    public void Stop()
    {
        ObjectDisposedException.ThrowIf(_isDisposed, this);
        _source.Cancel();
        _listener.Stop();
        _logger.Info("Server stopped");
    }
}
