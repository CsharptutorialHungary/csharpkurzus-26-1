using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe.Core;

public class FieldRenderer
{
    private static int _width;
    private byte _isOver = 0; // 0 - game continues, 1 - X wins, 2 - O wins, 3 - draw
    private static int _mutchToWin; // Number of same symbols in a row needed to win
    private static int _height;
    readonly Field[,] _fields;
    public FieldRenderer(int width, int height, int mutchToWin = 3)
    {
        _width = width;
        _height = height;
        _fields = new Field[_width, _height];
        _mutchToWin = mutchToWin;
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
        // Felső szegély kirajzolása
        Console.WriteLine(new string('-', _height * 4 + 1));

        for (int i = 0; i < _width; i++)
        {
            Console.Write("|"); // Sor kezdete

            for (int j = 0; j < _height; j++)
            {
                Field currentField = _fields[i, j];

                // Kiválasztott mező jelölése szögletes zárójelekkel [X] vagy [O] vagy [ ]
                if (currentField.getIsSelected())
                {
                    Console.Write("[{0}]|", currentField.ToString());
                }
                else
                {
                    Console.Write(" {0} |", currentField.ToString());
                }
            }
            Console.WriteLine(); // Sor vége

            // Mezők közötti elválasztó vagy alsó szegély
            Console.WriteLine(new string('-', _height * 4 + 1));
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
    public byte checkWiner(Field nowPlaced)
    {
        byte state = nowPlaced.getState();
        if (state == 0) return 0;

        byte[][] directions = new byte[][]
        {
            [3, 4], // Vízszintes
            [1, 6], // Függőleges
            [0, 7], // Balfent-Joblent
            [2, 5]  // Ballent-Jobfent
        };

        foreach (var direction in directions)
        {
            int count = 1;

            foreach (var dir in direction)
            {
                Field current = nowPlaced;
                while (true)
                {
                    current = GetNeighbour(current, dir);
                    if (current == null || current.getState() != state)
                        break;

                    count++;
                }
            }

            if (count >= _mutchToWin)
            {
                _isOver = state;
                return state;
            }
        }

        bool isDraw = !_fields.Cast<Field>().Any(f => f.getState() == 0);

        if (isDraw)
        {
            _isOver = 3;
            return 3;
        }

        return 0; 
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
