using System;
using System.Collections.Generic;
using System.Text;

namespace SzerepjatekCLI.Entities
{
    internal class Mage : Player
    {
        public Mage(){ }

        public override int Attack()
        {
            return AttackPower + Random.Shared.Next(5, 15);
        }

    }
}
