using System;

namespace Futbol.Importer.Helpers
{
    public static class ConsoleLog
    {
        public static void Start(string text)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($" - Started");
        }

        public static void Header(string text)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Write(text);
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static void Information(string text, string status)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($" - {status}");
        }

        public static void Error(string text, string status)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($" - {status}");
        }
    }
}
