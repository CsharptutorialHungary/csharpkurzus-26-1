using SzerepjatekCLI.Core;



public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Üdvözöllek a játékban!");
        Game game = new Game();
        game.Run();
    }
}