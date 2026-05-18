using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzerepjatekCLI.Actions;

public static class Dice
{
    private static readonly Random _random;

    static Dice()
    {
        _random = new Random();
    }

    public static bool TryLuck(int threshold)
    {
        int roll = _random.Next(1, 101); // 1-100 közötti szám generálása
        return roll <= threshold; // Sikeres, ha a dobás kisebb vagy egyenlő a küszöbnél
    }
    public static int Roll(int sides)
    {
        return _random.Next(1, sides + 1); // 1-sides közötti szám generálása
    }
}
