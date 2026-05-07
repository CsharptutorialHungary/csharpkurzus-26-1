using System;
using System.Collections.Generic;
using System.Text;

namespace SzerepjatekCLI.Items
{
    public interface Item
    {
        public int Id { get; set; }
        public int Weight { get; set; }
    }
}
