using System;
using System.Collections.Generic;
using System.Text;

namespace SzerepjatekCLI.Entities
{
    public class Rogue : Player
    {
        public Rogue() 
        {
            Id = "player_rogue";
            MaxHealth = 80;
            CurrentHealth = MaxHealth;
            AttackPower = 15;
            Defense = 5;
        }

        public override int Attack()
        {
            // crit esély
            if (Random.Shared.Next(100) < 25)
            {
                return AttackPower * 2;
            }

            return AttackPower;
        }

        public override void TakeDamage(int amount)
        {
            // dodge esély
            if (Random.Shared.Next(100) < 30)
            {
                Console.WriteLine("Kikerülte a támadást!");
                return;
            }

            base.TakeDamage(amount);
        }
    }
}

