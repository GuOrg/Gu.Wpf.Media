namespace Gu.Wpf.Media.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Windows.Data;

    [ValueConversion(typeof(TimeSpan), typeof(string), ParameterType = typeof(string))]
    public class TimeSpanToStringConverter : IValueConverter
    {
        public static readonly TimeSpanToStringConverter Default = new TimeSpanToStringConverter();

        private static readonly Dictionary<string, bool> CheckedFormatStrings = new Dictionary<string, bool>();

        // Expecting this to only be called from the UI-thread.
        // Sharing it and no locks.
        private static readonly StringBuilder StringBuilder = new StringBuilder();

        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!IsValidIntFormat(parameter))
            {
                return $"Expected parameter to be a valid format string for int, was {value?.GetType()}";
            }

            if (!(value is TimeSpan))
            {
                return $"Expected a timespan, was {value?.GetType()}";
            }

            StringBuilder.Clear();
            var timeSpan = (TimeSpan)value;
            if (timeSpan.Hours > 0)
            {
                StringBuilder.Append(timeSpan.Hours.ToString("00"))
                             .Append(":");
            }

            if (timeSpan.Hours > 0 || timeSpan.Minutes > 0)
            {
                StringBuilder.Append(timeSpan.Minutes.ToString("00"))
                             .Append(":");
            }

            StringBuilder.Append(timeSpan.Seconds.ToString("00"));

            var format = parameter as string;
            if (!string.IsNullOrEmpty(format))
            {
                StringBuilder.Append(".");
                StringBuilder.Append(timeSpan.Milliseconds.ToString("00"));
            }

            var formatted = StringBuilder.ToString();
            return formatted;
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TimeSpan result;
            if (TryParse(value, out result))
            {
                return result;
            }

            if (Is.InDesignMode)
            {
                throw new FormatException($"Failed parsing a TimeSpan from {value}");
            }

            // Not sure what is best here.
            return Binding.DoNothing;
        }

        private static bool IsValidIntFormat(object parameter)
        {
            if (parameter == null)
            {
                return true;
            }

            var format = parameter as string;
            if (format != null)
            {
                bool result;
                if (CheckedFormatStrings.TryGetValue(format, out result))
                {
                    return result;
                }

                try
                {
                    var text = 1.ToString(format);
                    var roundtrip = int.Parse(text);
                    result = roundtrip == 1;
                }
                catch (Exception)
                {
                    result = false;
                }

                CheckedFormatStrings[format] = result;
                return result;
            }

            return false;
        }

        private static bool TryParse(object value, out TimeSpan result)
        {
            var text = value as string;
            if (text == null)
            {
                result = default(TimeSpan);
                return false;
            }

            var pos = text.Length - 1;
            int temp;
            if (TryParseIntFromBack(text, ref pos, out temp))
            {
                result = default(TimeSpan);
                return false;
            }

            var ms = 0;
            int s;
            if (text[pos] == '.')
            {
                ms = temp;
            }
            else if (text[pos] == ':')
            {
                if (!IsInRange(temp, 0, 59))
                {
                    result = default(TimeSpan);
                    return false;
                }

                s = temp;
            }
            else if (pos == 0)
            {
                if (IsInRange(temp, 0, 59))
                {
                    result = TimeSpan.FromSeconds(temp);
                    return true;
                }

                result = default(TimeSpan);
                return false;
            }
            else
            {
                result = default(TimeSpan);
                return false;
            }

            var h = ParseInt(match.Groups["hours"]?.Value);
            if (!IsInRange(h, 0, 23))
            {
                result = default(TimeSpan);
                return false;
            }

            var m = ParseInt(match.Groups["minutes"]?.Value);
            if (!IsInRange(m, 0, 59))
            {
                result = default(TimeSpan);
                return false;
            }

            result = new TimeSpan(0, h, m, seconds, ms);
            return true;
        }

        private static bool TryParseIntFromBack(string text, ref int pos, out int result)
        {
            var end = pos;
            while (pos >= 0 && (char.IsDigit(text[pos]) || char.IsWhiteSpace(text[pos])))
            {
                pos--;
            }

            if (pos == end)
            {
                result = -1;
                return false;
            }

            var slice = text.Slice(pos, end);
            return int.TryParse(slice, out result);
        }

        private static bool IsInRange(int value, int min, int max)
        {
            if (value < min)
            {
                return false;
            }

            if (value > max)
            {
                return false;
            }

            return true;
        }
    }
}
