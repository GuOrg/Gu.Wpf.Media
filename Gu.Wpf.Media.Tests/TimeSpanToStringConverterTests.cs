namespace Gu.Wpf.Media.Tests
{
    using System;
    using System.Globalization;

    using Gu.Wpf.Media.Converters;

    using NUnit.Framework;

    public class TimeSpanToStringConverterTests
    {
        [TestCase("00:00:12", "12")]
        [TestCase("00:12:34", "12:34")]
        [TestCase("12:34:56", "12:34:56")]
        public void Roundtrip(string timeString, string expected)
        {
            var time = TimeSpan.ParseExact(timeString, @"hh\:mm\:ss", CultureInfo.InvariantCulture);
            var converted = (string)TimeSpanToStringConverter.Default.Convert(time, null, null, null);
            Assert.AreEqual(expected, converted);
            var roundtrip = (TimeSpan)TimeSpanToStringConverter.Default.ConvertBack(converted, null, null, null);
            Assert.AreEqual(time, roundtrip);
        }
    }
}
