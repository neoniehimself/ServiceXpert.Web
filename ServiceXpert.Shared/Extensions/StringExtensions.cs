using System.Text.RegularExpressions;

namespace ServiceXpert.Shared.Extensions
{
    public static class StringExtensions
    {
        public static string SplitCasedWord(this string str)
        {
            string result = Regex.Replace(str, "(?<!^)([A-Z])", " $1");
            return char.ToUpper(result[0]) + result.Substring(1);
        }
    }
}
