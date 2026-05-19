using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Calculator.HTTP;

public class HtmlRequestHandler : IRequestHandler
{
    private readonly string _serverPath;
    private readonly string _localPath;

    public HtmlRequestHandler(string serverPath, string localPath)
    {
        _serverPath = serverPath;
        _localPath = localPath;
    }

    public async Task<bool> HandleRequest(HttpRequest request,
                                    NetworkStream stream,
                                    CancellationToken cancellationToken)
    {
        if (request.Path.LocalPath != _serverPath)
            return false;

        if (!File.Exists(_localPath))
            return false;

        string message = File.ReadAllText(_localPath);

        string response = $"""
            HTTP/1.1 200 Ok
            Date: {DateTime.UtcNow:R}
            Content-Type: text/html; charset=utf-8
            Content-Length: {Encoding.UTF8.GetByteCount(message)}

            {message}
            """;

        var encoding = new UTF8Encoding(false);

        using StreamWriter writer = new StreamWriter(stream, encoding, leaveOpen: true);
        await writer.WriteAsync(response.AsMemory(), cancellationToken);

        return true;

    }
}