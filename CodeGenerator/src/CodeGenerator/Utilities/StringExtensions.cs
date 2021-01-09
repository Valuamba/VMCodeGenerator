using System;
using System.Text.RegularExpressions;

namespace CodeGenerator.Utilities
{
    public static class StringExtensions
    {
        public static string ToLowerFirstChar(this string input)
        {
            return !string.IsNullOrEmpty(input) && char.IsUpper(input[0])
                ? char.ToLower(input[0]) + input.Substring(1)
                : input;
        }

        public static int ToInt(this string input)
        {
            return Convert.ToInt32(input.RemoveWhitespaces());
        }

        public static long ToRoundedLong(this string input)
        {
            return Convert.ToInt64(Math.Ceiling(input.RemoveWhitespaces().ToDouble()));
        }

        public static double ToDouble(this string input)
        {
            return Convert.ToDouble(input.Replace(",", "."));
        }

        public static double ToRoundedDouble(this string input, int digits)
        {
            return Math.Round(input.ToDouble(), digits);
        }

        private static string RemoveWhitespaces(this string input)
        {
            return Regex.Replace(input, @"\s+", "");
        }
    }
}
