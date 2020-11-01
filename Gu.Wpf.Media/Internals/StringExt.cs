#pragma warning disable SA1600 // Elements must be documented

namespace Gu.Wpf.Media
{
    using System;

    internal static class StringExt
    {
        internal static string TrimEnd(this string text, string toTrim)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException(nameof(text));
            }

            if (!text.EndsWith(toTrim, StringComparison.Ordinal))
            {
                throw new ArgumentException($"The string {text} does not end with {toTrim}", nameof(text));
            }

            return text.Substring(0, text.Length - toTrim.Length);
        }
    }
}
