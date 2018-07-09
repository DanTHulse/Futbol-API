using System;

namespace Futbol.Importer.Helpers
{
    public static class ConsoleLog
    {
        public static void Start(string text, string status = "")
        {
            if (string.IsNullOrEmpty(status))
            {
                status = " - Started";
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($" - {status}");
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
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Error(string text, string status)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($" - {status}");
        }

        public static void Options<T>() where T : struct, IConvertible
        {
            Type genericType = typeof(T);

            if (genericType.IsEnum)
            {
                foreach (T obj in Enum.GetValues(genericType))
                {
                    Enum enumType = Enum.Parse(typeof(T), obj.ToString()) as Enum;
                    int x = Convert.ToInt32(enumType);
                    var value = enumType.ToString();

                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"{x} - ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write($"{value}");
                }
            }
        }
    }
}
