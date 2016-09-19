namespace Gu.Wpf.Media
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls;
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
                    this.ResetToNoSource();
                    this.ReRaiseEvent(o, e);
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
                this.HasMedia = true;
                this.HasAudio = this.mediaElement.HasAudio;
                this.HasVideo = this.mediaElement.HasVideo;
                this.CanPauseMedia = this.mediaElement.CanPause;
                this.NaturalVideoHeight = this.mediaElement.NaturalVideoHeight;
                this.NaturalVideoWidth = this.mediaElement.NaturalVideoWidth;
                this.Length = this.mediaElement.NaturalDuration.TimeSpan;
                if (this.State == MediaState.Pause || this.State == MediaState.Stop)
                {
                    this.SetCurrentValue(PositionProperty, TimeSpan.Zero);
                }

                this.ReRaiseEvent(o, e);
                CommandManager.InvalidateRequerySuggested();
            };

            this.CommandBindings.Add(new CommandBinding(MediaCommands.Play, HandleExecute(this.Play), HandleCanExecute(this.CanPlay)));
            this.CommandBindings.Add(new CommandBinding(MediaCommands.Pause, HandleExecute(this.Pause), HandleCanExecute(this.CanPause)));
            this.CommandBindings.Add(new CommandBinding(MediaCommands.Stop, HandleExecute(this.Stop), HandleCanExecute(this.CanStop)));
            this.CommandBindings.Add(new CommandBinding(MediaCommands.TogglePlayPause, HandleExecute(this.TogglePlayPause), HandleCanExecute(() => this.CanPlay() || this.CanPause())));
            this.CommandBindings.Add(new CommandBinding(MediaCommands.Rewind, HandleExecute(this.Rewind), HandleCanExecute(this.CanRewind)));
            this.CommandBindings.Add(new CommandBinding(MediaCommands.IncreaseVolume, HandleExecute(this.IncreaseVolume), HandleCanExecute(this.CanIncreaseVolume)));
            this.CommandBindings.Add(new CommandBinding(MediaCommands.DecreaseVolume, HandleExecute(this.DecreaseVolume), HandleCanExecute(this.CanDecreaseVolume)));
            this.CommandBindings.Add(new CommandBinding(MediaCommands.MuteVolume, HandleExecute(this.MuteVolume), HandleCanExecute(this.CanMuteVolume)));

            this.CommandBindings.Add(new CommandBinding(Commands.UnmuteVolume, HandleExecute(this.UnmuteVolume), HandleCanExecute(this.CanUnmuteVolume)));
            this.CommandBindings.Add(new CommandBinding(Commands.ToggleMute, HandleExecute(this.ToggleMute), HandleCanExecute(this.CanToggleMute)));
            this.CommandBindings.Add(new CommandBinding(Commands.Skip, HandleExecute(this.Skip), HandleCanExecute(this.CanSkip)));
            this.CommandBindings.Add(new CommandBinding(Commands.SkipForward, HandleExecute(this.SkipForward), HandleCanExecute(this.CanSkipForward)));
            this.CommandBindings.Add(new CommandBinding(Commands.SkipBack, HandleExecute(this.SkipBack), HandleCanExecute(this.CanSkipBack)));

            this.updatePositionTimer.Tick += (o, e) => this.SetCurrentValue(PositionProperty, this.mediaElement.Position);
            this.updateProgressTimer.Tick += (o, e) =>
                {
                    this.DownloadProgress = this.mediaElement.DownloadProgress;
                    this.BufferingProgress = this.mediaElement.BufferingProgress;
                };
            this.mediaElement.IsMuted = this.IsMuted;
            this.mediaElement.Volume = this.Volume;
            this.mediaElement.Balance = this.Balance;
            this.mediaElement.ScrubbingEnabled = this.ScrubbingEnabled;
            this.mediaElement.Stretch = this.Stretch;
            this.mediaElement.StretchDirection = this.StretchDirection;
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
        /// <param name="parameter">The command parameter</param>
        /// <returns>True if <see cref="Volume"/> can be decreased.</returns>
        public bool CanDecreaseVolume(object parameter)
        {
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            return this.Volume > 0 && !this.IsMuted && this.GetVolumeIncrementOrDefault(parameter) != 0;
        }

        /// <summary>
        /// <see cref="Volume"/> is a value between 0 and 1.
        /// Decreases <see cref="Volume"/> by <paramref name="parameter"/>
        /// Truncates to between 0 and 1 if overflow.
        /// </summary>
        /// <param name="parameter">
        /// The command parameter
        /// A value between 0 and 1. Typical value is 0.05.
        /// If null <see cref="VolumeIncrement"/> is used.
        /// </param>
        public void DecreaseVolume(object parameter)
        {
            this.DecreaseVolume(this.GetVolumeIncrementOrDefault(parameter));
        }

        /// <summary>
        /// <see cref="Volume"/> is a value between 0 and 1.
        /// Decreases <see cref="Volume"/> by <paramref name="increment"/>
        /// Truncates to between 0 and 1 if overflow.
        /// </summary>
        /// <param name="increment">
        /// A value between 0 and 1. Typical value is 0.05.
        /// </param>
        public void DecreaseVolume(double increment)
        {
            this.SetCurrentValue(VolumeProperty, this.mediaElement.Volume - increment);
            this.SetCurrentValue(IsMutedProperty, this.Volume <= 0);
        }

        /// <summary>
        /// Check if <see cref="Volume"/> can be increased.
        /// </summary>
        /// <param name="parameter">The command parameter</param>
        /// <returns>True if <see cref="Volume"/> can be increased.</returns>
        public bool CanIncreaseVolume(object parameter)
        {
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            return this.Volume < 1 && this.GetVolumeIncrementOrDefault(parameter) != 0;
        }

        /// <summary>
        /// <see cref="Volume"/> is a value between 0 and 1.
        /// Increases <see cref="Volume"/> by <paramref name="parameter"/>
        /// Truncates to between 0 and 1 if overflow.
        /// </summary>
        /// <param name="parameter">
        /// A value between 0 and 1. Typical value is 0.05.
        /// If null <see cref="VolumeIncrement"/> is used.
        /// </param>
        public void IncreaseVolume(object parameter)
        {
            this.IncreaseVolume(this.GetVolumeIncrementOrDefault(parameter));
        }

        /// <summary>
        /// <see cref="Volume"/> is a value between 0 and 1.
        /// Increases <see cref="Volume"/> by <paramref name="increment"/>
        /// Truncates to between 0 and 1 if overflow.
        /// </summary>
        /// <param name="increment">
        /// A value between 0 and 1. Typical value is 0.05.
        /// </param>
        public void IncreaseVolume(double increment)
        {
            if (this.IsMuted)
            {
                this.SetCurrentValue(VolumeProperty, increment);
                this.SetCurrentValue(IsMutedProperty, this.Volume <= 0);
                return;
            }

            this.SetCurrentValue(VolumeProperty, this.mediaElement.Volume + increment);
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
        /// Check if skip can be performed.
        /// </summary>
        /// <param name="parameter">The command parameter</param>
        /// <returns>True if <see cref="Length"/> is not null</returns>
        public bool CanSkip(object parameter)
        {
            return this.Length != null && this.GetSkipIncrement(parameter) != TimeSpan.Zero;
        }

        /// <summary>
        /// Changes current position by <paramref name="parameter"/>
        /// </summary>
        /// <param name="parameter">
        /// If null <see cref="SkipIncrement"/> is used.
        /// If a <see cref="TimeSpan"/> it is used.
        /// If an <see cref="int"/> it is <see cref="SkipIncrement"/> is multiplied by this.
        /// If a <see cref="double"/> position is skippedthis amount of seconds.
        ///  If else no skip is performed.
        /// Negative values are allowed.
        /// </param>
        public void Skip(object parameter)
        {
            this.Skip(this.GetSkipIncrement(parameter));
        }

        /// <summary>
        /// Check if skip can be performed.
        /// </summary>
        /// <param name="parameter">The command parameter</param>
        /// <returns>True if <see cref="Length"/> is not null</returns>
        public bool CanSkipBack(object parameter)
        {
            return this.CanSkip(parameter) && this.Position > TimeSpan.Zero;
        }

        /// <summary>
        /// Changes current position by <paramref name="parameter"/>
        /// </summary>
        /// <param name="parameter">
        /// If null <see cref="SkipIncrement"/> is used.
        /// If a <see cref="TimeSpan"/> it is used.
        /// If an <see cref="int"/> it is <see cref="SkipIncrement"/> is multiplied by this.
        /// If a <see cref="double"/> position is skippedthis amount of seconds.
        ///  If else no skip is performed.
        /// Negative values are allowed.
        /// </param>
        public void SkipBack(object parameter)
        {
            this.Skip(TimeSpan.FromSeconds(-1 * this.GetSkipIncrement(parameter).TotalSeconds));
        }

        /// <summary>
        /// Check if skip can be performed.
        /// </summary>
        /// <param name="parameter">The command parameter</param>
        /// <returns>True if <see cref="Length"/> is not null</returns>
        public bool CanSkipForward(object parameter)
        {
            return this.CanSkip(parameter) && this.Position < this.Length;
        }

        /// <summary>
        /// Changes current position by <paramref name="parameter"/>
        /// </summary>
        /// <param name="parameter">
        /// If null <see cref="SkipIncrement"/> is used.
        /// If a <see cref="TimeSpan"/> it is used.
        /// If an <see cref="int"/> it is <see cref="SkipIncrement"/> is multiplied by this.
        /// If a <see cref="double"/> position is skippedthis amount of seconds.
        ///  If else no skip is performed.
        /// Negative values are allowed.
        /// </param>
        public void SkipForward(object parameter)
        {
            this.Skip(this.GetSkipIncrement(parameter));
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

            this.SetCurrentValue(PositionProperty, this.Position.Value + time);
        }

        /// <summary>
        /// This is invoked when <see cref="Source"/> changes.
        /// </summary>
        /// <param name="source">The new source.</param>
        protected virtual void OnSourceChanged(Uri source)
        {
            if (source == null || string.IsNullOrWhiteSpace(source.OriginalString))
            {
                this.mediaElement.Source = null;
                this.ResetToNoSource();
            }
            else
            {
                if (source.Scheme == Uri.UriSchemeFile)
                {
                    // this looks really strange but it is for handling #  in paths.
                    this.mediaElement.Source = (Uri)TypeDescriptor.GetConverter(typeof(Uri)).ConvertFrom(source.LocalPath);
                }
                else
                {
                    this.mediaElement.Source = source;
                }

                switch (this.LoadedBehavior)
                {
                    case MediaState.Play:
                        if (this.State == MediaState.Play)
                        {
                            this.mediaElement.Play();
                            this.updatePositionTimer.Start();
                        }
                        else
                        {
                            this.Play();
                        }

                        break;
                    case MediaState.Pause:
                        if (this.State == MediaState.Pause)
                        {
                            this.mediaElement.Pause();
                            this.updatePositionTimer.Stop();
                            this.SetCurrentValue(PositionProperty, TimeSpan.Zero);
                        }
                        else
                        {
                            this.Pause();
                        }

                        break;
                    case MediaState.Stop:
                        if (this.State == MediaState.Stop)
                        {
                            this.mediaElement.Stop();
                            this.updatePositionTimer.Stop();
                            this.SetCurrentValue(PositionProperty, TimeSpan.Zero);
                        }
                        else
                        {
                            this.Stop();
                        }

                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
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
            if (string.IsNullOrEmpty(wrapper.mediaElement.Source?.OriginalString))
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

        private static CanExecuteRoutedEventHandler HandleCanExecute(Func<object, bool> canExecute)
        {
            return (o, e) =>
            {
                e.CanExecute = canExecute(e.Parameter);
                e.Handled = true;
            };
        }

        private void ResetToNoSource()
        {
            if (this.State == MediaState.Stop)
            {
                this.mediaElement.Stop();
                this.updatePositionTimer.Stop();
            }
            else
            {
                this.Stop();
            }

            this.HasMedia = false;
            this.HasAudio = null;
            this.HasVideo = null;
            this.IsBuffering = false;
            this.BufferingProgress = 0;
            this.DownloadProgress = 0;
            this.CanPauseMedia = null;
            this.NaturalVideoHeight = null;
            this.NaturalVideoWidth = null;
            this.Length = null;
            this.SetCurrentValue(PositionProperty, null);
            CommandManager.InvalidateRequerySuggested();
        }

        private void ReRaiseEvent(object sender, RoutedEventArgs e)
        {
            this.RaiseEvent(e);
            e.Handled = true;
        }

        private double GetVolumeIncrementOrDefault(object parameter)
        {
            if (parameter == null)
            {
                return this.VolumeIncrement;
            }

            double result;
            if (ObjectExt.TryConvertToDouble(parameter, out result))
            {
                return result;
            }

            return 0;
        }

        private TimeSpan GetSkipIncrement(object parameter)
        {
            if (parameter == null)
            {
                return this.SkipIncrement;
            }

            if (parameter is TimeSpan)
            {
                return (TimeSpan)parameter;
            }

            if (parameter is double)
            {
                return TimeSpan.FromSeconds((double)parameter);
            }

            if (parameter is int)
            {
                return TimeSpan.FromSeconds((int)parameter * this.SkipIncrement.TotalSeconds);
            }

            var text = parameter as string;
            if (text != null)
            {
                if (text == string.Empty)
                {
                    return this.SkipIncrement;
                }

                int intResult;
                if (int.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out intResult))
                {
                    return this.GetSkipIncrement(intResult);
                }

                double doubleResult;
                if (double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out doubleResult))
                {
                    return this.GetSkipIncrement(doubleResult);
                }

                TimeSpan timeResult;
                if (TimeSpan.TryParse(text, CultureInfo.InvariantCulture, out timeResult))
                {
                    return this.GetSkipIncrement(timeResult);
                }
            }

            return TimeSpan.Zero;
        }
    }
}
