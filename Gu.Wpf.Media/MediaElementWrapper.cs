namespace Gu.Wpf.Media
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Threading;

    public class MediaElementWrapper : Decorator
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

        public static readonly DependencyProperty LengthProperty = LengthPropertyKey.DependencyProperty;

        private readonly DispatcherTimer updatePositionTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(0.1) };
        private MediaElement mediaElement;

        public MediaElementWrapper()
        {
            this.mediaElement = new MediaElement
            {
                LoadedBehavior = MediaState.Manual,
                UnloadedBehavior = MediaState.Manual,
                Source = this.Source,
                ScrubbingEnabled = true
            };
            this.mediaElement.MediaOpened += (o, e) =>
            {
                this.Pause();
                this.Length = this.mediaElement.NaturalDuration.TimeSpan;
                CommandManager.InvalidateRequerySuggested();
            };

            this.Bind(SourceProperty, MediaElement.SourceProperty);
            this.Bind(VolumeProperty, MediaElement.VolumeProperty);
            this.CommandBindings.Add(new CommandBinding(MediaCommands.Play, HandleExecute(this.Play), HandleCanExecute(this.CanPlay)));
            this.CommandBindings.Add(new CommandBinding(MediaCommands.Pause, HandleExecute(this.Pause), HandleCanExecute(this.CanPause)));
            this.CommandBindings.Add(new CommandBinding(MediaCommands.Stop, HandleExecute(this.Stop), HandleCanExecute(this.CanStop)));
            this.CommandBindings.Add(new CommandBinding(MediaCommands.TogglePlayPause, HandleExecute(this.TogglePlayPause), HandleCanExecute(() => this.CanPlay() || this.CanPause())));
            this.CommandBindings.Add(new CommandBinding(MediaCommands.Rewind, HandleExecute(this.Rewind), HandleCanExecute(this.CanRewind)));
            this.CommandBindings.Add(new CommandBinding(MediaCommands.IncreaseVolume, HandleExecute(() => this.IncreaseVolume(this.VolumeIncrement)), HandleCanExecute(this.CanIncreaseVolume)));
            this.CommandBindings.Add(new CommandBinding(MediaCommands.DecreaseVolume, HandleExecute(() => this.DecreaseVolume(this.VolumeIncrement)), HandleCanExecute(this.CanDecreaseVolume)));
            this.updatePositionTimer.Tick += (o, e) => this.Position = this.mediaElement?.Position;
        }

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

        public string VideoFormats
        {
            get { return (string)this.GetValue(VideoFormatsProperty); }
            set { this.SetValue(VideoFormatsProperty, value); }
        }

        public string AudioFormats
        {
            get { return (string)this.GetValue(AudioFormatsProperty); }
            set { this.SetValue(AudioFormatsProperty, value); }
        }

        public override void BeginInit()
        {
            this.Child = this.mediaElement;
            base.BeginInit();
        }

        public bool CanPlay()
        {
            return this.mediaElement.Source != null && this.State != MediaState.Play;
        }

        public void Play()
        {
            this.State = MediaState.Play;
        }

        public bool CanPause()
        {
            return this.mediaElement.Source != null && this.State == MediaState.Play;
        }

        public void Pause()
        {
            this.State = MediaState.Pause;
        }

        public void TogglePlayPause()
        {
            this.State = this.State != MediaState.Play
                             ? MediaState.Play
                             : MediaState.Pause;
        }

        public bool CanStop()
        {
            return this.mediaElement.Source != null && this.State == MediaState.Play;
        }

        public void Stop()
        {
            this.State = MediaState.Stop;
        }

        private bool CanRewind()
        {
            return this.mediaElement.Source != null && this.Position > TimeSpan.Zero;
        }

        private void Rewind()
        {
            this.Position = TimeSpan.Zero;
        }

        private bool CanDecreaseVolume()
        {
            return this.Volume > 0;
        }

        private void DecreaseVolume(double increment)
        {
            this.Volume = Math.Min(this.mediaElement.Volume - increment, 1);
        }

        private bool CanIncreaseVolume()
        {
            return this.Volume < 1;
        }

        private void IncreaseVolume(double increment)
        {
            this.Volume = Math.Min(this.mediaElement.Volume + increment, 1);
        }

        private void Bind(DependencyProperty sourceProperty, DependencyProperty targetProperty)
        {
            var binding = new Binding(sourceProperty.Name) { Mode = BindingMode.OneWay, Source = this };
            BindingOperations.SetBinding(this.mediaElement, targetProperty, binding);
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
    }
}
