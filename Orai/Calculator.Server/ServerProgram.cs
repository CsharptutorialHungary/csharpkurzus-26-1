using Calculator.HTTP;
using Calculator.Server;

string frondend = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "frontend.html"));

using HttpServer server = new HttpServer(new ConsoleLogger(), 8080);
server.RequestHandlers.Add(new HtmlRequestHandler("/", frondend));
server.Start();
Console.ReadLine();
server.Stop();
return 0;