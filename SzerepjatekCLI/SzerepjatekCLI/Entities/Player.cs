using System;
using System.Collections.Generic;
using System.Text;

namespace SzerepjatekCLI.Entities;

internal class Player
{
    public Character Character { get; }

    public Player(Character character)
    {
        Character = character;
    }
}
