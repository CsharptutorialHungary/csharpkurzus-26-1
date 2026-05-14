using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;

namespace SzerepjatekCLI.Items
{
    public class Weapon:Item
    {
        public string Description { get; set; }
        ///public int Weight { get; set; }
        public int Damage { get; set; }
        public int Defense { get; set; }
     
        public Weapon()
        {
           Type = "Weapon";
        }

        public override string ToString()
        {
            return $"{Name} - {Description} - Súly: {Weight}";
        }
    }
}
