using System.Text.RegularExpressions;

namespace ServiceXpert.Domain.Shared.Extensions
{
    public static class StringExtension
    {
        public static string SplitCasedWord(this string str)
        {
            string result = Regex.Replace(str, "(?<!^)([A-Z])", " $1");
            return char.ToUpper(result[0]) + result.Substring(1);
        }

        public static bool EqualsOrdinalIgnoreCase(this string str, string stringToCompare)
        {
            return string.Equals(str, stringToCompare, StringComparison.OrdinalIgnoreCase);
        }
    }
}
