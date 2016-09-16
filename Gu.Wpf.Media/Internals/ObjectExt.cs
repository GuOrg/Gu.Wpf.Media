#pragma warning disable SA1600 // Elements must be documented

namespace Gu.Wpf.Media
{
    using System;
    using System.Globalization;

    internal static class ObjectExt
    {
        internal static bool TryConvertToDouble(object value, out double result)
        {
            if (value == null || value is bool)
            {
                result = 0;
                return false;
            }

            if (value is double)
            {
                result = (double)value;
                return true;
            }

            var text = value as string;
            if (text != null)
            {
                return double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out result);
            }

            var convertible = value as IConvertible;
            if (convertible == null)
            {
                result = 0;
                return false;
            }

            try
            {
                result = convertible.ToDouble(CultureInfo.InvariantCulture);
                return true;
            }
            catch (Exception)
            {
                result = 0;
                return false;
            }
        }
    }
}