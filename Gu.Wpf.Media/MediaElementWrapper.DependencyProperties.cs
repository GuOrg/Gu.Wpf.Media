namespace Gu.Wpf.Media
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public partial class MediaElementWrapper
    {
#pragma warning disable SA1202 // Elements must be ordered by access

        /// <summary>
        /// Identifies the <see cref="MediaElementWrapper.State" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="MediaElementWrapper.State" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty StateProperty = DependencyProperty.Register(
            nameof(State),
            typeof(MediaState),
            typeof(MediaElementWrapper),
            new PropertyMetadata(default(MediaState), OnStateChanged));

        /// <summary>
        /// Identifies the <see cref="MediaElementWrapper.IsPlaying" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="MediaElementWrapper.IsPlaying" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty IsPlayingProperty = DependencyProperty.Register(
            nameof(IsPlaying),
            typeof(bool),
            typeof(MediaElementWrapper),
            new PropertyMetadata(default(bool), OnIsPlayingChanged));

        /// <summary>
        /// Identifies the <see cref="MediaElementWrapper.Position" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="MediaElementWrapper.Position" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty PositionProperty = DependencyProperty.Register(
            nameof(Position),
            typeof(TimeSpan?),
            typeof(MediaElementWrapper),
            new PropertyMetadata(default(TimeSpan?), OnPositionChanged, OnPositionCoerce));

        private static readonly DependencyPropertyKey LengthPropertyKey = DependencyProperty.RegisterReadOnly(
            "Length",
            typeof(TimeSpan?),
            typeof(MediaElementWrapper),
            new PropertyMetadata(default(TimeSpan?)));

        /// <summary>
        /// Identifies the <see cref="MediaElementWrapper.Length" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="MediaElementWrapper.Length" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty LengthProperty = LengthPropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey CanPausePropertyKey = DependencyProperty.RegisterReadOnly(
            "CanPause",
            typeof(bool?),
            typeof(MediaElementWrapper),
            new PropertyMetadata(default(bool?)));

        /// <summary>
        /// Identifies the <see cref="MediaElementWrapper.CanPauseMedia" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="MediaElementWrapper.CanPauseMedia" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty CanPauseMediaProperty = CanPausePropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey NaturalVideoHeightPropertyKey = DependencyProperty.RegisterAttachedReadOnly(
            nameof(NaturalVideoHeight),
            typeof(int?),
            typeof(MediaElementWrapper),
            new PropertyMetadata(default(int?)));

        /// <summary>
        /// Identifies the <see cref="MediaElementWrapper.NaturalVideoHeight" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="MediaElementWrapper.NaturalVideoHeight" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty NaturalVideoHeightProperty = NaturalVideoHeightPropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey NaturalVideoWidthPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(NaturalVideoWidth),
            typeof(int?),
            typeof(MediaElementWrapper),
            new PropertyMetadata(default(int?)));

        /// <summary>
        /// Identifies the <see cref="MediaElementWrapper.NaturalVideoWidth" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="MediaElementWrapper.NaturalVideoWidth" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty NaturalVideoWidthProperty = NaturalVideoWidthPropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey HasAudioPropertyKey = DependencyProperty.RegisterReadOnly(
            "HasAudio",
            typeof(bool?),
            typeof(MediaElementWrapper),
            new PropertyMetadata(default(bool?)));

        /// <summary>
        /// Identifies the <see cref="MediaElementWrapper.HasAudio" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="MediaElementWrapper.HasAudio" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty HasAudioProperty = HasAudioPropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey HasVideoPropertyKey = DependencyProperty.RegisterReadOnly(
            "HasVideo",
            typeof(bool?),
            typeof(MediaElementWrapper),
            new PropertyMetadata(default(bool?)));

        /// <summary>
        /// Identifies the <see cref="MediaElementWrapper.HasVideo" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="MediaElementWrapper.HasVideo" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty HasVideoProperty = HasVideoPropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey HasMediaPropertyKey = DependencyProperty.RegisterReadOnly(
            "HasMedia",
            typeof(bool),
            typeof(MediaElementWrapper),
            new PropertyMetadata(default(bool)));

        /// <summary>
        /// Identifies the <see cref="MediaElementWrapper.HasMedia" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="MediaElementWrapper.HasMedia" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty HasMediaProperty = HasMediaPropertyKey.DependencyProperty;

        /// <summary>
        /// Identifies the <see cref="MediaElementWrapper.SpeedRatio" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="MediaElementWrapper.SpeedRatio" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty SpeedRatioProperty = DependencyProperty.Register(
            nameof(SpeedRatio),
            typeof(double),
            typeof(MediaElementWrapper),
            new PropertyMetadata(default(double), OnSpeedRatioChanged));

        private static readonly DependencyPropertyKey IsBufferingPropertyKey = DependencyProperty.RegisterReadOnly(
            "IsBuffering",
            typeof(bool),
            typeof(MediaElementWrapper),
            new PropertyMetadata(default(bool)));

        /// <summary>
        /// Identifies the <see cref="MediaElementWrapper.IsBuffering" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="MediaElementWrapper.IsBuffering" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty IsBufferingProperty = IsBufferingPropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey DownloadProgressPropertyKey = DependencyProperty.RegisterReadOnly(
            "DownloadProgress",
            typeof(double),
            typeof(MediaElementWrapper),
            new PropertyMetadata(default(double)));

        /// <summary>
        /// Identifies the <see cref="MediaElementWrapper.DownloadProgress" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="MediaElementWrapper.DownloadProgress" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty DownloadProgressProperty = DownloadProgressPropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey BufferingProgressPropertyKey = DependencyProperty.RegisterReadOnly(
            "BufferingProgress",
            typeof(double),
            typeof(MediaElementWrapper),
            new PropertyMetadata(default(double)));

        /// <summary>
        /// Identifies the <see cref="MediaElementWrapper.BufferingProgress" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="MediaElementWrapper.BufferingProgress" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty BufferingProgressProperty = BufferingProgressPropertyKey.DependencyProperty;

        /// <summary>
        /// Identifies the <see cref="MediaElementWrapper.VolumeIncrement" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="MediaElementWrapper.VolumeIncrement" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty VolumeIncrementProperty = DependencyProperty.Register(
            nameof(VolumeIncrement),
            typeof(double),
            typeof(MediaElementWrapper),
            new PropertyMetadata(0.05));

        /// <summary>
        /// Gets or sets a list with video file formats.
        /// This is a convenience for use in <see cref="Microsoft.Win32.OpenFileDialog"/>
        /// https://support.microsoft.com/en-us/kb/316992
        /// </summary>
        public static readonly DependencyProperty VideoFormatsProperty = DependencyProperty.Register(
            nameof(VideoFormats),
            typeof(string),
            typeof(MediaElementWrapper),
            new PropertyMetadata(FileFormats.DefaultVideoFormats));

        /// <summary>
        /// Gets or sets a list with audio file formats.
        /// This is a convenience for use in <see cref="Microsoft.Win32.OpenFileDialog"/>
        /// https://support.microsoft.com/en-us/kb/316992
        /// </summary>
        public static readonly DependencyProperty AudioFormatsProperty = DependencyProperty.Register(
            nameof(AudioFormats),
            typeof(string),
            typeof(MediaElementWrapper),
            new PropertyMetadata(FileFormats.DefaultAudioFormats));

        /// <summary>
        /// The DependencyProperty for the MediaElementWrapper.LoadedBehavior property.
        /// </summary>
        public static readonly DependencyProperty LoadedBehaviorProperty = MediaElement.LoadedBehaviorProperty.AddOwner(
            typeof(MediaElementWrapper),
            new FrameworkPropertyMetadata(MediaState.Play));

