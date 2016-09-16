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
                LoadedBehavior = System.Windows.Controls.MediaState.Manual,
                UnloadedBehavior = System.Windows.Controls.MediaState.Manual,
            };

            this.mediaElement.MediaFailed += (o, e) =>
                {
                    this.HasMedia = false;
                    this.HasAudio = null;
                    this.HasVideo = null;
                    this.CanPauseMedia = null;
                    this.NaturalVideoHeight = null;
                    this.NaturalVideoWidth = null;
                    this.Length = null;
                    this.SetCurrentValue(PositionProperty, null);
                    this.ReRaiseEvent(o, e);
                    CommandManager.InvalidateRequerySuggested();
                };

            this.mediaElement.MediaEnded += (o, e) =>
                {
                    this.SetCurrentValue(StateProperty, MediaState.Stop);
                    this.ReRaiseEvent(o, e);
                };

            this.mediaElement.BufferingStarted += (o, e) =>
                {
                    this.IsBuffering = true;
                    this.updateProgressTimer.Start();
                    this.ReRaiseEvent(o, e);
                };

            this.mediaElement.BufferingEnded += (o, e) =>
                {
                    this.IsBuffering = true;
                    this.updateProgressTimer.Stop();
                    this.ReRaiseEvent(o, e);
                };

            this.mediaElement.ScriptCommand += this.ReRaiseEvent;
            this.mediaElement.MediaOpened += (o, e) =>
            {
                switch (this.LoadedBehavior)
                {
                    case MediaState.Stop:
                        this.Stop();
                        break;
                    case MediaState.Play:
                        this.Play();
                        break;
                    case MediaState.Pause:
                        this.Pause();
                        this.SetCurrentValue(PositionProperty, TimeSpan.Zero);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                this.HasMedia = true;
                this.HasAudio = this.mediaElement.HasAudio;
                this.HasVideo = this.mediaElement.HasVideo;
                this.CanPauseMedia = this.mediaElement.CanPause;
                this.NaturalVideoHeight = this.mediaElement.NaturalVideoHeight;
                this.NaturalVideoWidth = this.mediaElement.NaturalVideoWidth;
                this.Length = this.mediaElement.NaturalDuration.TimeSpan;
                this.SetCurrentValue(PositionProperty, this.mediaElement.Position);
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
            this.CommandBindings.Add(new CommandBinding(MediaCommands.IncreaseVolume, HandleExecute(parameter => this.IncreaseVolume(this.GetVolumeIncrementOrDefault(parameter))), HandleCanExecute(this.CanIncreaseVolume)));
            this.CommandBindings.Add(new CommandBinding(MediaCommands.DecreaseVolume, HandleExecute(parameter => this.DecreaseVolume(this.GetVolumeIncrementOrDefault(parameter))), HandleCanExecute(this.CanDecreaseVolume)));
            this.CommandBindings.Add(new CommandBinding(MediaCommands.MuteVolume, HandleExecute(this.MuteVolume), HandleCanExecute(this.CanMuteVolume)));
            this.CommandBindings.Add(new CommandBinding(Commands.UnmuteVolume, HandleExecute(this.UnmuteVolume), HandleCanExecute(this.CanUnmuteVolume)));
            this.CommandBindings.Add(new CommandBinding(Commands.ToggleMute, HandleExecute(this.ToggleMute), HandleCanExecute(this.CanToggleMute)));
            this.updatePositionTimer.Tick += (o, e) => this.SetCurrentValue(PositionProperty, this.mediaElement.Position);
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
            this.SetCurrentValue(StateProperty, MediaState.Play);
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
            this.SetCurrentValue(StateProperty, MediaState.Pause);
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

            var mediaState = this.State != MediaState.Play
                ? MediaState.Play
                : MediaState.Pause;
            this.SetCurrentValue(StateProperty, mediaState);
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
            this.SetCurrentValue(StateProperty, MediaState.Stop);
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
            this.SetCurrentValue(PositionProperty, TimeSpan.Zero);
        }

        /// <summary>
        /// Check if <see cref="Volume"/> can be decreased.
        /// </summary>
        /// <returns>True if <see cref="Volume"/> can be decreased.</returns>
        public bool CanDecreaseVolume()
        {
            return this.Volume > 0 && !this.IsMuted;
        }

        /// <summary>
        /// <see cref="Volume"/> is a value between 0 and 1.
        /// Decreases <see cref="Volume"/> by <paramref name="increment"/>
        /// Truncates to between 0 and 1 if overflow.
        /// </summary>
        /// <param name="increment">
        /// A value between 0 and 1. Typical value is 0.05.
        /// If null <see cref="VolumeIncrement"/> is used.
        /// </param>
        public void DecreaseVolume(double increment)
        {
            this.SetCurrentValue(VolumeProperty, Clamp.Between(this.mediaElement.Volume - increment, 0, 1, 3));
            this.SetCurrentValue(IsMutedProperty, this.Volume <= 0);
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
        /// <param name="increment">
        /// A value between 0 and 1. Typical value is 0.05.
        /// If null <see cref="VolumeIncrement"/> is used.
        /// </param>
        public void IncreaseVolume(double increment)
        {
            if (this.IsMuted)
            {
                this.SetCurrentValue(VolumeProperty, Clamp.Between(increment, 0, 1, 3));
                this.SetCurrentValue(IsMutedProperty, this.Volume <= 0);
                return;
            }

            this.SetCurrentValue(VolumeProperty, Clamp.Between(this.mediaElement.Volume + increment, 0, 1, 3));
        }

        /// <summary>
        /// Checks if audio can be muted.
        /// </summary>
        /// <returns>True if audio can be muted.</returns>
        public bool CanMuteVolume()
        {
            return !this.IsMuted && this.Volume > 0;
        }

        /// <summary>
        /// Sets IsMuted = true
        /// </summary>
        public void MuteVolume()
        {
            this.SetCurrentValue(IsMutedProperty, true);
        }

        /// <summary>
        /// Checks if audio can be turned on.
        /// Resturns false if volume is zero.
        /// </summary>
        /// <returns>True if audio can be muted.</returns>
        public bool CanUnmuteVolume()
        {
            return this.IsMuted && this.Volume > 0;
        }

        /// <summary>
        /// Turns sound on by setting IsMuted = false
        /// </summary>
        public void UnmuteVolume()
        {
            this.SetCurrentValue(IsMutedProperty, false);
        }

        /// <summary>
        /// Checks if sound can be muted.
        /// </summary>
        /// <returns>True if sound can be muted.</returns>
        public bool CanToggleMute()
        {
            return this.Volume > 0;
        }

        /// <summary>
        /// Sets IsMuted = !Muted
        /// </summary>
        public void ToggleMute()
        {
            if (this.Volume <= 0)
            {
                this.SetCurrentValue(IsMutedProperty, true);
                return;
            }

            this.SetCurrentValue(IsMutedProperty, !this.IsMuted);
        }

        /// <summary>
        /// Pauses the player without changing the <see cref="State"/> and <see cref="IsPlaying"/> properties.
        /// </summary>
        public void Break()
        {
            this.mediaElement.Pause();
        }

        /// <summary>
        /// Resumes playing without changing the <see cref="State"/> and <see cref="IsPlaying"/> properties.
        /// </summary>
        public void Resume()
        {
            this.mediaElement.Play();
        }

        /// <summary>
        /// Skips <paramref name="time"/> from <see cref="Position"/>
        /// Guaranteed to be within 0 and <see cref="Length"/> after.
        /// </summary>
        /// <param name="time">The amount of time to skip, can be negative.</param>
        public void Skip(TimeSpan time)
        {
            if (this.Position == null || this.Length == null)
            {
                return;
            }

            var newTime = (this.Position.Value + time).TotalSeconds;
            newTime = Math.Max(0, Math.Min(this.Length.Value.TotalSeconds, newTime));
            this.SetCurrentValue(PositionProperty, TimeSpan.FromSeconds(newTime));
        }

        private static void OnPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var wrapper = (MediaElementWrapper)d;
            if (wrapper.mediaElement != null && e.NewValue != null)
            {
                wrapper.mediaElement.Position = (TimeSpan)e.NewValue;
            }
        }

        private static object OnPositionCoerce(DependencyObject d, object basevalue)
        {
            var wrapper = (MediaElementWrapper)d;
            if (wrapper.Length == null || basevalue == null)
            {
                return null;
            }

            var time = (TimeSpan)basevalue;
            if (time < TimeSpan.Zero)
            {
                return TimeSpan.Zero;
            }

            if (time > wrapper.Length.Value)
            {
                return wrapper.Length.Value;
            }

            return basevalue;
        }

        private static void OnStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var wrapper = (MediaElementWrapper)d;
            var newValue = (MediaState)e.NewValue;
            wrapper.SetCurrentValue(IsPlayingProperty, newValue == MediaState.Play);
            if (wrapper.mediaElement.Source == null)
            {
                return;
            }

            switch (newValue)
            {
                case MediaState.Play:
                    wrapper.mediaElement.Play();
                    wrapper.updatePositionTimer.Start();
                    break;
                case MediaState.Pause:
                    wrapper.mediaElement.Pause();
                    wrapper.updatePositionTimer.Stop();
                    break;
                case MediaState.Stop:
                    wrapper.mediaElement.Stop();
                    wrapper.updatePositionTimer.Stop();
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

        private static ExecutedRoutedEventHandler HandleExecute(Action<object> action)
        {
            return (o, e) =>
            {
                action(e.Parameter);
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
            var binding = new Binding(sourceProperty.Name)
            {
                Mode = BindingMode.OneWay,
                Source = this,
                NotifyOnTargetUpdated = true,
            };
            BindingOperations.SetBinding(this.mediaElement, targetProperty, binding);
        }

        private void OnTargetUpdated(object sender, DataTransferEventArgs e)
        {
            if (e.Property == MediaElement.SourceProperty)
            {
                if (this.mediaElement.Source != null)
                {
                    switch (this.LoadedBehavior)
                    {
                        case MediaState.Play:
                        case MediaState.Pause:
                        case MediaState.Stop:
                            // this gets stopped or paused in the loaded event
                            // we do this to load so we get data like length.
                            this.mediaElement.Play();
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
        }

        private double GetVolumeIncrementOrDefault(object parameter)
        {
            if (parameter == null)
            {
                return this.VolumeIncrement;
            }

            return parameter as double? ?? 0;
        }
    }
}
