namespace Gu.Wpf.Media
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public partial class MediaElementWrapper
    {
        public static readonly DependencyProperty SourceProperty = MediaElement.SourceProperty.AddOwner(typeof(MediaElementWrapper));

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

        public static readonly DependencyProperty VolumeProperty = MediaElement.VolumeProperty.AddOwner(typeof(MediaElementWrapper));

        public static readonly DependencyProperty VolumeIncrementProperty = DependencyProperty.Register(
            nameof(VolumeIncrement),
            typeof(double),
            typeof(MediaElementWrapper),
            new PropertyMetadata(default(double)));

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

        /// <summary>
        /// See <see cref="MediaElement.Source"/>
        /// </summary>
        public Uri Source
        {
            get { return (Uri)this.GetValue(SourceProperty); }
            set { this.SetValue(SourceProperty, value); }
        }

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
        /// See <see cref="MediaElement.Volume"/>
        /// </summary>
        public double Volume
        {
            get { return (double)this.GetValue(VolumeProperty); }
            set { this.SetValue(VolumeProperty, value); }
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
    }
}