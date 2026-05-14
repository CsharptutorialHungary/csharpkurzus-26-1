using System;
using System.Collections.Generic;
using System.Text;

namespace SzerepjatekCLI.Items
{
    public abstract class Item
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Weight { get; set; } = 0;
        public string Type { get; set; }


        public string toString()
        {
            return Name;
        }
    }

}
