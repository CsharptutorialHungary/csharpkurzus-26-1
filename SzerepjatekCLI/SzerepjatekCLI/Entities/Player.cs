using System;
using System.Collections.Generic;
using System.Text;

namespace SzerepjatekCLI.Entities;

public class Player : Character
{
    public Character Character { set; get; }

    public Player(Character character) : base(character.MaxHealth, character.AttackPower)
    {
        Character = character;
    }

    public override int Attack()
    {
        //ide kell majd megírni a harcrendszert vagy innen kell rá hivatkozni. Sőt, ezt kell meghívni a public harcnrendszerből
        return Character.AttackPower;
    }
}
