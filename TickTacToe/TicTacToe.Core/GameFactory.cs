using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace TicTacToe.Core;

public static class GameFactory
{
    public static GameController Create()
    {
        int  height = 3, width = 3, mutchToWin = 3; //default values
        Console.WriteLine("Please enter the width of the field: ");
        int.TryParse(Console.ReadLine(), out width);
        Console.WriteLine("Please enter the height of the field: ");
        int.TryParse(Console.ReadLine(), out height);
        Console.WriteLine("Please enter the number of same symbols in a row needed to win: ");
        int.TryParse(Console.ReadLine(), out mutchToWin);

        FieldRenderer fieldGenerator = new FieldRenderer(width, height, mutchToWin);
        fieldGenerator.GenerateField();
        CursorController cursorController = new CursorController(fieldGenerator);
        return new GameController(fieldGenerator, cursorController);
    }
}
