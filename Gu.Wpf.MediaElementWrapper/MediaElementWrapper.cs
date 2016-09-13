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

        public string AudioFormats
        {
            get { return (string)this.GetValue(AudioFormatsProperty); }
            set { this.SetValue(AudioFormatsProperty, value); }
        }

        public static readonly DependencyProperty LengthProperty = LengthPropertyKey.DependencyProperty;

        private readonly DispatcherTimer updatePositionTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(0.1) };
        private MediaElement mediaElement;

        public MediaElementWrapper()
        {
            this.CommandBindings.Add(new CommandBinding(MediaCommands.Play, HandleExecute(this.Play), HandleCanExecute(this.CanPlay)));
            this.CommandBindings.Add(new CommandBinding(MediaCommands.Pause, HandleExecute(this.Pause), HandleCanExecute(this.CanPause)));
            this.CommandBindings.Add(new CommandBinding(MediaCommands.Stop, HandleExecute(this.Stop), HandleCanExecute(this.CanStop)));
            this.CommandBindings.Add(new CommandBinding(MediaCommands.TogglePlayPause, HandleExecute(this.TogglePlayPause), HandleCanExecute(() => this.CanPlay() || this.CanPause())));
            this.updatePositionTimer.Tick += (o, e) => this.Position = this.mediaElement?.Position;
        }

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

        public TimeSpan? Position
        {
            get { return (TimeSpan?)this.GetValue(PositionProperty); }
            set { this.SetValue(PositionProperty, value); }
        }

        public TimeSpan? Length
        {
            get { return (TimeSpan?)this.GetValue(LengthProperty); }
            protected set { this.SetValue(LengthPropertyKey, value); }
        }

        public string VideoFormats
        {
            get { return (string)this.GetValue(VideoFormatsProperty); }
            set { this.SetValue(VideoFormatsProperty, value); }
        }

        public override void BeginInit()
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

            var sourceBinding = new Binding(SourceProperty.Name) { Mode = BindingMode.OneWay, Source = this };
            BindingOperations.SetBinding(this.mediaElement, MediaElement.SourceProperty, sourceBinding);
            this.Child = this.mediaElement;
            base.BeginInit();
        }

        public bool CanPlay()
        {
            return this.mediaElement?.Source != null && this.State != MediaState.Play;
        }

        public void Play()
        {
            this.State = MediaState.Play;
        }

        public bool CanPause()
        {
            return this.mediaElement?.Source != null && this.State == MediaState.Play;
        }

        public void Pause()
        {
            this.State = MediaState.Pause;
        }

        public void TogglePlayPause()
        {
            if (this.State != MediaState.Play)
            {
                this.State = MediaState.Play;
            }
            else
            {
                this.State = MediaState.Pause;
            }
        }

        public bool CanStop()
        {
            return this.mediaElement?.Source != null && this.State == MediaState.Play;
        }

        public void Stop()
        {
            this.State = MediaState.Stop;
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
            if (wrapper.mediaElement == null)
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
