using System;
using System.Collections.Generic;
using System.Text;

namespace SzerepjatekCLI.Entities
{
    internal class Mage : Player
    {
        public Mage()
        {
            Id = "player_mage";
            MaxHealth = 80;
            CurrentHealth = MaxHealth;
            AttackPower = 15;
            Defense = 5;
            //Speed = 10;
        }

        public override int Attack()
        {
            return AttackPower + Random.Shared.Next(5, 15);
        }

    }
}
