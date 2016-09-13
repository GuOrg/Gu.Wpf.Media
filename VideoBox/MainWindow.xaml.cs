namespace VideoBox
{
    using System;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Threading;
    using Microsoft.Win32;

    public partial class MainWindow : Window
    {
        private bool isPlaying;
        private bool isSeeking;

        public MainWindow()
        {
            this.InitializeComponent();
            this.MediaElement.MediaOpened += this.OnMediaOpened;

            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(0.1) };
            timer.Tick += (_, __) => { this.ProgressSlider.Value =  this.MediaElement?.Position.TotalSeconds ?? 0; };
            timer.Start();
        }
  
        private void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Media files (*.mp3;*.mpg;*.mpeg)|*.mp3;*.mpg;*.mpeg;*.mp4|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                this.MediaElement.Source = new Uri(openFileDialog.FileName);
                this.MediaElement.Play();
            }
        }

        private void Play_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !this.isPlaying && this.MediaElement?.NaturalDuration.HasTimeSpan == true;
        }

        private void Play_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.MediaElement.Play();
            this.isPlaying = true;
        }

        private void Pause_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.isPlaying;
        }

        private void Pause_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.MediaElement.Pause();
            this.isPlaying = false;
        }

        private void Stop_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.isPlaying;
        }

        private void Stop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.MediaElement.Stop();
            this.isPlaying = false;
        }


        private void Next_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.MediaElement?.NaturalDuration.HasTimeSpan == true &&
                           this.MediaElement.Position < this.MediaElement.NaturalDuration.TimeSpan;
        }

        private async void Next_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var position = this.MediaElement.Position;
            this.MediaElement.Play();
            while (this.MediaElement.Position <= position)
            {
                await Dispatcher.Yield();
            }

            this.MediaElement.Pause();
        }

        private void OnMediaOpened(object _, RoutedEventArgs __)
        {
            this.MediaElement.Pause();
            this.isPlaying = false;
            this.ProgressSlider.Maximum = this.MediaElement.NaturalDuration.TimeSpan.TotalSeconds;
        }

        private void OnDragStarted(object sender, DragStartedEventArgs e)
        {
            this.isSeeking = true;
            this.MediaElement.Pause();
        }

        private void OnDragCompleted(object sender, DragCompletedEventArgs e)
        {
            this.isSeeking = false;
            this.MediaElement.Position = TimeSpan.FromSeconds(this.ProgressSlider.Value);
            if (this.isPlaying)
            {
                this.MediaElement.Play();
            }
        }

        private void OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.MediaElement.Position = TimeSpan.FromSeconds(this.ProgressSlider.Value);
        }

        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            this.MediaElement.Volume += e.Delta > 0 ? 0.1 : -0.1;
        }
    }
}
