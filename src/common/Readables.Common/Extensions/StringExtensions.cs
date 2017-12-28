using System;
namespace Readables.Common.Extensions
{
    public static class StringExtensions
    {
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source != null && toCheck != null && source.IndexOf(toCheck, comp) >= 0;
        }        

        public static bool IsPicture(this string str)
        {
            return str.EndsWith(".jpg", StringComparison.InvariantCultureIgnoreCase) ||
                str.EndsWith(".png", StringComparison.InvariantCultureIgnoreCase) ||
                str.EndsWith(".gif", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
