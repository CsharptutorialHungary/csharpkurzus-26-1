using System;
using System.Collections.Generic;
using System.Text;

using SzerepjatekCLI.Entities;
using SzerepjatekCLI.Items;
using SzerepjatekCLI.Story;

namespace SzerepjatekCLI.Core
{
    //mi történik éppen a játékban
    public record GameState
    {
        public Character Player { get; init; }
        public string CurrentLocation { get; init; }
        public List<Item> Inventory { get; init; } = new();

    }
}
