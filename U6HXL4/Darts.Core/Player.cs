namespace Darts.Core;

public class Player
{

    public string name { get; set; }
    public int score { get; set; }
    public int tempScore { get; set; }
    public List<Throw> throws { get; set; } = new List<Throw>();
    public List<Throw> tempThrows { get; set; } = new List<Throw>();

    public bool tooMuchFlag { get; set; } = false;

    public Player(string name, int score)
    {
        this.name = name;
        this.score = score;
        this.tempScore = score;
    }

    public int getMaxThrow()
    {
        if (throws == null || throws.Count == 0)
        {
            return 0;
        }
        return throws.Max(t => t.score * t.multiplier);
    }

    public void PlayerThrows(Throw dartThrow)
    {
        tempScore = tempScore - (dartThrow.score * dartThrow.multiplier);
        if (tempScore >= 0)
        {
            tempThrows.Add(dartThrow);
        }
        else
            tooMuchFlag = true;
    }

    public bool isRoundOk()
    {
        if (tooMuchFlag)
        {
            tempScore = score;
            tooMuchFlag = false;
            tempThrows.Clear();
            return false;
        }
        if (tempScore >= 0)
        {
            score = tempScore;
            throws.AddRange(tempThrows);
            tempThrows.Clear();
            return true;
        }

        return false;
    }

    public bool isPlayerWonEarly()
    {
        return tempScore == 0;
    }

    public string turnString()
    {
        return ($"{name}'s turn");
    }

    public string scoreToString()
    {
        return ($"{score} points left");
    }
    public string tempScoreToString()
    {
        return ($"{tempScore} points left");
    }
}
