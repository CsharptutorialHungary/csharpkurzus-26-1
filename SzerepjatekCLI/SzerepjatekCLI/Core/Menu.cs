using System;
using System.Collections.Generic;
using System.Text;

using SzerepjatekCLI.Utils;

namespace SzerepjatekCLI.Core

{
    internal class Menu
    {

        public static int ShowMainMenu()
        {
            Console.WriteLine("Válassz egy lehetőséget:");
            Console.WriteLine("1. Új játék");
            Console.WriteLine("2. Játék betöltése");
            Console.WriteLine("3. Menü");
            return InputHandler.ReadIntInRange("Választás:", 1, 3);
        }
    }
}
