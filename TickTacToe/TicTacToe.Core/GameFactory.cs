using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace TicTacToe.Core;

public static class GameFactory
{
    public static GameController Create()
    {
        int height = 3, width = 3; //default values
        Console.WriteLine("Please enter the width of the field: ");
        int.TryParse(Console.ReadLine(), out width);
        Console.WriteLine("Please enter the height of the field: ");
        int.TryParse(Console.ReadLine(), out height);

        FieldRenderer fieldGenerator = new FieldRenderer(width, height);
        fieldGenerator.GenerateField();
        return new GameController(fieldGenerator);
    }
}
