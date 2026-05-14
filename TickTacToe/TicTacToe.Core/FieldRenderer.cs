using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.Core;

public class FieldRenderer
{
    private static int _width;
    private static int _height;
    public FieldRenderer(int width, int height)
    {
        _width = width;
        _height = height;
    }

    public void GenerateField()
    {
        Field[,] field = new Field[_width, _height];
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                Console.Write("|_");
                field[i, j] = new Field(i, j);
            }
            Console.WriteLine("|");
        }
    }
}
