using System;

namespace Futbol.Importer.Helpers
{
    /// <summary>
    /// Extension methods for the Console
    /// </summary>
    public static class ConsoleEx
    {
        /// <summary>
        /// Reads the number input from the console, does not allow characters.
        /// </summary>
        /// <returns>string of numbers entered</returns>
        public static int ReadNumber()
        {
            string value = "";
            int numValue = 0;
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace)
                {
                    bool x = Int32.TryParse(key.KeyChar.ToString(), out numValue);
                    if (x)
                    {
                        value += key.KeyChar;
                        Console.Write(key.KeyChar);
                    }
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && value.Length > 0)
                    {
                        value = value.Substring(0, (value.Length - 1));
                        Console.Write("\b \b");
                    }
                }
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();

            numValue = Int32.TryParse(value, out int newNumValue) == true ? newNumValue : 0;

            return numValue;
        }
    }
}
