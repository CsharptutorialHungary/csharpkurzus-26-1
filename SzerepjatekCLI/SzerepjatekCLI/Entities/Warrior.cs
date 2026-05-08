using System;
using System.Collections.Generic;
using System.Text;

namespace SzerepjatekCLI.Entities
{
    internal class Warrior:Player
    {
        public Warrior() { }

        public override int Attack()
        {
            return AttackPower + Random.Shared.Next(0, 10);
        }

        public override void TakeDamage(int amount)
        {
            int reduced = amount - 3;
            base.TakeDamage(Math.Max(reduced, 0));
        }
    }
}
