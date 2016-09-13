namespace Gu.Wpf.Media
{
    using System;
    using System.Windows;

    public partial class MediaElementWrapper
    {
        /// <summary>
        /// MediaFailedEvent is a routed event.
        /// </summary>
        public static readonly RoutedEvent MediaFailedEvent = EventManager.RegisterRoutedEvent(
            "MediaFailed",
            RoutingStrategy.Bubble,
            typeof(EventHandler<ExceptionRoutedEventArgs>),
            typeof(MediaElementWrapper));

        /// <summary>
        /// Raised when there is a failure in media.
        /// </summary>
        public event EventHandler<ExceptionRoutedEventArgs> MediaFailed
        {
            add
            {
                this.AddHandler(MediaFailedEvent, value);
            }
            remove
            {
                this.RemoveHandler(MediaFailedEvent, value);
            }
        }


        /// <summary>
        /// MediaOpened is a routed event.
        /// </summary>
        public static readonly RoutedEvent MediaOpenedEvent = EventManager.RegisterRoutedEvent(
            "MediaOpened",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(MediaElementWrapper));

        /// <summary>
        /// Raised when the media is opened
        /// </summary>
        public event RoutedEventHandler MediaOpened
        {
            add
            {
                this.AddHandler(MediaOpenedEvent, value);
            }
            remove
            {
                this.RemoveHandler(MediaOpenedEvent, value);
            }
        }

        /// <summary>
        /// BufferingStarted is a routed event.
        /// </summary>
        public static readonly RoutedEvent BufferingStartedEvent = EventManager.RegisterRoutedEvent(
            "BufferingStarted",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(MediaElementWrapper));

        /// <summary>
        /// Raised when buffering starts on the corresponding media.
        /// </summary>
        public event RoutedEventHandler BufferingStarted
        {
            add
            {
                this.AddHandler(BufferingStartedEvent, value);
            }
            remove
            {
                this.RemoveHandler(BufferingStartedEvent, value);
            }
        }

        /// <summary>
        /// BufferingEnded is a routed event.
        /// </summary>
        public static readonly RoutedEvent BufferingEndedEvent = EventManager.RegisterRoutedEvent(
            "BufferingEnded",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(MediaElementWrapper));

        /// <summary>
        /// Raised when buffering ends on the corresponding media.
        /// </summary>
        public event RoutedEventHandler BufferingEnded
        {
            add
            {
                this.AddHandler(BufferingEndedEvent, value);
            }
            remove
            {
                this.RemoveHandler(BufferingEndedEvent, value);
            }
        }

        /// <summary>
        /// ScriptCommand is a routed event.
        /// </summary>
        public static readonly RoutedEvent ScriptCommandEvent = EventManager.RegisterRoutedEvent(
            "ScriptCommand",
            RoutingStrategy.Bubble,
            typeof(EventHandler<MediaScriptCommandRoutedEventArgs>),
            typeof(MediaElementWrapper));

        /// <summary>
        /// Raised when a script command in the media is encountered during playback.
        /// </summary>
        public event EventHandler<MediaScriptCommandRoutedEventArgs> ScriptCommand
        {
            add
            {
                this.AddHandler(ScriptCommandEvent, value);
            }
            remove
            {
                this.RemoveHandler(ScriptCommandEvent, value);
            }
        }

        /// <summary>
        /// MediaEnded is a routed event
        /// </summary>
        public static readonly RoutedEvent MediaEndedEvent = EventManager.RegisterRoutedEvent(
            "MediaEnded",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(MediaElementWrapper));

        /// <summary>
        /// Raised when the corresponding media ends.
        /// </summary>
        public event RoutedEventHandler MediaEnded
        {
            add
            {
                this.AddHandler(MediaEndedEvent, value);
            }
            remove
            {
                this.RemoveHandler(MediaEndedEvent, value);
            }
        }
    }
}