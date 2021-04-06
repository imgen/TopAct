using System.Collections.Generic;
using System.Linq;

namespace TopAct.Common
{
    public static class CommonExtensions
    {
        public static bool IsNullOrWhiteSpace(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }

        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> sequence)
        {
            return sequence is null || sequence.Any() is false;
        }
    }
}
