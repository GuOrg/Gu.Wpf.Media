namespace Gu.Wpf.Media
{
    using System;

    /// <summary>
    /// Helper methods for <see cref="MouseWheelDirection"/>.
    /// </summary>
    public static class MouseWheelDirectionExt
    {
        /// <summary>
        /// Get if <paramref name="direction"/> si a valid value.
        /// </summary>
        /// <param name="direction"><see cref="MouseWheelDirection"/>.</param>
        /// <returns>True if <paramref name="direction"/> is valid.</returns>
        public static bool IsDefined(MouseWheelDirection direction)
        {
            switch (direction)
            {
                case MouseWheelDirection.None:
                case MouseWheelDirection.Up:
                case MouseWheelDirection.Down:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Get the string representation of <paramref name="direction"/>.
        /// </summary>
        /// <param name="direction"><see cref="MouseWheelDirection"/>.</param>
        /// <returns>The string representation.</returns>
        public static string ToName(this MouseWheelDirection direction)
        {
            switch (direction)
            {
                case MouseWheelDirection.None:
                    return nameof(MouseWheelDirection.None);
                case MouseWheelDirection.Up:
                    return nameof(MouseWheelDirection.Up);
                case MouseWheelDirection.Down:
                    return nameof(MouseWheelDirection.Down);
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }
    }
}