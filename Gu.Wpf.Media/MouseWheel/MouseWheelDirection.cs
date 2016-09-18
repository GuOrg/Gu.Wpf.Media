namespace Gu.Wpf.Media.MouseWheel
{
    /// <summary>
    /// A set of possible states for <see cref="MouseWheelGesture"/>
    /// </summary>
    public enum MouseWheelDirection
    {
        /// <summary>
        /// This corresponds to zero <see cref="System.Windows.Input.MouseWheelEventArgs.Delta"/>
        /// </summary>
        None,

        /// <summary>
        /// User scrolled content up.
        /// This corresponds to positive <see cref="System.Windows.Input.MouseWheelEventArgs.Delta"/>
        /// </summary>
        Up,

        /// <summary>
        /// User scrolled content down.
        /// This corresponds to negative <see cref="System.Windows.Input.MouseWheelEventArgs.Delta"/>
        /// </summary>
        Down
    }
}