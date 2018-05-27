using System;

namespace Futbol.Importer.Helpers
{
    public static class ConsoleLog
    {
        /// <summary>
        /// Logs start message.
        /// </summary>
        /// <param name="text">The text.</param>
        public static void Start(string text)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($" - Started");
        }

        /// <summary>
        /// Logs header message.
        /// </summary>
        /// <param name="text">The text.</param>
        public static void Header(string text)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Write(text);
            Console.BackgroundColor = ConsoleColor.Black;
        }

        /// <summary>
        /// Logs information message.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="status">The status.</param>
        public static void Information(string text, string status)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($" - {status}");
        }

        /// <summary>
        /// Logs error message
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="status">The status.</param>
        public static void Error(string text, string status)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($" - {status}");
        }

        /// <summary>
        /// Logs the options
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Options<T>() where T : struct, IConvertible
        {
            Type genericType = typeof(T);

            if (genericType.IsEnum)
            {
                foreach (T obj in Enum.GetValues(genericType))
                {
                    Enum enumType = Enum.Parse(typeof(T), obj.ToString()) as Enum;
                    int x = Convert.ToInt32(enumType); // x is the integer value of enum
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
