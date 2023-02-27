using sas.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sas.Terminal
{
    public static class Terminal
    {
        private static int sizeOfTab = 4;
        private static string welcomeText = "Welcome to Spoje Art's Sharp Terminal!";

        public static void Clear()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;
            //TO-DO: If posible add font size for header
            var outputLong =
                " ___  ___   ___      _  ___     _         _    _            _____                  _              _ " + Environment.NewLine +
                "/ __|| _ \\ / _ \\  _ | || __|   /_\\   _ _ | |_ ( )___  ___  |_   _|___  _ _  _ __  (_) _ _   __ _ | |" + Environment.NewLine +
                "\\__ \\|  _/| (_) || || || _|   / _ \\ | '_||  _||/(_-< |___|   | | / -_)| '_|| '  \\ | || ' \\ / _` || |" + Environment.NewLine +
                "|___/|_|   \\___/  \\__/ |___| /_/ \\_\\|_|   \\__|  /__/         |_| \\___||_|  |_|_|_||_||_||_|\\__,_||_|";
            var outpuShort =
                " ___    _      _ _" + Environment.NewLine +
                "/ __|  /_\\   _| | |_" + Environment.NewLine +
                "\\__ \\ / _ \\ |_  .  _|" + Environment.NewLine +
                "|___//_/ \\_\\|_     _|" + Environment.NewLine +
                "              |_|_|" + Environment.NewLine;

            //Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (welcomeText.Length / 2)) + "}" + Environment.NewLine, welcomeText));

            WriteOutput(outputLong, isTitle: true);
            WriteOutput(outpuShort, isTitle: true);

            Console.ResetColor();
        }

        public static void WriteHelp()
        {
            string output = string.Empty;

            output += $"{new string(' ', sizeOfTab)}Name: <Get-Help>{Environment.NewLine}";
            output += $"{new string(' ', sizeOfTab)}Syntax:{Environment.NewLine}"
                + $"{new string(' ', sizeOfTab * 2)}Get-Help {{[-Page] <int>}}{Environment.NewLine}"
                + $"{new string(' ', sizeOfTab * 2)}Get-Help {{[-Command] <string>}}{Environment.NewLine}";
            output += $"{new string(' ', sizeOfTab)}Aliases:{Environment.NewLine}"
                + $"{new string(' ', sizeOfTab * 2)}{"Get-Help".PadLeft(10)} | get-help{Environment.NewLine}"
                + $"{new string(' ', sizeOfTab * 2)}{"Help".PadLeft(10)} | help{Environment.NewLine}"
                + $"{new string(' ', sizeOfTab * 2)}{"h".PadLeft(10)} | h{Environment.NewLine}"
                + $"{new string(' ', sizeOfTab * 2)}{"?".PadLeft(10)}";
        }
        public static void WriteHelp(int pageNumber = 0)
        {
            WriteOutput("In Pregress...");
        }
        public static void WriteHelp(string command)
        {
            string output = string.Empty;

            switch(command.ToLower())
            {
                case "get-help":
                case "help":
                case "h":
                case "?":
                    output += $"{new string(' ', sizeOfTab)}Name: <Get-Help>{Environment.NewLine}";
                    output += $"{new string(' ', sizeOfTab)}Syntax:{Environment.NewLine}" 
                        + $"{new string(' ', sizeOfTab * 2)}Get-Help {{[-Page] <int>}}{Environment.NewLine}"
                        + $"{new string(' ', sizeOfTab * 2)}Get-Help {{[-Command] <string>}}{Environment.NewLine}";
                    output += $"{new string(' ', sizeOfTab)}Aliases:{Environment.NewLine}"
                        + $"{new string(' ', sizeOfTab * 2)}{"Get-Help".PadLeft(10)} | get-help{Environment.NewLine}"
                        + $"{new string(' ', sizeOfTab * 2)}{"Help".PadLeft(10)} | help{Environment.NewLine}"
                        + $"{new string(' ', sizeOfTab * 2)}{"h".PadLeft(10)} | h{Environment.NewLine}"
                        + $"{new string(' ', sizeOfTab * 2)}{"?".PadLeft(10)}";

                    WriteOutput(output);
                    break;
            }
        }

        public static void WriteOutput(string output, bool isError = false, bool isClear = false, bool isTitle = false)
        {
            int _sizeOfTab = 0;

            if (!isClear)
            {
                Console.WriteLine();

                if (isError) Console.ForegroundColor = ConsoleColor.DarkRed;
                else Console.ForegroundColor = ConsoleColor.DarkGreen;

                _sizeOfTab = sizeOfTab;
            }

            if (output.Contains(Environment.NewLine))
            {
                var lines = output.Split(Environment.NewLine);
                var length = lines.Max(line => line.Length);
                var leadingSpaceTitle = new string(' ', (Console.WindowWidth - length) / 2);
                var leadingSpaceRest = new string(' ', _sizeOfTab);

                if (isTitle)
                    Console.WriteLine(string.Join(Environment.NewLine, lines.Select(line => leadingSpaceTitle + line)));
                else
                    Console.WriteLine(string.Join(Environment.NewLine, lines.Select(line => leadingSpaceRest + line)));
            }
            else
                Console.WriteLine("{0}{1}", new string(' ', _sizeOfTab), output);

            Console.ResetColor();

            if (!isClear)
                Console.WriteLine();
        }

        public static void PrettyPrint(SyntaxNode node, string indent = "", bool isLast = true)
        {
            var marker = isLast ? "    └───" : "    ├───";

            Console.Write(indent);
            Console.Write(marker);
            Console.Write(node.Kind);

            if (node is SyntaxToken t && t.Value != null)
            {
                Console.Write(" ");
                Console.Write(t.Value);
            }

            Console.WriteLine();

            indent += isLast ? "        " : "    │   ";

            var lastChild = node.GetChildren().LastOrDefault();

            foreach (var child in node.GetChildren())
            {
                PrettyPrint(child, indent, lastChild == child);
            }
        }
    }
}
