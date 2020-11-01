namespace Gu.Wpf.Media
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Windows.Data;
    using System.Windows.Markup;

    /// <summary>
    /// An <see cref="IValueConverter"/> that returns a formatted string of a <see cref="TimeSpan"/>.
    /// </summary>
    [ValueConversion(typeof(TimeSpan), typeof(string), ParameterType = typeof(string))]
    [MarkupExtensionReturnType(typeof(TimeSpanToStringConverter))]
    public sealed class TimeSpanToStringConverter : MarkupExtension, IValueConverter
    {
        /// <summary>
        /// The default instance used like Converter="{x:Static TimeSpanToStringConverter.Default}".
        /// </summary>
        public static readonly TimeSpanToStringConverter Default = new TimeSpanToStringConverter();

        // Expecting this to only be called from the UI-thread.
        // Sharing it and no locks.
        private static readonly StringBuilder StringBuilder = new StringBuilder();
        private static readonly string[] Formats =
        {
            @"h\:mm\:ss",
            @"h\:mm\:ss\.FFFFFFF",
            @"m\:ss",
            @"m\:ss\.FFFFFFF",
        };

        /// <summary>
        /// The string to return when the value is null.
        /// </summary>
        public string NullString { get; set; } = "-:--";

        /// <inheritdoc />
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
            {
                return this.NullString;
            }

            if (!IsValidFormat(parameter))
            {
                var message = this.CreateErrorMessage($"Expected parameter to be a valid format like 'fff' or 'FF', was: {parameter}");
                if (Is.InDesignMode)
                {
                    throw new ArgumentException(message, nameof(parameter));
                }

                return message;
            }

            if (value is TimeSpan timeSpan)
            {
                StringBuilder.Clear();
                if (timeSpan.Hours > 0)
                {
                    StringBuilder.Append(timeSpan.Hours.ToString(CultureInfo.InvariantCulture))
                                 .Append(':')
                                 .Append(timeSpan.Minutes.ToString("00"))
                                 .Append(':')
                                 .Append(timeSpan.Seconds.ToString("00"));
                }
                else
                {
                    StringBuilder.Append(timeSpan.Minutes)
                                 .Append(':')
                                 .Append(timeSpan.Seconds.ToString("00"));
                }

                var format = parameter as string;
                if (!string.IsNullOrEmpty(format))
                {
                    var fraction = timeSpan.ToString(format);
                    if (fraction != string.Empty)
                    {
                        StringBuilder.Append('.')
                                     .Append(fraction);
                    }
                }

                var formatted = StringBuilder.ToString();
                return formatted;
            }
            else
            {
                var message = this.CreateErrorMessage($"Expected a timespan, was {value}");
                if (Is.InDesignMode)
                {
                    throw new ArgumentException(message, nameof(parameter));
                }

                return message;
            }
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null || Equals(this.NullString, value))
            {
                return null;
            }

            if (!IsValidFormat(parameter))
            {
                var message = this.CreateErrorMessage($"Expected parameter to be a valid format like 'fff' or 'FF', was: {parameter}");
                if (Is.InDesignMode)
                {
                    throw new ArgumentException(message, nameof(parameter));
                }

                return message;
            }

            if (TryParse(value, out var result))
            {
                return result;
            }

            if (Is.InDesignMode)
            {
                var message = this.CreateErrorMessage($"Failed parsing a TimeSpan from {value}");
                throw new FormatException(message);
            }

            // Returning raw value letting the binding fail the framework way
            return value;
        }

        private static bool IsValidFormat(object parameter)
        {
            return parameter switch
            {
                null => true,
                string text => Regex.IsMatch(text, @"^(F{1,7}|f{1,7})$", RegexOptions.Singleline),
                _ => false,
            };
        }

        private static bool TryParse(object value, out TimeSpan result)
        {
            if (value is string text)
            {
                return TimeSpan.TryParseExact(text, Formats, CultureInfo.InvariantCulture, out result);
            }

            result = default;
            return false;
        }

        private string CreateErrorMessage(string message, [CallerMemberName] string caller = null)
        {
            return $"{this.GetType().FullName}.{caller} failed\r\n" + message;
        }
    }
}
