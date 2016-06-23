using System.Text.RegularExpressions;

namespace FluentBehave.Tools
{
    public static class StringExtensions
    {
        public static string UppercaseFirst(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }

        public static string RemoveMultipleSpaces(this string s)
        {
            return Regex.Replace(s, @"\s+", " ");
        }

        public static string RemoveSpaces(this string s)
        {
            return Regex.Replace(s, @"\s+", string.Empty);
        }
    }
}
