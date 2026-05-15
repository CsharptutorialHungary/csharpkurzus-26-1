using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.Core;

public class FieldRenderer
{
    private static int _width;
    private static int _height;
    readonly Field[,] _fields;
    public FieldRenderer(int width, int height)
    {
        _width = width;
        _height = height;
        _fields = new Field[_width, _height];
    }
    public int GetWidth()
    {
        return _width;
    }
    public int GetHeight()
    {
        return _height;
    }
    public Field GetField(int x, int y)
    {
        return _fields[x, y];
    }

    public void RenderField()
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                Console.Write("|_" + _fields[i, j]);

                if (_fields[i, j].getIsSelected())
                    Console.Write("^");
                 else
                    Console.Write(" ");

            }
            Console.WriteLine("|");
        }
    }
    public void GenerateField()
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                Console.Write("|_");
                _fields[i, j] = new Field(i, j);
            }
            Console.WriteLine("|");
        }
    }
    public Field GetNeighbour(Field original, byte index)
    {

        /*      0 1 2
                3   4
                5 6 7    
        */
        int originalX = original.getX();
        int originalY = original.getY();
        Field neighbour = null!;

        if (index > 7)
            return null!;

        try
        {
            switch (index)
            {
                case 0:
                    neighbour = GetField(originalX - 1, originalY - 1);
                    break;
                case 1:
                    neighbour = GetField(originalX - 1, originalY);
                    break;
                case 2:
                    neighbour = GetField(originalX - 1, originalY + 1);
                    break;
                case 3:
                    neighbour = GetField(originalX, originalY - 1);
                    break;
                case 4:
                    neighbour = GetField(originalX, originalY + 1);
                    break;
                case 5:
                    neighbour = GetField(originalX + 1, originalY - 1);
                    break;
                case 6:
                    neighbour = GetField(originalX + 1, originalY);
                    break;
                case 7:
                    neighbour = GetField(originalX + 1, originalY + 1);
                    break;
            }
        }
        catch (IndexOutOfRangeException)
        {
            return null!;
        }
        return neighbour;
    }
}
