using System;

namespace Futbol.Importer.Helpers
{
    public static class StringEx
    {
        /// <summary>
        /// Left function on string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <returns></returns>
        public static string Left(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            maxLength = Math.Abs(maxLength);

            return (value.Length <= maxLength
                   ? value
                   : value.Substring(0, maxLength)
                   );
        }
    }
}
