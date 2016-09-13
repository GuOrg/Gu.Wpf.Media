namespace VideoBox
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Threading;

    using Microsoft.Win32;

    public partial class VideoView : UserControl
    {
        private bool isPlaying;

        public VideoView()
        {
            InitializeComponent();
            this.MediaElement.MediaOpened += this.OnMediaOpened;

            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(0.1) };
            timer.Tick += (_, __) => { this.ProgressSlider.Value = this.MediaElement?.Position.TotalSeconds ?? 0; };
            timer.Start();
        }

        private void OpenCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OpenExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Media files|*.mp3;*.mpg;*.mpeg;*.mp4|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                this.MediaElement.Source = new Uri(openFileDialog.FileName);
                this.MediaElement.Play();
            }
        }

        private void PlayCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !this.isPlaying && this.MediaElement?.NaturalDuration.HasTimeSpan == true;
        }

        private void PlayExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.MediaElement.Play();
            this.isPlaying = true;
        }

        private void PauseCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.isPlaying;
        }

        private void PauseExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.MediaElement.Pause();
            this.isPlaying = false;
        }

        private void StopCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.isPlaying;
        }

        private void StopExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.MediaElement.Stop();
            this.isPlaying = false;
        }

        private void ForwardCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.MediaElement?.NaturalDuration.HasTimeSpan == true &&
                           this.MediaElement.Position < this.MediaElement.NaturalDuration.TimeSpan;
        }

        private async void ForwardExecuted(object sender, ExecutedRoutedEventArgs e)
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

        private void OnProgressSliderDragStarted(object sender, DragStartedEventArgs e)
        {
            this.MediaElement.Pause();
        }

        private void OnProgressSliderDragCompleted(object sender, DragCompletedEventArgs e)
        {
            this.MediaElement.Position = TimeSpan.FromSeconds(this.ProgressSlider.Value);
            if (this.isPlaying)
            {
                this.MediaElement.Play();
            }
        }

        private void OnProgressSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.MediaElement.Position = TimeSpan.FromSeconds(this.ProgressSlider.Value);
        }

        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            this.MediaElement.Volume += e.Delta > 0 ? 0.1 : -0.1;
        }
    }
}
