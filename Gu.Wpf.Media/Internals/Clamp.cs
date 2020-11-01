namespace Gu.Wpf.Media
{
    using System;

    internal static class Clamp
    {
        internal static double Between(double value, double min, double max, int digits = -1)
        {
            if (value < min)
            {
                return min;
            }

            if (value > max)
            {
                return max;
            }

            if (digits >= 0)
            {
                return Math.Round(value, digits);
            }

            return value;
        }
    }
}
