using System;
using System.Collections.Generic;
using System.Text;

namespace SzerepjatekCLI.Entities
{
    internal class Mage : Character
    {
        public Mage() : base(80, 25) { }

        public override int Attack()
        {
            return AttackPower + Random.Shared.Next(5, 15);
        }
    }
}
