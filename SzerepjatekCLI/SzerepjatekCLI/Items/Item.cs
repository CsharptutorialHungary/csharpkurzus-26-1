using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SzerepjatekCLI.Items
{
    [JsonDerivedType(typeof(Weapon), "weapon")]
    [JsonDerivedType(typeof(Potion), "potion")]
    [JsonDerivedType(typeof(MoneyItem), "money")]
    [JsonPolymorphic()]
    public abstract class Item
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Weight { get; set; } = 0;

        public override string ToString()
        {
            return Name;
        }
    }

}
