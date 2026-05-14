using Darts.Core;

namespace Darts.Client;

internal class Program
{
    private static int Main(string[] args)
    {
        int playerCount = 0;
        int startingPoint = 0;
        bool doubleIn;
        bool doubleOut;
        bool gameActive;
        Player currentPlayer;
        Game game;


        Console.WriteLine("Welcome to my Darts application!");
        Console.Write("> ");

        while (true)
        {

            while (true)
            {
                try
                {
                    Console.WriteLine("Player count: ");
                    playerCount = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Not a number!");
                }
            }
            while (true)
            {
                try
                {
                    Console.WriteLine("Starting point? (301, 501): ");
                    startingPoint = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Not a number!");
                }
            }
            Console.WriteLine("Double in? [Y/N]:   ---TODO---");
            doubleIn = Console.ReadLine()?.ToUpper() == "Y";

            Console.WriteLine("Double Out? [Y/N]:   ---TODO---");
            doubleOut = Console.ReadLine()?.ToUpper() == "Y";

            game = new(startingPoint, doubleIn, doubleOut);

            for (int i = 1; i < playerCount + 1; i++)
            {
                Console.WriteLine($"Player {i} name: ");
                string playerName = Console.ReadLine();
                if (playerName == "" || playerName == null)
                    playerName = $"Player{i}";
                game.players.Add(new Player(playerName, startingPoint));

            }

            Console.WriteLine("GAME CREATED! HAVE FUN!");


            ScoreboardConsoleWrite();

            while (true)
            {
                currentPlayer = game.getCurrentPlayer();

                //jatekoslog
                Console.WriteLine("");
                Console.WriteLine(currentPlayer.turnString());

                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine(currentPlayer.tempScoreToString());
                    Console.WriteLine("Write your throw in this form: multiplier point");
                    game.PlayerThrowsOneDart(Console.ReadLine()!);
                    if (currentPlayer.tooMuchFlag)
                    {
                        Console.WriteLine("Thats too much!!!!");
                        break;
                    }

                    if (currentPlayer.isPlayerWonEarly())
                    {
                        Console.WriteLine("THE WINNER!!!");
                        break;
                    }

                }
                currentPlayer.isRoundOk();
                Console.WriteLine(currentPlayer.scoreToString());

                if (game.isGameEnd())
                    break;
                game.nextPlayer();
            }


            ScoreboardConsoleWrite();
            maxThrow();
            Console.WriteLine();
            Console.WriteLine("Do you want to play again? Write: new");
            Console.WriteLine("Do you want to save the score and exit? Write: save");
            Console.WriteLine("Do you want to exit? Write: exit");
            string answer;
            while (true)
            {
                answer = Console.ReadLine();

                if (answer == "exit" || answer == "new" || answer == "save")
                {
                    break;
                }
            }
            if (answer == "new")
            {
                continue;
            }
            if (answer == "save")
            {
                game.saveScoreboard();
                Console.WriteLine("Scoreboard is saved under Documents/dartsscores");
                Console.ReadLine();
                Environment.Exit(0);
            }
            if (answer == "exit")
            {
                Environment.Exit(0);
            }
        }



        void ScoreboardConsoleWrite()
        {
            int i = 0;


            Console.WriteLine();
            Console.WriteLine("Scoreboard:");
            foreach (var player in game.Scoreboard())
            {
                i++;
                Console.WriteLine($"#{i}: Name: {player.Item1} , Points left: {player.Item2}");
            }

        }


        void maxThrow()
        {
            int i = 0;
            Console.WriteLine();
            Console.WriteLine("Biggest throws for each player");
            foreach (var player in game.players)
            {
                i++;
                Console.WriteLine($"#{i}: Name: {player.name} , Biggest: {player.getMaxThrow()}");
            }
        }
        return 0;
    }
}
