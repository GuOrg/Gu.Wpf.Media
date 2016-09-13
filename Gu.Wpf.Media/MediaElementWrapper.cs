namespace Gu.Wpf.Media
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Threading;

    public partial class MediaElementWrapper : Decorator
    {
        private readonly DispatcherTimer updatePositionTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(0.1) };
        private readonly DispatcherTimer updateProgressTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
        private readonly MediaElement mediaElement;

        /// <summary>Initializes a new instance of the <see cref="MediaElementWrapper"/> class.</summary>
        public MediaElementWrapper()
        {
            this.mediaElement = new MediaElement
            {
                LoadedBehavior = MediaState.Manual,
                UnloadedBehavior = MediaState.Manual,
            };

            this.mediaElement.MediaFailed += this.ReRaiseEvent;
            this.mediaElement.MediaEnded += this.ReRaiseEvent;
            this.mediaElement.BufferingStarted += (o, e) =>
                {
                    this.IsBuffering = true;
                    this.updateProgressTimer.Start();
                    this.ReRaiseEvent(o, e);
                };
            this.mediaElement.BufferingEnded += (o, e) =>
                {
                    this.IsBuffering = true;
                    this.updateProgressTimer.Start();
                    this.ReRaiseEvent(o, e);
                };
            this.mediaElement.ScriptCommand += this.ReRaiseEvent;
            this.mediaElement.MediaOpened += (o, e) =>
            {
                this.Pause();
                this.Length = this.mediaElement.NaturalDuration.TimeSpan;
                this.CanPauseMedia = this.mediaElement.CanPause;
                this.NaturalVideoHeight = this.mediaElement.NaturalVideoHeight;
                this.NaturalVideoWidth = this.mediaElement.NaturalVideoWidth;
                this.ReRaiseEvent(o, e);
                CommandManager.InvalidateRequerySuggested();
            };

            this.Bind(SourceProperty, MediaElement.SourceProperty);
            this.Bind(VolumeProperty, MediaElement.VolumeProperty);
            this.Bind(BalanceProperty, MediaElement.BalanceProperty);
            this.Bind(IsMutedProperty, MediaElement.IsMutedProperty);
            this.Bind(ScrubbingEnabledProperty, MediaElement.ScrubbingEnabledProperty);
            this.Bind(StretchProperty, MediaElement.StretchProperty);
            this.Bind(StretchDirectionProperty, MediaElement.StretchDirectionProperty);
            this.CommandBindings.Add(new CommandBinding(MediaCommands.Play, HandleExecute(this.Play), HandleCanExecute(this.CanPlay)));
            this.CommandBindings.Add(new CommandBinding(MediaCommands.Pause, HandleExecute(this.Pause), HandleCanExecute(this.CanPause)));
            this.CommandBindings.Add(new CommandBinding(MediaCommands.Stop, HandleExecute(this.Stop), HandleCanExecute(this.CanStop)));
            this.CommandBindings.Add(new CommandBinding(MediaCommands.TogglePlayPause, HandleExecute(this.TogglePlayPause), HandleCanExecute(() => this.CanPlay() || this.CanPause())));
            this.CommandBindings.Add(new CommandBinding(MediaCommands.Rewind, HandleExecute(this.Rewind), HandleCanExecute(this.CanRewind)));
            this.CommandBindings.Add(new CommandBinding(MediaCommands.IncreaseVolume, HandleExecute(() => this.IncreaseVolume(this.VolumeIncrement)), HandleCanExecute(this.CanIncreaseVolume)));
            this.CommandBindings.Add(new CommandBinding(MediaCommands.DecreaseVolume, HandleExecute(() => this.DecreaseVolume(this.VolumeIncrement)), HandleCanExecute(this.CanDecreaseVolume)));
            this.CommandBindings.Add(new CommandBinding(MediaCommands.MuteVolume, HandleExecute(this.Mute), HandleCanExecute(this.CanMute)));
            this.updatePositionTimer.Tick += (o, e) => this.Position = this.mediaElement.Position;
            this.updateProgressTimer.Tick += (o, e) =>
                {
                    this.DownloadProgress = this.mediaElement.DownloadProgress;
                    this.BufferingProgress = this.mediaElement.BufferingProgress;
                };
            Binding.AddTargetUpdatedHandler(this.mediaElement, this.OnTargetUpdated);
        }

        /// <inheritdoc/>
        public override void BeginInit()
        {
            this.Child = this.mediaElement;
            base.BeginInit();
        }

        /// <summary>
        /// Check if playback of current media, if any, can be started.
        /// </summary>
        /// <returns>True if media can be started.</returns>
        public bool CanPlay()
        {
            return this.mediaElement.Source != null && this.State != MediaState.Play;
        }

        /// <summary>
        /// Sets <see cref="State"/> to <see cref="MediaState.Play"/>
        /// </summary>
        public void Play()
        {
            this.State = MediaState.Play;
        }

        /// <summary>
        /// Check if current playback, if any, can be paused.
        /// </summary>
        /// <returns>True if media can be paused.</returns>
        public bool CanPause()
        {
            return this.mediaElement.Source != null && this.CanPauseMedia == true && this.State == MediaState.Play;
        }

        /// <summary>
        /// Sets <see cref="State"/> to <see cref="MediaState.Pause"/>
        /// </summary>
        public void Pause()
        {
            this.State = MediaState.Pause;
        }

        /// <summary>
        /// Toggles between <see cref="MediaState.Play"/> and <see cref="MediaState.Pause"/>
        /// Does nothing if no media is loaded.
        /// </summary>
        public void TogglePlayPause()
        {
            if (this.Length == null)
            {
                return;
            }

            this.State = this.State != MediaState.Play
                             ? MediaState.Play
                             : MediaState.Pause;
        }

        /// <summary>
        /// Check if current playback, if any, can be stopped.
        /// </summary>
        /// <returns>True if media can be stopped.</returns>
        public bool CanStop()
        {
            return this.mediaElement.Source != null && this.State == MediaState.Play;
        }

        /// <summary>
        /// Sets <see cref="State"/> to <see cref="MediaState.Pause"/>
        /// </summary>
        public void Stop()
        {
            this.State = MediaState.Stop;
        }

        /// <summary>
        /// Check if current playback, if any, can be rewound.
        /// </summary>
        /// <returns>True if media can be rewound.</returns>
        public bool CanRewind()
        {
            return this.mediaElement.Source != null && this.Position > TimeSpan.Zero;
        }

        /// <summary>
        /// Sets <see cref="Position"/> to <see cref="TimeSpan.Zero"/>
        /// </summary>
        public void Rewind()
        {
            this.Position = TimeSpan.Zero;
        }

        /// <summary>
        /// Check if <see cref="Volume"/> can be decreased.
        /// </summary>
        /// <returns>True if <see cref="Volume"/> can be decreased.</returns>
        public bool CanDecreaseVolume()
        {
            return this.Volume > 0;
        }

        /// <summary>
        /// <see cref="Volume"/> is a value between 0 and 1.
        /// Decreases <see cref="Volume"/> by <paramref name="increment"/>
        /// Truncates to between 0 and 1 if overflow.
        /// </summary>
        /// <param name="increment">A value between 0 and 1. Typical value is 0.05.</param>
        public void DecreaseVolume(double increment)
        {
            this.Volume = Math.Max(0, Math.Min(this.mediaElement.Volume - increment, 1));
        }

        /// <summary>
        /// Check if <see cref="Volume"/> can be increased.
        /// </summary>
        /// <returns>True if <see cref="Volume"/> can be increased.</returns>
        public bool CanIncreaseVolume()
        {
            return this.Volume < 1;
        }

        /// <summary>
        /// <see cref="Volume"/> is a value between 0 and 1.
        /// Increases <see cref="Volume"/> by <paramref name="increment"/>
        /// Truncates to between 0 and 1 if overflow.
        /// </summary>
        /// <param name="increment">A value between 0 and 1. Typical value is 0.05.</param>
        public void IncreaseVolume(double increment)
        {
            this.Volume = Math.Max(0, Math.Min(this.mediaElement.Volume + increment, 1));
        }

        /// <summary>
        /// Checks if audio can be muted.
        /// </summary>
        /// <returns>True if audio can be muted.</returns>
        public bool CanMute()
        {
            return !this.IsMuted;
        }

        /// <summary>
        /// Sets IsMuted = true
        /// </summary>
        public void Mute()
        {
            this.IsMuted = true;
        }

        private static void OnPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var wrapper = (MediaElementWrapper)d;
            if (wrapper.mediaElement != null)
            {
                wrapper.mediaElement.Position = (TimeSpan)e.NewValue;
            }
        }

        private static void OnStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var wrapper = (MediaElementWrapper)d;
            if (wrapper.mediaElement.Source == null)
            {
                return;
            }

            switch ((MediaState)e.NewValue)
            {
                case MediaState.Play:
                    wrapper.mediaElement.Play();
                    wrapper.updatePositionTimer.Start();
                    break;
                case MediaState.Close:
                    wrapper.mediaElement.Close();
                    wrapper.updatePositionTimer.Stop();
                    break;
                case MediaState.Pause:
                    wrapper.mediaElement.Pause();
                    wrapper.updatePositionTimer.Stop();
                    break;
                case MediaState.Stop:
                    wrapper.mediaElement.Stop();
                    wrapper.updatePositionTimer.Stop();
                    break;
                case MediaState.Manual:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static ExecutedRoutedEventHandler HandleExecute(Action action)
        {
            return (o, e) =>
                {
                    action();
                    e.Handled = true;
                };
        }

        private static CanExecuteRoutedEventHandler HandleCanExecute(Func<bool> canExecute)
        {
            return (o, e) =>
            {
                e.CanExecute = canExecute();
                e.Handled = true;
            };
        }

        private void ReRaiseEvent(object sender, RoutedEventArgs e)
        {
            this.RaiseEvent(e);
            e.Handled = true;
        }

        private void Bind(DependencyProperty sourceProperty, DependencyProperty targetProperty)
        {
            var binding = new Binding(sourceProperty.Name) { Mode = BindingMode.OneWay, Source = this, NotifyOnTargetUpdated = true };
            BindingOperations.SetBinding(this.mediaElement, targetProperty, binding);
        }

        private void OnTargetUpdated(object sender, DataTransferEventArgs e)
        {
            if (e.Property == MediaElement.SourceProperty)
            {
                if (this.mediaElement.Source != null)
                {
                    this.mediaElement.Play();
                }
            }
        }
    }
}
