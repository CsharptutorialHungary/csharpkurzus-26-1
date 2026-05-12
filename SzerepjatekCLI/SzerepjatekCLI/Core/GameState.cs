using System;
using System.Collections.Generic;
using System.Text;

using SzerepjatekCLI.Entities;
using SzerepjatekCLI.Items;
using SzerepjatekCLI.Story;

namespace SzerepjatekCLI.Core
{
    //mi történik éppen a játékban, a mentéshez kell, ez kerül mentésre is, és ez alapján töltjük be a játékot
    public record GameState
    {
        public Player Player { get; set; }
        public string CurrentLocation { get; set; }
    }
}
