using TicTacToe.Core;

namespace TicTacToe.Client;
public class ClientClass
{
    private static int Main(string[] args)
    {
        GameFactory.Create();

        return 0;
    }


}
