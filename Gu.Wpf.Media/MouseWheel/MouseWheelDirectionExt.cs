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
            return direction switch
            {
                MouseWheelDirection.None or MouseWheelDirection.Up or MouseWheelDirection.Down => true,
                _ => false,
            };
        }

        /// <summary>
        /// Get the string representation of <paramref name="direction"/>.
        /// </summary>
        /// <param name="direction"><see cref="MouseWheelDirection"/>.</param>
        /// <returns>The string representation.</returns>
        public static string ToName(this MouseWheelDirection direction)
        {
            return direction switch
            {
                MouseWheelDirection.None => nameof(MouseWheelDirection.None),
                MouseWheelDirection.Up => nameof(MouseWheelDirection.Up),
                MouseWheelDirection.Down => nameof(MouseWheelDirection.Down),
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null),
            };
        }
    }
}
