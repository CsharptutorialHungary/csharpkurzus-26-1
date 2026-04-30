using System;
using System.Collections.Generic;
using System.Text;

namespace SzerepjatekCLI.Utils
{
    internal static class InputHandler
    {
        public static int ReadIntInRange(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write($"{prompt} ");

                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    WriteError("Üres bemenet!");
                    continue;
                }

                if (!int.TryParse(input, out int value))
                {
                    WriteError("Nem számot adott meg!");
                    continue;
                }

                if (value < min || value > max)
                {
                    WriteError($"Csak {min} és {max} között lehet!");
                    continue;
                }

                return value;
            }
        }

        internal static string ReadName(string v)
        {

            while (true)
            {
                Console.Write(v + " ");
                string? input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                    return input;
                   
                Console.WriteLine("A név nem lehet üres!");
            }
        }

        internal static string ReadString(string v)
        {

            while (true)
            {
                //Console.Write(v + " ");
                string? input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                    return input;

                Console.WriteLine("A név nem lehet üres!");
            }
        }


        private static void WriteError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
