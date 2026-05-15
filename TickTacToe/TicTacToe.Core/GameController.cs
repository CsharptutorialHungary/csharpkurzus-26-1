using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.Core;

public class GameController
{
    readonly FieldRenderer _renderer;
    readonly CursorController _cursorController;
    private byte _gamestate = 0; // 0 - in menu, 1 - in game, 2 - game over
    private bool _currentPlayer = false; // false - player X, true - player O
    public GameController(FieldRenderer fieldRenderer, CursorController cursorController)
    {
        _renderer = fieldRenderer;
        _cursorController = cursorController;
        Console.WriteLine("Kérlek nyomj meg bármilyen gombot a játék elindításához!");
        Console.ReadKey();
        Start();
    }

    void Start()
    {
        _gamestate = 1;
        inGame();

    }

    void inMenu()
    {
        _gamestate = 0;
        Console.WriteLine("A játék megállítva");
        Console.WriteLine("Indításhoz nyomd meg az Esc billentyűt!");
        Console.ReadKey();
        Start();
    }

    void inGame()
    {
        Console.WriteLine("A játék elindult");
        Console.WriteLine("A játék az Esc billentyűvel megállítható");
        Console.WriteLine("A nyilak segítségével tudsz a pályán navigálni illetve a Space billentyűvel tudod lehelyezni a jelölődet");
        Console.WriteLine("A játékot a {0} karakter kezdi.", _currentPlayer ? "O" : "X");
        _renderer.RenderField();
        ConsoleKey key;
        Field sellectedField = _renderer.GetField(0, 0); // Initialize with default

        while ((key = Console.ReadKey(true).Key) != ConsoleKey.Escape)
        {
            bool placeSuccessful = false;
            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    sellectedField = _cursorController.MoveCursor(3) ?? sellectedField;
                    break;
                case ConsoleKey.RightArrow:
                    sellectedField = _cursorController.MoveCursor(4) ?? sellectedField;
                    break;
                case ConsoleKey.UpArrow:
                    sellectedField = _cursorController.MoveCursor(1) ?? sellectedField;
                    break;
                case ConsoleKey.DownArrow:
                    sellectedField = _cursorController.MoveCursor(6) ?? sellectedField;
                    break;
                case ConsoleKey.Spacebar:
                    placeSuccessful = sellectedField.setState((byte)(_currentPlayer ? 2 : 1));

                    if (placeSuccessful)
                        _currentPlayer = !_currentPlayer;

                    break;

            }
            if (sellectedField == null)
            {
                Console.WriteLine("Érvénytelen mozgás!");
                continue;
            }

            Console.Clear();
            _renderer.RenderField();

            if (placeSuccessful)
                Console.WriteLine("A(z) {0} játékos lehelyezte a jelölőjét", !_currentPlayer ? "O" : "X");
        }
        inMenu();

/*      0 1 2
        3   4
        5 6 7    
*/
    }

}
