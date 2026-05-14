using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.Core;

public class GameController
{
    readonly FieldRenderer _renderer;
    public GameController(FieldRenderer fieldRenderer) => _renderer = fieldRenderer;


}
