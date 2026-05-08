using System;
using System.Collections.Generic;
using System.Text;

using SzerepjatekCLI.Entities;

namespace SzerepjatekCLI.Items
{
    internal class Potion : Item
    {
        public int Weight { get; set; }
        public string Description { get; set; }
        public bool IsHealthPotion { get; set; }
        public int HealthModifier { get; set; }
        public string Type { get; set; }
        public Potion()
        {
            Type = "Potion";
        }
        public Character Drink(Character character)
        {
            if (IsHealthPotion)
            {
                character.CurrentHealth += HealthModifier;
                return character;
            }
            character.CurrentHealth -= HealthModifier;
            return character;
        }
        public override string ToString()
        {
            return $"{Name} - {Description} - Súly: {Weight}";
        }

    }
}
