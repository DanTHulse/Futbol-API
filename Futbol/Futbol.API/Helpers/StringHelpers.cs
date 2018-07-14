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

        public static bool MatchWildcardString(this string input, string pattern)
        {
            if (string.Compare(input, pattern) == 0)
            {
                return true;
            }
            else if (string.IsNullOrEmpty(input))
            {
                if (string.IsNullOrEmpty(pattern.Trim(new char[1] { '*' })))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (pattern.Length == 0)
            {
                return false;
            }
            else if (pattern[0] == '?')
            {
                return MatchWildcardString(input.Substring(1), pattern.Substring(1));
            }
            else if (pattern[pattern.Length - 1] == '?')
            {
                return MatchWildcardString(input.Substring(0, input.Length - 1),
                                           pattern.Substring(0, pattern.Length - 1));
            }
            else if (pattern[0] == '*')
            {
                if (MatchWildcardString(input, pattern.Substring(1)))
                {
                    return true;
                }
                else
                {
                    return MatchWildcardString(input.Substring(1), pattern);
                }
            }
            else if (pattern[pattern.Length - 1] == '*')
            {
                if (MatchWildcardString(input, pattern.Substring(0, pattern.Length - 1)))
                {
                    return true;
                }
                else
                {
                    return MatchWildcardString(input.Substring(0, input.Length - 1), pattern);
                }
            }
            else if (pattern[0] == input[0])
            {
                return MatchWildcardString(input.Substring(1), pattern.Substring(1));
            }
            return false;
        }
    }
}
