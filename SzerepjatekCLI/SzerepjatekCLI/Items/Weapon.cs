using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;

namespace SzerepjatekCLI.Items
{
    internal class Weapon:Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Damage { get; set; }
        public int Defense { get; set; }

        public int Weight { get; }
        public Weapon(string name, string description, int damage, int defense): base()
        {
            Name = name;
            Description = description;
            Damage = damage;
            Defense = defense;
        }

    }
}
