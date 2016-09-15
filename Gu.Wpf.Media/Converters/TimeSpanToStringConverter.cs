namespace Gu.Wpf.Media
{
    using System;
    using System.Globalization;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Windows.Data;
    using System.Windows.Markup;

    /// <inheritdoc />
    [ValueConversion(typeof(TimeSpan?), typeof(string), ParameterType = typeof(string))]
    [MarkupExtensionReturnType(typeof(IValueConverter))]
    public class TimeSpanToStringConverter : MarkupExtension, IValueConverter
    {
        /// <summary>
        /// The default instance used like Converter="{x:Static TimeSpanToStringConverter.Default}"
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
        /// Use this to configure how errors are handled at runtime.
        /// </summary>
        public ErrorHandlingStrategy RuntimeErrorHandling { get; set; } = ErrorHandlingStrategy.SilentFailure;

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
            if (value == null)
            {
                return this.NullString;
            }

            if (!IsValidFormat(parameter))
            {
                var message = $"Expected parameter to be a valid format like 'fff' or 'FF', was: {value?.GetType()}";
                if (Is.InDesignMode || this.RuntimeErrorHandling == ErrorHandlingStrategy.Throw)
                {
                    throw new ArgumentException(message, nameof(parameter));
                }

                return message;
            }

            if (!(value is TimeSpan))
            {
                var message = $"Expected a timespan, was {value?.GetType()}";
                if (Is.InDesignMode || this.RuntimeErrorHandling == ErrorHandlingStrategy.Throw)
                {
                    throw new ArgumentException(message, nameof(parameter));
                }

                return message;
            }

            StringBuilder.Clear();
            var timeSpan = (TimeSpan)value;
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

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || Equals(this.NullString, value))
            {
                return null;
            }

            if (!IsValidFormat(parameter))
            {
                var message = $"Expected parameter to be a valid format like 'fff' or 'FF', was: {value?.GetType()}";
                if (Is.InDesignMode || this.RuntimeErrorHandling == ErrorHandlingStrategy.Throw)
                {
                    throw new ArgumentException(message, nameof(parameter));
                }

                return Binding.DoNothing;
            }

            TimeSpan result;
            if (TryParse(value, out result))
            {
                return result;
            }

            if (Is.InDesignMode || this.RuntimeErrorHandling == ErrorHandlingStrategy.Throw)
            {
                throw new FormatException($"Failed parsing a TimeSpan from {value}");
            }

            return Binding.DoNothing;
        }

        private static bool IsValidFormat(object parameter)
        {
            if (parameter == null)
            {
                return true;
            }

            var text = parameter as string;
            if (text == null)
            {
                return false;
            }

            return Regex.IsMatch(text, @"^(F{1,7}|f{1,7})$", RegexOptions.Singleline);
        }

        private static bool TryParse(object value, out TimeSpan result)
        {
            var text = value as string;
            if (text == null)
            {
                result = default(TimeSpan);
                return false;
            }

            return TimeSpan.TryParseExact(text, Formats, CultureInfo.InvariantCulture, out result);
        }
    }
}
