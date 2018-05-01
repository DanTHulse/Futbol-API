using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Futbol.API.Helpers
{
    public static class StringHelpers
    {
        /// <summary>
        /// To the int array.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>An array of ints from a delimited string</returns>
        public static int[] ToIntArray(this string data)
        {
            int[] array = new int[0];

            if (!string.IsNullOrEmpty(data))
            {
                array = data.Split(new Char[] { ',', '-', ':', ';', ' ', '_' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => Convert.ToInt32(s))
                    .ToArray();
            }

            return array;
        }
    }
}
