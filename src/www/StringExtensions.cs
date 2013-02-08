using System;

namespace nTestSwarm
{
    public static class StringExtensions
    {
        public static string ReplaceNullOrWhitespace(this string value, string defaultValue)
        {
            return ReplaceNullOrWhitespace(value, defaultValue, true);
        }

        public static string ReplaceNullOrWhitespace(this string value, string defaultValue, bool trimValue)
        {
            if (string.IsNullOrWhiteSpace(value))
                return defaultValue;
            else
                if (trimValue)
                    return value.Trim();
                else
                    return value;
        }

        public static string ReplaceNullOrWhitespace(this string value, Func<string> defaultValue)
        {
            return ReplaceNullOrWhitespace(value, defaultValue, true);
        }

        public static string ReplaceNullOrWhitespace(this string value, Func<string> defaultValue, bool trimValue)
        {
            if (string.IsNullOrWhiteSpace(value))
                return defaultValue();
            else
                if (trimValue)
                    return value.Trim();
                else
                    return value;
        }
    }
}