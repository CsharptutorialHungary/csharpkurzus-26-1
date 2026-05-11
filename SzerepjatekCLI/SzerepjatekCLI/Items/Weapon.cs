using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;

namespace SzerepjatekCLI.Items
{
    internal class Weapon : Item
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Damage { get; set; }

        public int Defense { get; set; }

        public int Weight { get; set; }

        public Weapon()
        {
        }

        public Weapon(
            int id,
            string name,
            string description,
            int damage,
            int defense,
            int weight)
        {
            Id = id;
            Name = name;
            Description = description;
            Damage = damage;
            Defense = defense;
            Weight = weight;
        }
    }
}
