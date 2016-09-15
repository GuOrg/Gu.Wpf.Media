namespace Gu.Wpf.Media
{
    using System.Windows.Input;

    /// <summary>
    /// A set of Standard Commands
    /// </summary>
    public static class Commands
    {
        /// <summary>
        /// Toggle full screen command.
        /// </summary>
        public static RoutedUICommand ToggleFullScreen { get; } = new RoutedUICommand(
                                                                      "Toggle full screen.",
                                                                      nameof(ToggleFullScreen),
                                                                      typeof(Commands));

        /// <summary>
        /// Toggle sound on and off.
        /// </summary>
        public static RoutedUICommand ToggleMute { get; } = new RoutedUICommand(
                                                              "Toggle sound on and off.",
                                                              nameof(ToggleMute),
                                                              typeof(Commands));

        /// <summary>
        /// Turn sound on.
        /// </summary>
        public static RoutedUICommand UnmuteVolume { get; } = new RoutedUICommand(
                                                              "Turn sound on.",
                                                              nameof(UnmuteVolume),
                                                              typeof(Commands));
    }
}
