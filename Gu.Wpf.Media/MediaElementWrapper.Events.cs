namespace Gu.Wpf.Media
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// A wrapper for <see cref="MediaElement"/> which provides a more sane API for usage from xaml.
    /// </summary>
    public partial class MediaElementWrapper
    {
#pragma warning disable SA1201 // Elements must appear in the correct order

        /// <summary>
        /// MediaFailedEvent is a routed event.
        /// </summary>
        public static readonly RoutedEvent MediaFailedEvent = MediaElement.MediaFailedEvent.AddOwner(typeof(MediaElementWrapper));

        /// <summary>
        /// Raised when there is a failure in media.
        /// </summary>
        public event EventHandler<ExceptionRoutedEventArgs> MediaFailed
        {
            add => this.AddHandler(MediaFailedEvent, value);
            remove => this.RemoveHandler(MediaFailedEvent, value);
        }

        /// <summary>
        /// MediaOpened is a routed event.
        /// </summary>
        public static readonly RoutedEvent MediaOpenedEvent = MediaElement.MediaOpenedEvent.AddOwner(typeof(MediaElementWrapper));

        /// <summary>
        /// Raised when the media is opened
        /// </summary>
        public event RoutedEventHandler MediaOpened
        {
            add => this.AddHandler(MediaOpenedEvent, value);
            remove => this.RemoveHandler(MediaOpenedEvent, value);
        }

        /// <summary>
        /// BufferingStarted is a routed event.
        /// </summary>
        public static readonly RoutedEvent BufferingStartedEvent = MediaElement.BufferingStartedEvent.AddOwner(typeof(MediaElementWrapper));

        /// <summary>
        /// Raised when buffering starts on the corresponding media.
        /// </summary>
        public event RoutedEventHandler BufferingStarted
        {
            add => this.AddHandler(BufferingStartedEvent, value);
            remove => this.RemoveHandler(BufferingStartedEvent, value);
        }

        /// <summary>
        /// BufferingEnded is a routed event.
        /// </summary>
        public static readonly RoutedEvent BufferingEndedEvent = MediaElement.BufferingEndedEvent.AddOwner(typeof(MediaElementWrapper));

        /// <summary>
        /// Raised when buffering ends on the corresponding media.
        /// </summary>
        public event RoutedEventHandler BufferingEnded
        {
            add => this.AddHandler(BufferingEndedEvent, value);
            remove => this.RemoveHandler(BufferingEndedEvent, value);
        }

        /// <summary>
        /// ScriptCommand is a routed event.
        /// </summary>
        public static readonly RoutedEvent ScriptCommandEvent = MediaElement.ScriptCommandEvent.AddOwner(typeof(MediaElementWrapper));

        /// <summary>
        /// Raised when a script command in the media is encountered during playback.
        /// </summary>
        public event EventHandler<MediaScriptCommandRoutedEventArgs> ScriptCommand
        {
            add => this.AddHandler(ScriptCommandEvent, value);
            remove => this.RemoveHandler(ScriptCommandEvent, value);
        }

        /// <summary>
        /// MediaEnded is a routed event.
        /// </summary>
        public static readonly RoutedEvent MediaEndedEvent = MediaElement.MediaEndedEvent.AddOwner(typeof(MediaElementWrapper));

        /// <summary>
        /// Raised when the corresponding media ends.
        /// </summary>
        public event RoutedEventHandler MediaEnded
        {
            add => this.AddHandler(MediaEndedEvent, value);
            remove => this.RemoveHandler(MediaEndedEvent, value);
        }

#pragma warning restore SA1201 // Elements must appear in the correct order
    }
}
