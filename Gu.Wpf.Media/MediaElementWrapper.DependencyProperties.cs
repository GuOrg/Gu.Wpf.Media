namespace Gu.Wpf.Media
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public partial class MediaElementWrapper
    {
        public static readonly DependencyProperty StateProperty = DependencyProperty.Register(
            nameof(State),
            typeof(MediaState),
            typeof(MediaElementWrapper),
            new PropertyMetadata(default(MediaState), OnStateChanged));

        public static readonly DependencyProperty PositionProperty = DependencyProperty.Register(
            nameof(Position),
            typeof(TimeSpan?),
            typeof(MediaElementWrapper),
            new PropertyMetadata(default(TimeSpan?), OnPositionChanged));

        private static readonly DependencyPropertyKey LengthPropertyKey = DependencyProperty.RegisterReadOnly(
            "Length",
            typeof(TimeSpan?),
            typeof(MediaElementWrapper),
            new PropertyMetadata(default(TimeSpan?)));

        public static readonly DependencyProperty LengthProperty = LengthPropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey CanPausePropertyKey = DependencyProperty.RegisterReadOnly(
            "CanPause",
            typeof(bool?),
            typeof(MediaElementWrapper),
            new PropertyMetadata(default(bool?)));

        public static readonly DependencyProperty CanPauseMediaProperty = CanPausePropertyKey.DependencyProperty;

        public static readonly DependencyProperty NaturalVideoHeightProperty = DependencyProperty.Register(
            nameof(NaturalVideoHeight),
            typeof(int?),
            typeof(MediaElementWrapper),
            new PropertyMetadata(default(int?)));

        public static readonly DependencyProperty NaturalVideoWidthProperty = DependencyProperty.Register(
            nameof(NaturalVideoWidth),
            typeof(int?),
            typeof(MediaElementWrapper),
            new PropertyMetadata(default(int?)));

        private static readonly DependencyPropertyKey HasAudioPropertyKey = DependencyProperty.RegisterReadOnly(
            "HasAudio",
            typeof(bool?),
            typeof(MediaElementWrapper),
            new PropertyMetadata(default(bool?)));

        public static readonly DependencyProperty HasAudioProperty = HasAudioPropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey HasVideoPropertyKey = DependencyProperty.RegisterReadOnly(
            "HasVideo",
            typeof(bool?),
            typeof(MediaElementWrapper),
            new PropertyMetadata(default(bool?)));

        public static readonly DependencyProperty HasVideoProperty = HasVideoPropertyKey.DependencyProperty;

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

        public static readonly DependencyProperty IsBufferingProperty = IsBufferingPropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey DownloadProgressPropertyKey = DependencyProperty.RegisterReadOnly(
            "DownloadProgress",
            typeof(double),
            typeof(MediaElementWrapper),
            new PropertyMetadata(default(double)));

        public static readonly DependencyProperty DownloadProgressProperty = DownloadProgressPropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey BufferingProgressPropertyKey = DependencyProperty.RegisterReadOnly(
            "BufferingProgress",
            typeof(double),
            typeof(MediaElementWrapper),
            new PropertyMetadata(default(double)));

        public static readonly DependencyProperty BufferingProgressProperty = BufferingProgressPropertyKey.DependencyProperty;

        public static readonly DependencyProperty VolumeIncrementProperty = DependencyProperty.Register(
            nameof(VolumeIncrement),
            typeof(double),
            typeof(MediaElementWrapper),
            new PropertyMetadata(0.05));

        public static readonly DependencyProperty VideoFormatsProperty = DependencyProperty.Register(
            nameof(VideoFormats),
            typeof(string),
            typeof(MediaElementWrapper),
            new PropertyMetadata("*.dat; *.wmv; *.3g2; *.3gp; *.3gp2; *.3gpp; *.amv; *.asf;  *.avi; *.bin; *.cue; *.divx; *.dv; *.flv; *.gxf; *.iso; *.m1v; *.m2v; *.m2t; *.m2ts; *.m4v; *.mkv; *.mov; *.mp2; *.mp2v; *.mp4; *.mp4v; *.mpa; *.mpe; *.mpeg; *.mpeg1; *.mpeg2; *.mpeg4; *.mpg; *.mpv2; *.mts; *.nsv; *.nuv; *.ogg; *.ogm; *.ogv; *.ogx; *.ps; *.rec; *.rm; *.rmvb; *.tod; *.ts; *.tts; *.vob; *.vro; *.webm"));

        // https://support.microsoft.com/en-us/kb/316992
        public static readonly DependencyProperty AudioFormatsProperty = DependencyProperty.Register(
            nameof(AudioFormats),
            typeof(string),
            typeof(MediaElementWrapper),
            new PropertyMetadata("*.mp3; *.wma; *.aac; *.adt; *.adts; *.m4a; *.wav; *.aif; *.aifc; *.aiff; *.cda"));

        public MediaState State
        {
            get { return (MediaState)this.GetValue(StateProperty); }
            set { this.SetValue(StateProperty, value); }
        }

        /// <summary>
        /// The position in the current media.
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
            set { this.SetValue(NaturalVideoHeightProperty, value); }
        }

        /// <summary>
        /// Returns the natural width of the media in the video. Only valid after
        /// the MediaOpened event has fired.
        /// </summary>
        public int? NaturalVideoWidth
        {
            get { return (int?)this.GetValue(NaturalVideoWidthProperty); }
            set { this.SetValue(NaturalVideoWidthProperty, value); }
        }

        /// <summary>
        /// Returns whether the given media has audio. Only valid after the
        /// MediaOpened event has fired.
        /// </summary>
        public bool? HasAudio
        {
            get { return (bool?)this.GetValue(HasAudioProperty); }
            protected set { this.SetValue(HasAudioPropertyKey, value); }
        }

        /// <summary>
        /// Returns whether the given media has video. Only valid after the
        /// MediaOpened event has fired.
        /// </summary>
        public bool? HasVideo
        {
            get { return (bool?)this.GetValue(HasVideoProperty); }
            protected set { this.SetValue(HasVideoPropertyKey, value); }
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

        private static void OnSpeedRatioChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((MediaElementWrapper)d).mediaElement.SpeedRatio = (double)e.NewValue;
        }
    }
}