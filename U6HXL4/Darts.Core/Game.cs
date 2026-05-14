using System.Text.Json;

namespace Darts.Core;

public class Game
{
    public List<Player> players { get; set; } = new List<Player>();
    public int currentPlayer = 0;
    public int startingPoint { get; set; }
    public bool doubleIn { get; set; } ///TODO
    public bool doubleOut { get; set; }///TODO

    public Game(int startingPoint, bool doubleIn, bool doubleOut)
    {
        this.startingPoint = startingPoint;
        this.doubleIn = doubleIn;
        this.doubleOut = doubleOut;
    }

    public Game()
    {

    }
    public Player getCurrentPlayer()
    {
        return players[currentPlayer];
    }

    public void nextPlayer()
    {
        if (players.Count == 1)
            return;

        if (currentPlayer + 1 == players.Count)
        {
            currentPlayer = 0;
        }
        else
        {
            currentPlayer++;
        }
    }

    public bool PlayerThrowsOneDart(string dartThrow = "")
    {
        string[] parts = dartThrow.Split(' ');
        int multiplier = 1;
        int throwScore = 0;

        try
        {
            if (parts.Length == 1)
                throwScore = Convert.ToInt32(parts[0]);
            if (parts.Length > 1)
            {
                multiplier = Convert.ToInt32(parts[0]);
                throwScore = Convert.ToInt32(parts[1]);
            }
        }
        catch (FormatException)
        {
            return false;
        }

        if (multiplier < 1 || multiplier > 3 || !isThrowable(throwScore) || (multiplier == 3 && throwScore == 25))
            return false;

        Throw currentThrow = new Throw(throwScore, multiplier);
        getCurrentPlayer().PlayerThrows(currentThrow);

        return true;

    }

    private bool isThrowable(int number)
    {
        if (number < 0 || number > 25)
            return false;
        if (number > 20 && number < 25)
            return false;
        return true;
    }

    public bool isGameEnd()
    {
        return getCurrentPlayer().score == 0;
    }


    public List<(string, int)> Scoreboard()
    {
        List<(string, int)> results = [];

        results = players
            .OrderBy(p => p.score)
            .Select(p => (p.name, p.score))
            .ToList();
        return results;
    }

    public void saveScoreboard()
    {
        string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        docPath = Path.Combine(docPath, "dartsscores");
        Directory.CreateDirectory(docPath);
        string date = DateTime.Today.ToString("yyyy_MM_dd");
        string filePath = Path.Combine(docPath, $"Scoreboard_{date}.json");
        string jsonImportString = "";
        List<string>? result = new();


        int i = 0;



        if (File.Exists(filePath))
        {
            jsonImportString = File.ReadAllText(filePath);
            if (!string.IsNullOrWhiteSpace(jsonImportString))
            {
                var decodedJson = JsonSerializer.Deserialize<List<string>>(jsonImportString);
                result.AddRange(decodedJson!);
                result.Add("--------------------------------");
            }
        }

        result.Add(DateTime.Now.ToString("t"));
        result.Add("Scoreboard:");
        foreach (var player in Scoreboard())
        {
            i++;
            result.Add($"#{i}: Name: {player.Item1} , Points left: {player.Item2}");
        }

        i = 0;

        result.Add("Biggest throws for each player");
        foreach (var player in players)
        {
            i++;
            result.Add($"#{i}: Name: {player.name} , Biggest: {player.getMaxThrow()}");
        }

        string jsonString = JsonSerializer.Serialize(result);
        File.WriteAllText(filePath, jsonString);
    }
}