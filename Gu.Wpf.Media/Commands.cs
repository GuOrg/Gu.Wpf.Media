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

        public static RoutedUICommand ToggleMute { get; } = new RoutedUICommand(
                                                              "Toggle mute.",
                                                              nameof(ToggleMute),
                                                              typeof(Commands));
    }
}
