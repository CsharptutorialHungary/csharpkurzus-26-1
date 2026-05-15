using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.Core;

public class CursorController
{
    Field _current = null!;
    readonly FieldRenderer _renderer;
    private int _cursorX = 0;
    private int _cursorY = 0;
    public CursorController(FieldRenderer fieldRenderer)
    {
        _renderer = fieldRenderer;
        _current = _renderer.GetField(_cursorX, _cursorY);
        _current.setIsSelected(true);
    }

    public Field MoveCursor(byte direction)
    {
        Field neighbour = _renderer.GetNeighbour(_current, direction);

        if (neighbour == null)
            return null!;

        _current.setIsSelected(false);
        _current = neighbour;
        _current.setIsSelected(true);
        _cursorX = _current.getX();
        _cursorY = _current.getY();
        return _current;
    }
}
