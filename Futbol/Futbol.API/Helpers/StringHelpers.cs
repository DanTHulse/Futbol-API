using System;
using System.Linq;

namespace Futbol.API.Helpers
{
    public static class StringHelpers
    {
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
