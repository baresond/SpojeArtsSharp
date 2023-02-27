using sas.Math;
using sas.Terminal;
using System;
using System.Runtime.InteropServices;

namespace sas
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Spoje Art's Sharp Terminal";

            Terminal.Terminal.Clear();
            while (true)
            {
                Console.Write("    > ");

                var line = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(line))
                    return;

                Commands.Execute(line);
            }
        }
    }
}