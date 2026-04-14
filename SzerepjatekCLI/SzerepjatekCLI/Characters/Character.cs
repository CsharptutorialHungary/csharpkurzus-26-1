using System;
using System.Collections.Generic;
using System.Text;

namespace SzerepjatekCLI.Characters
{
    public record Character
    {
        public string Id { get; init; }
        public string Name { get; init; }

        public int MaxHealth { get; init; }
        public int CurrentHealth { get; init; }

        public int Attack { get; init; }
    }

}
