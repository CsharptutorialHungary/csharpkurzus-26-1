using System;
using System.Collections.Generic;
using System.Text;

namespace SzerepjatekCLI.Entities
{
    internal class Warrior:Character
    {
        public Warrior() : base(120, 20) { }

        public override int Attack()
        {
            return AttackPower + Random.Shared.Next(0, 10);
        }

        public override void TakeDamage(int amount)
        {
            // tankosabb
            int reduced = amount - 3;
            base.TakeDamage(Math.Max(reduced, 0));
        }
    }
}
