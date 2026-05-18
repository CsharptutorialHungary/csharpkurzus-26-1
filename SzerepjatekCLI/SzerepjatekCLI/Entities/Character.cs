using System;
using System.Collections.Generic;
using System.Text;

using SzerepjatekCLI.Items;

namespace SzerepjatekCLI.Entities
{
    public record CharacterStats(int MaxHealth, int AttackPower, int Defense, int CurrentHealth);
    public abstract class Character
    {
        public string Id { get; init; }
        public string Name { get; set; }
    
        public int MaxHealth { get; init; }
        public int CurrentHealth { get; set; }

        public int AttackPower { get; init; }
        public int Defense { get; init; }

        public Inventory Inventory { get; set; } = new();
        public CharacterStats Stats => new CharacterStats(MaxHealth, AttackPower, Defense, CurrentHealth);

        /*protected Character(int maxHealth, int attackPower)
        {
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
            AttackPower = attackPower;
        }*/

        public Character()
        {

        }

        public abstract int Attack();

        public virtual void TakeDamage(int amount)
        {
            CurrentHealth -= amount;
            if (CurrentHealth < 0) CurrentHealth = 0;
        }
        public void PrintStats(CharacterStats stats)
        {
            Console.WriteLine($"Max HP: {stats.MaxHealth}, Jelenlegi HP: {stats.CurrentHealth}, ATK: {stats.AttackPower}, DEF: {stats.Defense}");
        }

        public bool IsAlive => CurrentHealth > 0;

        public bool isLowHealth => CurrentHealth <= MaxHealth * 0.3;

    }
}