#pragma warning restore SA1202 // Elements must be ordered by access

        /// <summary>
        /// Get or sets the current <see cref="MediaState"/>.
        /// </summary>
        public MediaState State
        {
            get { return (MediaState)this.GetValue(StateProperty); }
            set { this.SetValue(StateProperty, value); }
        }

        /// <summary>
        /// Sets <see cref="State"/> to <see cref="MediaState.Pause"/> if <see cref="MediaState.Play"/> and to <see cref="MediaState.Play"/> if <see cref="MediaState.Pause"/>
        /// </summary>
        public bool IsPlaying
        {
            get { return (bool)this.GetValue(IsPlayingProperty); }
            set { this.SetValue(IsPlayingProperty, value); }
        }

        /// <summary>
        /// Gets or sets the current position in the media.
        /// Null if no media.
        /// </summary>
        public TimeSpan? Position
        {
            get { return (TimeSpan?)this.GetValue(PositionProperty); }
            set { this.SetValue(PositionProperty, value); }
        }

        /// <summary>
        /// The length of the current media
        /// </summary>
        public TimeSpan? Length
        {
            get { return (TimeSpan?)this.GetValue(LengthProperty); }
            protected set { this.SetValue(LengthPropertyKey, value); }
        }

        /// <summary>
        /// Returns whether the given media can be paused. This is only valid
        /// after the MediaOpened event has fired.
        /// </summary>
        public bool? CanPauseMedia
        {
            get { return (bool?)this.GetValue(CanPauseMediaProperty); }
            protected set { this.SetValue(CanPausePropertyKey, value); }
        }

        /// <summary>
        /// Returns the natural height of the media in the video. Only valid after
        /// the MediaOpened event has fired.
        /// </summary>
        public int? NaturalVideoHeight
        {
            get { return (int?)this.GetValue(NaturalVideoHeightProperty); }
            protected set { this.SetValue(NaturalVideoHeightPropertyKey, value); }
        }

        /// <summary>
        /// Returns the natural width of the media in the video. Only valid after
        /// the MediaOpened event has fired.
        /// </summary>
        public int? NaturalVideoWidth
        {
            get { return (int?)this.GetValue(NaturalVideoWidthProperty); }
            protected set { this.SetValue(NaturalVideoWidthPropertyKey, value); }
        }

        /// <summary>
        /// Returns whether the given media has audio, null if no media.
        /// </summary>
        public bool? HasAudio
        {
            get { return (bool?)this.GetValue(HasAudioProperty); }
            protected set { this.SetValue(HasAudioPropertyKey, value); }
        }

        /// <summary>
        /// Returns whether the given media has video, null if no media.
        /// </summary>
        public bool? HasVideo
        {
            get { return (bool?)this.GetValue(HasVideoProperty); }
            protected set { this.SetValue(HasVideoPropertyKey, value); }
        }

        /// <summary>
        /// Returns whether the given media has video.
        /// </summary>
        public bool HasMedia
        {
            get { return (bool)this.GetValue(HasMediaProperty); }
            protected set { this.SetValue(HasMediaPropertyKey, value); }
        }

        /// <summary>
        /// Allows the speed ration of the media to be controlled.
        /// </summary>
        public double SpeedRatio
        {
            get { return (double)this.GetValue(SpeedRatioProperty); }
            set { this.SetValue(SpeedRatioProperty, value); }
        }

        /// <summary>
        /// Returns whether the given media is currently being buffered. This
        /// applies to network accessed media only.
        /// </summary>
        public bool IsBuffering
        {
            get { return (bool)this.GetValue(IsBufferingProperty); }
            protected set { this.SetValue(IsBufferingPropertyKey, value); }
        }

        /// <summary>
        /// Returns the download progress of the media.
        /// </summary>
        public double DownloadProgress
        {
            get { return (double)this.GetValue(DownloadProgressProperty); }
            protected set { this.SetValue(DownloadProgressPropertyKey, value); }
        }

        /// <summary>
        /// Returns the buffering progress of the media.
        /// </summary>
        public double BufferingProgress
        {
            get { return (double)this.GetValue(BufferingProgressProperty); }
            protected set { this.SetValue(BufferingProgressPropertyKey, value); }
        }

        /// <summary>
        /// The increment by which the IncreaseVolume
        /// </summary>
        public double VolumeIncrement
        {
            get { return (double)this.GetValue(VolumeIncrementProperty); }
            set { this.SetValue(VolumeIncrementProperty, value); }
        }

        /// <summary>
        /// A list of video file formats that can be sued when opening files.
        /// </summary>
        public string VideoFormats
        {
            get { return (string)this.GetValue(VideoFormatsProperty); }
            set { this.SetValue(VideoFormatsProperty, value); }
        }

        /// <summary>
        /// A list of audio file formats that can be sued when opening files.
        /// </summary>
        public string AudioFormats
        {
            get { return (string)this.GetValue(AudioFormatsProperty); }
            set { this.SetValue(AudioFormatsProperty, value); }
        }

        /// <summary>
        /// Specifies the initial state when media is loaded.
        /// Unlike MediaElement.LoadedBehavior this does not affect controlling playback later.
        /// </summary>
        public MediaState LoadedBehavior
        {
            get { return (MediaState) this.GetValue(LoadedBehaviorProperty); }
            set { this.SetValue(LoadedBehaviorProperty, value); }
        }

        private static void OnIsPlayingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var wrapper = (MediaElementWrapper)d;
            if ((bool)e.NewValue)
            {
                wrapper.SetCurrentValue(StateProperty, MediaState.Play);
            }
            else if (wrapper.State == MediaState.Play)
            {
                wrapper.SetCurrentValue(StateProperty, MediaState.Pause);
            }
        }

        private static void OnSpeedRatioChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((MediaElementWrapper)d).mediaElement.SpeedRatio = (double)e.NewValue;
        }

        private static void OnVolumeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var wrapper = (MediaElementWrapper)d;
            var newValue = (double)e.NewValue;
            if (newValue <= 0)
            {
                wrapper.SetCurrentValue(IsMutedProperty, true);
            }
            else if (wrapper.IsMuted && newValue >= 0)
            {
                wrapper.SetCurrentValue(IsMutedProperty, false);
            }
        }

        private static object OnIsMutedCoerce(DependencyObject d, object basevalue)
        {
            var wrapper = (MediaElementWrapper)d;
            if (wrapper.Volume <= 0)
            {
                return true;
            }

            return basevalue;
        }
    }
}