using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SzerepjatekCLI.Entities;

namespace SzerepjatekCLI.Fight;

public class Fight
{
    public void FightRound(Character player, Character enemy)
    {
        
    }
}

/*
 * Console.WriteLine($"Kezdődik a harc {player.Name} és {enemy.Name} között!");
        while (player.IsAlive && enemy.IsAlive)
        {
            // Player's turn
            Console.WriteLine($"{player.Name} támad!");
            int playerDamage = player.Attack();
            enemy.TakeDamage(playerDamage);
            Console.WriteLine($"{enemy.Name} sebződött {playerDamage} pontot! ({enemy.CurrentHealth}/{enemy.MaxHealth})");
            if (!enemy.IsAlive)
            {
                Console.WriteLine($"{enemy.Name} legyőzve!");
                break;
            }
            // Enemy's turn
            Console.WriteLine($"{enemy.Name} támad!");
            int enemyDamage = enemy.Attack();
            player.TakeDamage(enemyDamage);
            Console.WriteLine($"{player.Name} sebződött {enemyDamage} pontot! ({player.CurrentHealth}/{player.MaxHealth})");
            if (!player.IsAlive)
            {
                Console.WriteLine($"{player.Name} legyőzve!");
                break;
            }
        }
 */
